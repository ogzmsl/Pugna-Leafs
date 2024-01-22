using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private MyPlayer playerActionsAsset;
    private InputAction move;

    private Rigidbody rb;
    private Animator animator;

    [SerializeField] private float movementForce = 1f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float maxSpeed = 5f;
    private Vector3 forceDirection = Vector3.zero;

    [SerializeField] private Camera playerCamera;

    private float horizontalInput;
    private float verticalInput;

    [SerializeField] private float attackCooldown = 1f;
    private bool isAttacking = false;
    private Coroutine attackCooldownCoroutine;

    private bool isSprinting = false;
    [SerializeField] private float sprintMultiplier = 2f; // Sprint hýzýnýn normal hýza oraný
    [SerializeField] private float slideTiming;
    private float smoothedVerticalInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerActionsAsset = new MyPlayer();
    }

    private void OnEnable()
    {
        playerActionsAsset.Player.Jump.started += DoJump;
        playerActionsAsset.Player.Jump.canceled += EndJump;
        playerActionsAsset.Player.Q_Button.started += OnAttackInput;
        playerActionsAsset.Player.E_Button.started += OnEAttackInput;
        playerActionsAsset.Player.F_Button.started += OnFAttackInput;
        move = playerActionsAsset.Player.Move;

        // Sprint giriþi performed olarak deðiþtirildi
        playerActionsAsset.Player.Sprint.performed += OnSprintPerformed;
        playerActionsAsset.Player.Sprint.canceled += OnSprintCanceled;

        playerActionsAsset.Player.Enable();
    }

    private void OnDisable()
    {
        playerActionsAsset.Player.Jump.started -= DoJump;
        playerActionsAsset.Player.Jump.canceled -= EndJump;
        playerActionsAsset.Player.Q_Button.started -= OnAttackInput;
        playerActionsAsset.Player.E_Button.started -= OnEAttackInput;
        playerActionsAsset.Player.F_Button.started -= OnFAttackInput;

        // Sprint giriþi performed olarak deðiþtirildi
        playerActionsAsset.Player.Sprint.performed -= OnSprintPerformed;
        playerActionsAsset.Player.Sprint.canceled -= OnSprintCanceled;

        playerActionsAsset.Player.Disable();
    }

    private void FixedUpdate()
    {
        UpdateInputs();
        UpdateMovement();
        UpdateAnimator();
    }
    private void UpdateInputs()
    {
        float rawHorizontalInput = move.ReadValue<Vector2>().x;
        float rawVerticalInput = move.ReadValue<Vector2>().y;

        float targetVerticalInput = isSprinting ? rawVerticalInput * sprintMultiplier : rawVerticalInput;

       
        smoothedVerticalInput = Mathf.Lerp(smoothedVerticalInput, targetVerticalInput, Time.deltaTime * 5f);

        
        verticalInput = rawVerticalInput;

        horizontalInput = rawHorizontalInput;
    }

    private void UpdateMovement()
    {
        forceDirection += horizontalInput * GetCameraRight(playerCamera) * (isSprinting ? movementForce * sprintMultiplier : movementForce);
        forceDirection += verticalInput * GetCameraForward(playerCamera) * (isSprinting ? movementForce * sprintMultiplier : movementForce);

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if (rb.velocity.y < 0f)
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;

        if (horizontalVelocity.magnitude> maxSpeed)
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;

        LookAt();
    }

    private void UpdateAnimator()
    {
        float sprintMultiplier = 1f;

        if (isSprinting)
        {
            sprintMultiplier = 2f;
        }

        animator.SetFloat("Horizontal", horizontalInput);

        
        animator.SetFloat("Vertical", smoothedVerticalInput * sprintMultiplier);
    }


    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
            rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            rb.angularVelocity = Vector3.zero;
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        if (IsGrounded())
        {
            forceDirection += Vector3.up * jumpForce;

            // Set the playback speed of the jump animation to make it faster
            float jumpAnimationSpeed = 3.0f; // Adjust this value as needed
            animator.SetFloat("JumpSpeedMultiplier", jumpAnimationSpeed);

            animator.SetBool("Jump", true);
        }
    }

    private void EndJump(InputAction.CallbackContext obj)
    {
        animator.SetBool("Jump", false);
    }

    private void OnAttackInput(InputAction.CallbackContext obj)
    {
        if (!isAttacking)
        {
            StartCoroutine(AttackSequence("Attack_Q"));
        }
    }

    private void OnEAttackInput(InputAction.CallbackContext obj)
    {
        if (!isAttacking)
        {
            StartCoroutine(AttackSequence("Attack_E"));
        }
    }

    private void OnFAttackInput(InputAction.CallbackContext obj)
    {
        if (!isAttacking)
        {
            StartCoroutine(AttackSequence("Attack_F"));
        }
    }

    private void OnSprintPerformed(InputAction.CallbackContext obj)
    {
        // Sprint giriþi performed olduðunda sprint durumunu deðiþtir
        isSprinting = true;
        Debug.Log("Sprint started");
    }

    private void OnSprintCanceled(InputAction.CallbackContext obj)
    {
        // Sprint giriþi canceled olduðunda sprint durumunu deðiþtir
        isSprinting = false;
        Debug.Log("Sprint stopped");
    }

    private IEnumerator AttackSequence(string attackAnimationTrigger)
    {
        isAttacking = true;

        // Baþlangýçta saldýrý animasyonunu oynat
        animator.SetBool(attackAnimationTrigger, true);

        // Saldýrý animasyonunun uzunluðunu bekleyerek animasyonun tamamlanmasýný saðla
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Saldýrý animasyonunu kapat
        animator.SetBool(attackAnimationTrigger, false);

        // Kooldown süresince bekleyerek tekrar saldýrýya izin ver
        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;

        // Saldýrýdan sonra kayma hareketini durdur
        //StopSliding();
    }

   /*private void StopSliding()
    {
        // Gradually reduce horizontal velocity
        float dampingFactor = 0.9f;
        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.x *= dampingFactor;
        horizontalVelocity.z *= dampingFactor;
        rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);
    }*/

    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.25f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.3f))
            return true;
        else
            return false;
    }
}
