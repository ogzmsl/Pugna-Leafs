using System.Collections;
using UnityEditor;
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

    [SerializeField] private float attackCooldown = 0.25f;
    private bool isAttacking = false;
    private Coroutine attackCooldownCoroutine;

    private bool isSprinting = false;
    [SerializeField] private float sprintMultiplier = 2f;
    private float slideTiming;
    private float smoothedVerticalInput;
    private float stopThreshold = 0.01f;
    [SerializeField] private float fallGravity = 10f;
    [SerializeField] private float maxFallSpeed = -10f;

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

        playerActionsAsset.Player.Sprint.performed -= OnSprintPerformed;
        playerActionsAsset.Player.Sprint.canceled -= OnSprintCanceled;

        playerActionsAsset.Player.Disable();
    }

    private void FixedUpdate()
    {
        UpdateInputs();
        if (!isAttacking)
        {
            UpdateMovement();
        }
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
        
        if (Mathf.Abs(horizontalInput) > stopThreshold || Mathf.Abs(verticalInput) > stopThreshold)
        {
            forceDirection += horizontalInput * GetCameraRight(playerCamera) * (isSprinting ? movementForce * sprintMultiplier : movementForce);
            forceDirection += verticalInput * GetCameraForward(playerCamera) * (isSprinting ? movementForce * sprintMultiplier : movementForce);

            rb.AddForce(forceDirection, ForceMode.Impulse);
            forceDirection = Vector3.zero;

            if (rb.velocity.y < 0f)
                rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;

            Vector3 horizontalVelocity = rb.velocity;
            horizontalVelocity.y = 0;

            if (horizontalVelocity.sqrMagnitude > maxSpeed)
                rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;
        }
        else
        {
          
            rb.velocity = Vector3.zero;
        }

        

        LookAt();
    }

    private void UpdateAnimator()
    {
        float sprintMultiplier = 1f;

        if (isSprinting)
        {
            sprintMultiplier = 2f;
        }

        bool isGrounded = IsGrounded();

        animator.SetBool("IsGrounded", isGrounded);

        if (isGrounded)
        {
            animator.SetFloat("Horizontal", horizontalInput);
            animator.SetFloat("Vertical", smoothedVerticalInput * sprintMultiplier);
            animator.SetBool("IsFalling", false);
        }
        else if(!isGrounded)
        {
            
            animator.SetFloat("Horizontal", 0f);
            animator.SetFloat("Vertical", 0f);
            animator.SetBool("IsFalling", true);
            rb.velocity += Vector3.down * fallGravity;


        }
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
            float jumpAnimationSpeed = 3.0f;
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
        isSprinting = true;
        Debug.Log("Sprint started");
    }

    private void OnSprintCanceled(InputAction.CallbackContext obj)
    {
        isSprinting = false;
        Debug.Log("Sprint stopped");
    }

    private IEnumerator AttackSequence(string attackAnimationTrigger)
    {
        isAttacking = true;
        animator.SetBool(attackAnimationTrigger, true);
        if (isAttacking)
        {
            rb.velocity = Vector3.zero;
        }
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool(attackAnimationTrigger, false);

        isAttacking = false;
       
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.25f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.3f))
            return true;
        else
            return false;
    }
}
