using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
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

        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

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

        //efekler için değişkenler

        public GameObject firePoint;
        public List<GameObject> vfx = new List<GameObject>();
        public GameObject effectToSpawn;
        public float effectTimePlayAfter;



        //player için değişkenler
        public GameObject MyPlayer;

        // cinemachine
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;




        //UI İÇİN GİRİŞLER
        public GameObject UI;
        public GameObject UIforSettings;
        private bool UIActive=false;
        public Button Resume;
        public Button Quit;
        public Button Settings;
        public Button BackSetting;
        public GameObject settingsMenufirst;
        public GameObject Menufirst;
        public GameObject settingtoMenu;
        public GameObject CombatUI;
        public GameObject firstCombatUI;
        private bool isActiveFbutton=true;


        //skill için girişler
        public Button fButton;
        public Button qButton;
        public Button ebutton;


        // player
        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;



        //benim yazdığım kısım orbball
        private int _animIDOrbball;
        private bool hasExecuted = false;

        public float visibleenemydegree = 45f;

        //benim yazdığım kısım projecttile;
        public int _animIDOProjecttile;


        //benim yazdığım kısım enemy detected
        public Transform enemyTarnsform;
        public float targetDistance = 5f;
        public Transform cameraTransform;



        //benim yazdığım kısım bullet time
        public float slowdownfactor = 0.05f;
        public float slowdowmLenght = 2f;








        


#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        private PlayerInput _playerInput;
#endif
        private Animator _animator;
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;

        private const float _threshold = 0.01f;

        private bool _hasAnimator;



        //bullet time için fonksiyon
        private void SlowMotion()
        {
            Time.timeScale = slowdownfactor;
             
        }

                    

        



        public bool IsCurrentDeviceMouse
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


        public void Awake()
        {
            // get a reference to our main camera
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
        }

        private void Start()
        {
            //UI için Listenerlar
            Resume.onClick.AddListener(resumeButton);
            Quit.onClick.AddListener(Quiting);
            Settings.onClick.AddListener(SettingsButton);
            BackSetting.onClick.AddListener(backSettingButton);
            fButton.onClick.AddListener(FskillsFunction);
            EventSystem.current.SetSelectedGameObject(firstCombatUI);

            CombatUI.SetActive(false);
            


            //benim yazdığım kısım skiller için

            effectToSpawn = vfx[0];


            UI.SetActive(false);

            UIforSettings.SetActive(false); 


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

            // reset our timeouts on start
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;
            


        }

        //UI dinleme fonksiyonları
        public void resumeButton()
        {

            UI.SetActive(false);
            Time.timeScale = 1;

        }




        private void Update()
        {

           
           

            if (_input.fire)
            {
                _animator.SetBool("projecttile", true);
                
                Debug.Log("project tile");
            }
            else if(!_input.fire)
            {
                _animator.SetBool("projecttile", false);
                _input.fire = false;
            }


            _hasAnimator = TryGetComponent(out _animator);

            JumpAndGravity();
            GroundedCheck();
            Move();
          

            //benim entegre ettiğim skill ve combat  sisteni


            if (enemyTarnsform != null && Vector3.Distance(transform.position, enemyTarnsform.position) < targetDistance)
            {
                _animator.SetBool("combat_state", true);
                if (GorunmeAlaniIcerisindeMi(enemyTarnsform.position, cameraTransform.position, cameraTransform.forward, visibleenemydegree)&&isActiveFbutton)
                {
                    
                    _animator.SetBool("SlowMotionAndCombatUI", true);
                    FskillsFunctionON();
              
                    

                    UIActive = true;
                    Invoke("SlowMotion", 0.5f);
                        

                }
                

            }
            else
            {
                Time.timeScale = 1;
                _animator.SetBool("combat_state", false);
            }



            effectTimePlayAfter -= Time.deltaTime;
         

           
            // orball

            if (_input.Orbball && !hasExecuted&& effectTimePlayAfter <= 0)
            {

                _animator.SetBool("Orbball", true);
                spawnVFX();


                Debug.Log("Orbball");
                hasExecuted = true;
            }
            else if (!_input.Orbball)
            {
                _animator.SetBool("Orbball", false);
                hasExecuted = false;
                _input.Orbball = false;
            }


            if (_input.menu)
            {
                Paused();
                UIActive = true;
                Time.timeScale = 0;
                                                               
            }
         
        
            
        }




        public void backSettingButton()
        {
            UIforSettings.SetActive(false);
            UI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(settingtoMenu);
        }



        public void SettingsButton()
        {
            UIforSettings.SetActive(true);
            UI.SetActive(false);
            EventSystem.current.SetSelectedGameObject(settingsMenufirst);
            
        }



        public void Quiting()
        {

            Application.Quit();
        }


        public void Paused()
        {
            

            openmainMenu();
        }

      
        public void openmainMenu()
        {
            UI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(Menufirst);
        }

      
        public void FskillsFunctionON()
        {
           
            CombatUI.SetActive(true);
        }

        public void FskillsFunction()
        {
            isActiveFbutton = false;
            CombatUI.SetActive(false);
            _animator.SetBool("SlowMotionAndCombatUI", false);
            
            Debug.Log("F skill");
        }





        //görüş açısı fonksiyonu eğer düşmanı görür ise ve aynı zamanda yakın ise bu fonksiyon true döner
        bool GorunmeAlaniIcerisindeMi(Vector3 hedefPozisyon, Vector3 gozPozisyon, Vector3 gozYonu, float aciSiniri)
        {
            Vector3 hedefVektor = hedefPozisyon - gozPozisyon;
            hedefVektor.Normalize();

            float aci = Vector3.Angle(gozYonu, hedefVektor);

            return aci < aciSiniri * 0.5f;
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
            //benim yazdığım kısım
            _animIDOrbball = Animator.StringToHash("Orbball");
            _animIDOProjecttile = Animator.StringToHash("projecttile");

        }

        private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
                transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);

            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDGrounded, Grounded);
            }
        }

        public void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            // Cinemachine will follow this target
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
                _cinemachineTargetYaw, 0.0f);
        }



        //hareket için güncellenmiş kısım
        public void Move()
        {
           
            float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

           

           
            if (_input.move == Vector2.zero) targetSpeed = 0.0f;

            
            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * SpeedChangeRate);

                
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
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

        //Benim yazdığım kısım Skiller için

        public void JumpAndGravity()
        {
            if (Grounded)
            {
                // reset the fall timeout timer
                _fallTimeoutDelta = FallTimeout;

                // update animator if using character
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDJump, false);
                    _animator.SetBool(_animIDFreeFall, false);

                }

                // stop our velocity dropping infinitely when grounded
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                // Jump
                if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                    // update animator if using character
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDJump, true);
                    }
                }

                // jump timeout
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                // reset the jump timeout timer
                _jumpTimeoutDelta = JumpTimeout;

                // fall timeout
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    // update animator if using character
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDFreeFall, true);
                    }
                }

                // if we are not grounded, do not jump
                _input.jump = false;
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
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

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
                GroundedRadius);
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (FootstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
                }
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
            }
        }



    //vfx spawn noktası
        public void spawnVFX()
        {
           GameObject vfx;
            if (firePoint != null)
            {
                //vfx instantiate oldu
                vfx = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);

                Vector3 playerForward = MyPlayer.transform.forward;

                //oyuncunun baktığı yöne git
                vfx.transform.rotation = Quaternion.LookRotation(playerForward);
                vfx.transform.eulerAngles = new Vector3(0, vfx.transform.eulerAngles.y, 0);
            }
            else
            {
                Debug.Log("ates noktası ekle");
            }          
        }
    

    }
}