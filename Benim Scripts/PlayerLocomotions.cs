using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotions : MonoBehaviour
{
    InputManager inputManager;
    animatorManager animatorManager;

    Vector3 MovementDirections; //hareket talimatlrý(yön gibi) yollanýlan
    Transform cameraObject;
    [Header("Movement Flags")]
    public bool isspriting;
    public bool isGrounded;

    public float inAirTimer;
    public float leapingvelocity;
    public float fallingspeed;
    public LayerMask GroundLayer;
    public float raycastheight=0.5f;


    PlayerManager playerManager;
    [Header("Movement Speeds")]
    Rigidbody KarakterRigitbody;
    public float kosmahizi = 5;
    public float yurumehizi = 1.5f;
    public float sprinthizi = 7f;

 
  

    public float RotationdSpeed = 14;


    private float originalColliderHeight;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        KarakterRigitbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        playerManager = GetComponent<PlayerManager>();
        animatorManager = GetComponent<animatorManager>();
        originalColliderHeight = KarakterRigitbody.GetComponent<CapsuleCollider>().height;
        KarakterRigitbody.useGravity = true;
    }

    public void HandleAllMovement() //girdileri tutdum
    {
        HandleAndLanding();
        if (playerManager.isInteracting)
            return;

        HandleMovement();
        HandleRotations();
       
    }

    


    public void HandleJumping()
    {

        if (playerManager.isInteracting || !isGrounded)
            return;

        if (inputManager.jumpInput&&inputManager.canJump)
        {
            animatorManager.PlayTargetAnimation("Jump", true);
            // Add logic for jumping, e.g., applying vertical force
        }
        inputManager.canJump = false;
    }


    private void HandleMovement() //camera yönü girdileri göre Hareket talimatlarýna atandý;
    {
        MovementDirections = cameraObject.forward * inputManager.verticalInput;
        MovementDirections = MovementDirections + cameraObject.right * inputManager.HorizontalInput;
        MovementDirections.Normalize();
        MovementDirections.y = 0; // yukarý bakmasýn -> gökyüzüne

        if (isspriting)
        {
            MovementDirections = MovementDirections * sprinthizi*Time.deltaTime*20;
        }
 



        if (inputManager.MoveAmount >= 0.5f)
        {
            MovementDirections = MovementDirections * kosmahizi;
        }
        else
        {
            MovementDirections = MovementDirections * yurumehizi;
        }

        
        Vector3 movementvelocity = MovementDirections;
        KarakterRigitbody.velocity = movementvelocity;
    }

    private void HandleRotations()
    {
      
        Vector3 targetdirections = Vector3.zero;

       targetdirections = cameraObject.forward * inputManager.verticalInput;
        targetdirections = MovementDirections + cameraObject.right * inputManager.HorizontalInput;
        targetdirections.Normalize();
        targetdirections.y = 0;

        //bu if bloðu gerek duyulursa deaktif yapýlarak input olmazsa yönünü sýfýrlar
        if (targetdirections == Vector3.zero) 
            targetdirections = transform.forward;

        Quaternion targetRotations = Quaternion.LookRotation(targetdirections);
        Quaternion Playerrotions = Quaternion.Slerp(transform.rotation, targetRotations, RotationdSpeed * Time.deltaTime);
        transform.rotation = Playerrotions;
    }

    private void HandleAndLanding()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position;
        raycastOrigin.y = raycastOrigin.y + raycastheight;
        if (!isGrounded)
        {
            if (!playerManager.isInteracting&&!isGrounded)
            {
                animatorManager.PlayTargetAnimation("Falling", true);
            }

            inAirTimer = inAirTimer + Time.deltaTime;
            KarakterRigitbody.AddForce(transform.forward * leapingvelocity);
            KarakterRigitbody.AddForce(-Vector3.up * fallingspeed * inAirTimer);
        }

        if(Physics.SphereCast(raycastOrigin,0.2f,-Vector3.up,out hit,GroundLayer))
        {
            if (!isGrounded && playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Land", true);
            }
           
         
            inAirTimer = 0;
            isGrounded = true;
            
        }
        else
        {
            isGrounded = false;
        }

       
    }

   

   
}
