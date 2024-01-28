using System.Collections;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class ThirdPersonController : MonoBehaviour
    {
        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 2.0f;

        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 5.335f;

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float JumpHeight = 1.2f;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.50f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;

        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;

        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;

        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;

        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;

        // cinemachine
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        // player
        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        public  float _verticalVelocity;
        private float _terminalVelocity = 53.0f;
        [SerializeField] private float _JumpDashHeight;
        [SerializeField] private float _HorizontalJumpVelocit;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;
        private int _animIDOrbball;
        private int _animIDProjectTile;
        private int _animIDMagicAttack;
        private int _animIDBlock;
        private int _animIDRange;
        private int _animIDashRight;



        public float targetspeed;


        Vector3 cameraForward;





#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        private PlayerInput _playerInput;
#endif
        private Animator _animator;
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;

        private const float _threshold = 0.01f;

        private bool _hasAnimator;
        private bool isAttacking;
        private bool isProjectTileAttack;
        private bool isMagicAttack;
        private bool isBlocked;
        private bool isRange;
        private bool hasLoggedJumpAngle = false;
        private bool isRightDash;



        

        //private bool isJumping = false;



        private bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
                return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
            }
        }


        private void Awake()
        {
       
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
            
        }

        private void Start()
        {
            _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
            
            _hasAnimator = TryGetComponent(out _animator);
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
            _playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

            AssignAnimationIDs();

          
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;
        }

        private void Update()
        {



            _hasAnimator = TryGetComponent(out _animator);

            JumpAndGravity();
            GroundedCheck();
            Move();
            Orball();
            ProjectTile();
            MagicAttack();
            Blocked();
            Mouseleft();
            // DashRight();
           


        }



        private void DashRight()
        {
            float inputAngleRight = Mathf.Atan2(_input.move.x, _input.move.y) * Mathf.Rad2Deg;

            if (DashRightConditionMet() && _input.jump && !hasLoggedJumpAngle)
            {

                _verticalVelocity = Mathf.Sqrt(_JumpDashHeight * -2f * Gravity);


                _animator.SetBool(_animIDashRight, true);
                isRightDash = true;
                _input.jump = false;
                StartCoroutine(Dashed());
       

                hasLoggedJumpAngle = true;
            }
            else if (!_input.jump)
            {
             
                hasLoggedJumpAngle = false;
            }
        }

        private bool DashRightConditionMet()
        {
            float inputAngleRight = Mathf.Atan2(_input.move.x, _input.move.y) * Mathf.Rad2Deg;
            return inputAngleRight == 45f && _input.jump;
        }

        private bool JumpConditionMet()
        {
            float inputAngleJump= Mathf.Atan2(_input.move.x, _input.move.y) * Mathf.Rad2Deg;
            return inputAngleJump == 0;
        }



        private IEnumerator Dashed()
        {
            yield return 

            isRightDash = false;
            _animator.SetBool(_animIDashRight, false);
        }




        private void Mouseleft()
        {
            if (_input.mouseLeft&&!isRange&&Grounded)
            {

                cameraForward = _mainCamera.transform.forward;
                cameraForward.y = 0.0f;


                if (cameraForward != Vector3.zero)
                {
                    transform.forward = cameraForward * Time.deltaTime;
                }


                _animator.SetBool(_animIDOrbball, true);
                isRange = true;
                StartCoroutine(Ranged());
            }
        }

       private IEnumerator Ranged()
        {
            yield return new WaitForSeconds(1f);

            isRange = false;
            _animator.SetBool(_animIDOrbball, false);

        }




        private void Blocked()
        {
            if (_input.Block&&!isBlocked&&Grounded)
            {
                cameraForward = _mainCamera.transform.forward;
                cameraForward.y = 0.0f;


                if (cameraForward != Vector3.zero)
                {
                    transform.forward = cameraForward * Time.deltaTime;
                }


                _animator.SetBool("Block", _input.Block);

            }
            else
            {
                _animator.SetBool("Block", false);
            }
           
        }







        private void MagicAttack()
        {
            if (_input.fire && !isMagicAttack && Grounded)
            {
               
                cameraForward = _mainCamera.transform.forward;
                cameraForward.y = 0.0f; 

              
                if (cameraForward != Vector3.zero)
                {
                    transform.forward = cameraForward*Time.deltaTime;
                }

                
                _animator.SetBool(_animIDMagicAttack, true);
                isMagicAttack = true;

           
                StartCoroutine(ResetMagicAttack());
            }
        }





        private void ProjectTile()
        {
            if (_input.Project_Tile&&!isProjectTileAttack&&Grounded)
            {

                cameraForward = _mainCamera.transform.forward;
                cameraForward.y = 0.0f;


                if (cameraForward != Vector3.zero)
                {
                    transform.forward = cameraForward * Time.deltaTime;
                }


                _animator.SetBool(_animIDProjectTile, true);
                isProjectTileAttack = true;
                StartCoroutine(ResetProjectTile());
            }
        }

        private void Orball()
        {
            if (_input.Orbball && !isAttacking&&Grounded)
            {


                cameraForward = _mainCamera.transform.forward;
                cameraForward.y = 0.0f;


                if (cameraForward != Vector3.zero)
                {
                    transform.forward = cameraForward * Time.deltaTime;
                }

                _animator.SetBool(_animIDRange, true);
                isAttacking = true;               
                
                StartCoroutine(ResetAttackFlag());
            }
        }





        private IEnumerator ResetMagicAttack()
        {
            yield return new  WaitForSeconds(1f);

            isMagicAttack = false;
            _animator.SetBool(_animIDMagicAttack, false);
        }


        private IEnumerator ResetAttackFlag()
        {
           
            yield return new WaitForSeconds(1.8f);

           
           
            isAttacking = false;
         
            _animator.SetBool(_animIDRange, false);
            
        }

        private IEnumerator ResetProjectTile()
        {
            yield return new WaitForSeconds(1.6f);

            isProjectTileAttack = false;
            _animator.SetBool(_animIDProjectTile, false);


        }

        private IEnumerator ResetBlock()
        {
            yield return new WaitForSeconds(1.5f);
            isBlocked = false;
            _animator.SetBool(_animIDBlock, false);

        }



        private void LateUpdate()
        {
            CameraRotation();
        }

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
            _animIDOrbball = Animator.StringToHash("AttackOrbball");
            _animIDProjectTile = Animator.StringToHash("AttackProjectTile");
            _animIDMagicAttack = Animator.StringToHash("MagicAttack");
            _animIDBlock = Animator.StringToHash("Block");
            _animIDRange = Animator.StringToHash("RangeAttack");
            _animIDashRight = Animator.StringToHash("DashRight");

        }

        private void GroundedCheck()
        {
       
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
                transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);

          
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDGrounded, Grounded);
            }
        }

        private void CameraRotation()
        {

            if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
            
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
            }

          
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

      
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
                _cinemachineTargetYaw, 0.0f);
        }

        private void Move()
        {

            if (_animator.GetBool(_animIDOrbball)||_animator.GetBool(_animIDProjectTile)||_animator.GetBool(_animIDMagicAttack)||_animator.GetBool(_animIDBlock)||_animator.GetBool(_animIDRange))
            {
            
                _speed = 0.0f;
                _animationBlend = 0.0f;
            }
            else
            {

               
                targetspeed = _input.sprint ? SprintSpeed : MoveSpeed;


              
                if (_input.move == Vector2.zero) targetspeed = 0.0f;

            
                float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

                float speedOffset = 0.1f;
                float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;


                // değişiklik
                


                if (currentHorizontalSpeed < targetspeed - speedOffset ||
                    currentHorizontalSpeed > targetspeed + speedOffset)
                {
            
                    _speed = Mathf.Lerp(currentHorizontalSpeed, targetspeed * inputMagnitude,
                        Time.deltaTime * SpeedChangeRate);

                    
                    _speed = Mathf.Round(_speed * 1000f) / 1000f;
                }
                else
                {
                    _speed = targetspeed;
                }

                _animationBlend = Mathf.Lerp(_animationBlend, targetspeed, Time.deltaTime * SpeedChangeRate);
                if (_animationBlend < 0.01f) _animationBlend = 0f;

               
                Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

                
                if (_input.move != Vector2.zero)
                {
                    _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                      _mainCamera.transform.eulerAngles.y;
                    float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                        RotationSmoothTime);

             
                    transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
                }


                Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

              
                _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                                 new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

              
                if (_hasAnimator)
                {
                    _animator.SetFloat(_animIDSpeed, _animationBlend);
                    _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
                }

               
                
            }

           
        }

        private void JumpAndGravity()
        {
            if (Grounded)
            {
              
                _fallTimeoutDelta = FallTimeout;

               
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDJump, false);
                    _animator.SetBool(_animIDFreeFall, false);
                }

              
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                // Jump
                if (_input.jump && _jumpTimeoutDelta <= 0.0f /*&& JumpConditionMet()*/)
                {
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDJump, true);
                    }

                  
                    hasLoggedJumpAngle = false;
                }


                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {

                _jumpTimeoutDelta = JumpTimeout;

   
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                  
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDFreeFall, true);
                    }
                }

           
                _input.jump = false;
            }

           
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }

      

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

         
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
                GroundedRadius);
        }

        


        
        
    }
}