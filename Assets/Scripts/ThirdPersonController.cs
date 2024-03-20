using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif


namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class ThirdPersonController : MonoBehaviour
    {
        [Header("Player")]
        [Tooltip("Hareket hızı")]
        public float MoveSpeed = 2.0f;

        [Tooltip("Sprint")]
        public float SprintSpeed = 5.335f;

        [Tooltip("player rotation speed")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Tooltip("Acceleration")]
        public float SpeedChangeRate = 10.0f;

        [Space(10)]
        [Tooltip(" jump height")]
        public float JumpHeight = 1.2f;

        [Tooltip(" gravity value")]
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

        [Tooltip("What layers the character uses as ground")]//LAYERMASK!!
        public LayerMask GroundLayers;

        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;

        [Tooltip("Additional degress to override the camera")]
        public float CameraAngleOverride = 0.0f;

        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;

        [Header("VFX")]
        public GameObject vfxPrefab;
        public Transform ProjectTilePoint;

        // cinemachine
        [Header("Camera")]
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        [Header("Speed Wind")]
        public ParticleSystem wind;
        private bool isWindSpeed = false;



        // player
        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        public float _verticalVelocity;
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
        private int _animIDdie;
        private int _animIDHorizpntalDamage;




        private int TabCount;



        public float targetspeed;


        public Vector3 cameraForward;



        public Transform AimTransform;
        public Transform maincameratransform;
        public Transform playerCameraRoot;
        public Transform cmfreelook;


        [Header("Shield")]
        Shield shield;



#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        private PlayerInput _playerInput;
#endif
        private Animator _animator;
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;
        private ProjectTile tile;
        ButterflyController butterFly;
        ShieldTime shieldforShield;



        private const float _threshold = 0.01f;

        private bool _hasAnimator;
        private bool isAttacking;
        private bool isProjectTileAttack;
        private bool isMagicAttack;
        private bool isBlocked;
        private bool isRange;
        private bool hasLoggedJumpAngle = false;
        private bool isRightDash;
        private bool isSpawningVFX = false;
        public bool AimChance;
        public GameObject spineAiming;
        public bool VfxEnding = false;
        public bool isTabing;
        public bool isdie = true;
        private bool uzaklastirSet = false;


        public PlayerHealt healt;

        public NavMeshControl nav;
        public NavMeshControlTwo navtwo;
        public NavMeshControlthree navthree;
        public NavMeshControlFour navFour;
        public NavMeshControlFive navFive;


        public bool isDamaged;


        //stamina
        public Stamina stamina;


        //Camera Shake
        public CameraShake shake;


        //private bool isJumping = false;

        public Transform PlayerTransform;
        public GoblinAttackOneAnimationEvent @event;
        public ButterflyControlerNEW butterflyController;
        public ButterFlyAttack butterFlyAttack;
        public QSpeel speel;
        public Intractions intactfirst;
        public ChestTwo chestTwo;
        public EButtonEffect ESpell;


        //FootstepControl

        [SerializeField] private ParticleSystem FootVfxLeft;
        [SerializeField] private ParticleSystem FootVfxRight;
        public Chest chest;

        public lightControl light;
        [SerializeField] private AudioSource LightSound;



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


            butterFly = GetComponent<ButterflyController>();
            shield = GetComponent<Shield>();
            @event = FindObjectOfType<GoblinAttackOneAnimationEvent>();



        }


        private void Start()
        {



            if (nav == null)
            {
                Debug.Log("navigation script bulunamadı");
            }


            _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;

            shield = FindObjectOfType<Shield>();
            shieldforShield = FindObjectOfType<ShieldTime>();



            if (shield != null)
            {

                shield.shieldInstance = GetComponent<ParticleSystem>();
            }
            else
            {

                Debug.LogError("Shield yok!");
            }
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
            //MagicAttack();
            Blocked();
            Mouseleft();
            ShieldOrButterfly();
            WindSpeed();
            Die();
            Intract();
            StaminaControl();
            shakeInput();
            LightController();
            if (light.azalıyor)
            {
                StartCoroutine(waitLightCam());
            }

        }


        #region Intaction
        private void Intract()
        {
            if (_input.Intraction)
            {
                chest.intract = true;
             

            }

            if (_input.Intraction && chestTwo.isChestTwo)
            {
                chestTwo.Intract = true;

            }



            _input.Intraction = false;
        }

        #endregion



        //Die
        #region Die
        private bool isDead = false;
        [SerializeField]
        private float yourRadius;
        [SerializeField]
        private float yourForce;

        private void Die()
        {
            if (healt.die && !isDead)
            {
                _animator.SetTrigger("Die 0");
                isDead = true;
            }
        }


        #endregion



        //LightController

        IEnumerator waitLightCam()
        {
            yield return new WaitForSeconds(1.5f);

            shake.ShakeCamera(2f,5);
        }



        private void LightController()
        {
            if (_input.Project_Tile)
            {
                Debug.Log("IŞIK AZALIYOR");
                light.azalıyor = true;


                StartCoroutine(waitvoice());
                StartCoroutine(waitUp());
            }
          

            IEnumerator waitUp()
            {
                yield return new WaitForSeconds(2);
                light.artıyor = true;
                light.azalıyor = false;
                StartCoroutine(waitdown());
                _input.Project_Tile = false;
            }

            IEnumerator waitdown()
            {
                yield return new WaitForSeconds(2);
                light.artıyor = false;

            }

            IEnumerator waitvoice()
            {
                yield return new WaitForSeconds(0.35f);
                LightSound.Play();
            }


        }




        //Tab kontroller
        #region Shield Or Butterfly 




        private void ShieldOrButterfly()
        {
            if (_input.Tab)
            {
                _input.Tab = !_input.Tab;
                Debug.Log("bas");
                TabCount += 1;
                TabCountCalculate();
                Debug.Log(isTabing);
            }
        }


        private void TabCountCalculate()
        {
            if (TabCount % 2 == 0)
            {
                isTabing = true;
                butterflyController.isRightClicked = false;
            }
            else if (TabCount % 2 == 1)
            {

                butterflyController.isRightClicked = true;
                isTabing = false;
            }

        }


        #endregion




        //Mouse Left
        #region MOUSELEFT AND PROJECT TİLE


        //iki tane aşama olacak
        //1->mouseleft
        //2->Açı hesaplatma 





        private void Mouseleft()
        {
            if (_input.mouseLeft && !isRange && Grounded && !isDead && isTabing)
            {
                cameraForward = _mainCamera.transform.forward;
                cameraForward.y = 0.0f;



                if (cameraForward != Vector3.zero)
                {

                    transform.forward = cameraForward * Time.deltaTime;
                    if (DashRightConditionMet())
                    {
                        
                        _animator.SetFloat(_animIDSpeed, 400);
                    }
                    else if (DashLeftBackConditionMet()) // Sol arkaya doğru hareket koşulunu ilk kontrol edelim
                    {
                       
                        _animator.SetFloat(_animIDSpeed, 500);
                    }
                    else if (DashRightBackConditionMet())
                    {
                       
                        _animator.SetFloat(_animIDSpeed, 700);
                    }
                    else if (DashLeftConditionMet())
                    {
                       
                        _animator.SetFloat(_animIDSpeed, 300);
                    }
                    else if (DashBackConditionMet())
                    {
                        
                        _animator.SetFloat(_animIDSpeed, 600);
                    }

                    if (DashRightConditionMet() || DashLeftConditionMet() || DashBackConditionMet() || DashLeftBackConditionMet() || DashRightBackConditionMet())
                    {
                        SprintSpeed = 7;
                        _speed = 2;
                    }
                    else
                    {
                        SprintSpeed = 7;
                        _speed = 4;
                    }


                }
                // playerCameraRoot.transform.position = AimTransform.transform.position;

                _animator.SetBool("AttackOrbball", _input.mouseLeft);




            }
            else
            {



                _animator.SetBool("AttackOrbball", false);
                // playerCameraRoot.transform.position = cmfreelook.transform.position;
            }
        }

        private IEnumerator WaitForMouseRelease()
        {
            yield return new WaitUntil(() => !_input.mouseLeft);
            isSpawningVFX = false;
        }


        private IEnumerator StopSpawnVFXAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            isSpawningVFX = false;
        }



        private IEnumerator SpawnVFXRepeatedly()
        {
            if (!isSpawningVFX)
            {
                isSpawningVFX = true;

                while (true)
                {
                    InstantiateVfx();
                    yield return new WaitForSeconds(1.0f);
                }
            }
        }


        private IEnumerator VFXTimeforMOUSE()
        {
            yield return new WaitForSeconds(0.7f);
            InstantiateVfx();
        }



        private IEnumerator Ranged()
        {



            yield return new WaitForSeconds(1f);

            isRange = false;

            _animator.SetBool(_animIDOrbball, false);

        }





        #endregion


        #region Wind

        private void WindSpeed()
        {
            if (_input.sprint && !wind.isPlaying&&stamina.Staminabool&&_input.move!=Vector2.zero)
            {
                wind.Play();
            }
            else if ((!_input.sprint && wind.isPlaying || DashBackConditionMet() || DashLeftBackDiagonalConditionMet() || DashLeftConditionMet() || DashRightConditionMet())||!stamina.isSprint)
            {
                wind.Stop();
            }
        }

        #endregion


        #region footsttepVFX





        #endregion


        #region Stanina

        private void StaminaControl()
        {
            if (_input.sprint)
            {
                stamina.Staminabool = true;

            }
            else
            {
                stamina.Staminabool = false;
            }
                    
                    


        }


        #endregion





        #region HAREKET
        private void Move()
        {
            if (_animator.GetBool(_animIDProjectTile) || _animator.GetBool(_animIDMagicAttack) || _animator.GetBool(_animIDRange) || isDead)
            {
                _speed = 0.0f;
                _animationBlend = 0.0f;
                shieldforShield.isFilling = true;

            }
            else
            {
                float targetSpeed = 0.0f;

                if (_input.sprint&&stamina.isSprint)
                {



                    FootVfxLeft.gameObject.SetActive(true); FootVfxRight.gameObject.SetActive(true);
                    targetSpeed = SprintSpeed;
                    // Sprint olduğunda _animIDSpeed değerini 120 olarak ayarla
                    if (_input.move != Vector2.zero)
                    {
                        float startSpeed = _animator.GetFloat(_animIDSpeed);
                        float LerptargetSpeed = 200;

                        shieldforShield.isFilling = true;


                        // Lerp oranı
                        float lerpRatio = 0.1f;

                        // Lineer interpolasyon 
                        float newSpeed = Mathf.Lerp(startSpeed, LerptargetSpeed, lerpRatio);
                        _animator.SetFloat(_animIDSpeed, newSpeed);

                    }
                    else if (_input.move == Vector2.zero)
                    {
                        float finishSpeed = _animator.GetFloat(_animIDSpeed);
                        float LerpFinishSprintToIdle = 0;
                        float LerfRadio = 0.1f;

                        float newLerfSpeed = Mathf.Lerp(finishSpeed, LerpFinishSprintToIdle, LerfRadio);

                        _animator.SetFloat(_animIDSpeed, newLerfSpeed);
                    }
                }
                else if (_input.move != Vector2.zero)
                {
                    FootVfxLeft.gameObject.SetActive(false); FootVfxRight.gameObject.SetActive(false);
                    targetSpeed = MoveSpeed;
                    float StartSpeedWalk = _animator.GetFloat(_animIDSpeed);
                    float LerpTargetSpeedWalk = 100;
                    shieldforShield.isFilling = true;




                    float lerpRatioWalk = 0.1f;

                    float newspeed = Mathf.Lerp(StartSpeedWalk, LerpTargetSpeedWalk, lerpRatioWalk);
                    // Sprint olmadığında _animIDSpeed değerini normal hızda ayarla
                    _animator.SetFloat(_animIDSpeed, newspeed);
                }
                else if (_input.move == Vector2.zero)
                {
                    float finishSpeed = _animator.GetFloat(_animIDSpeed);
                    float LerpFinishSprintToIdle = 0;
                    float LerfRadio = 0.1f;

                    float newLerfSpeed = Mathf.Lerp(finishSpeed, LerpFinishSprintToIdle, LerfRadio);

                    _animator.SetFloat(_animIDSpeed, newLerfSpeed);
                    shieldforShield.isFilling = true;
                }

                if (_input.move == Vector2.zero)
                {
                    targetSpeed = 0.0f;
                    shieldforShield.isFilling = true;
                }

                float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;
                float speedOffset = 0.1f;
                float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

                if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
                {
                    _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);
                    _speed = Mathf.Round(_speed * 1000f) / 1000f;
                }
                else
                {
                    _speed = targetSpeed;
                }

                _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
                if (_animationBlend < 0.01f)
                {
                    _animationBlend = 0f;
                    shieldforShield.isFilling = true;
                }

                Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

                if (_input.move != Vector2.zero)
                {
                    _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
                    float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);
                    transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
                }

                Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

                _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

                if (!_input.sprint && _hasAnimator)
                {
                    // _animator.SetFloat(_animIDSpeed, _animationBlend);
                    _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
                }

                float animationSpeed = 1.0f; // Normal hız

                // Son 21 ile son 2 
                float normalizedTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                if (normalizedTime > 0.81f && normalizedTime < 0.98f) // Son 21 ile son 2 
                {
                    animationSpeed = Mathf.Lerp(1.0f, 0.5f, (normalizedTime - 0.81f) / (0.98f - 0.81f)); // Normal hızdan 21 
                }


                // Animasyon hızı
                _animator.speed = animationSpeed;
            }
        }


        #endregion

        //Space
        #region ZIPLAMA

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

                    FootVfxLeft.gameObject.SetActive(false); FootVfxRight.gameObject.SetActive(false);
                    if (_hasAnimator)
                    {

                        _animator.SetBool(_animIDJump, true);
                    }


                    hasLoggedJumpAngle = false;
                }


                if (_jumpTimeoutDelta >= 0.0f)
                {
                    FootVfxLeft.gameObject.SetActive(false); FootVfxRight.gameObject.SetActive(false);
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
                        FootVfxLeft.gameObject.SetActive(false); FootVfxRight.gameObject.SetActive(false);
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

        #endregion


     

        //Sağ Tık
        #region BLOKLAMA



        private void Blocked()
        {
            if (_input.Block && Grounded && !isDead)
            {

                healt.isdamage = false;
                cameraForward = _mainCamera.transform.forward;
                cameraForward.y = 0.0f;
                _animator.SetBool("Block", true);


                


                //Shield
                if (isTabing && shieldforShield.isFilling)
                {


                    @event.ishield = false;
                    if (shieldforShield.isFilling)
                    {


                        shield.ShieldInstantiate();
                        navFive.uzaklastirfive = true;
                        navFour.uzaklasyirFour = true;
                        navthree.uzaklasyirthree = true;
                        navtwo.uzaklasyirtwo = true;
                        nav.uzaklastir = true;
                        VfxEnding = false;
                        shieldforShield.isFilling = false;
                        shieldforShield.fillImage.fillAmount -= shieldforShield.decreaseSpeed * Time.deltaTime;

                        if (!uzaklastirSet)
                        {
                          
                            uzaklastirSet = true;
                        }
                    }
                    if (shieldforShield.fillImage.fillAmount < 0.01)
                    {
                        shield.ShieldDestroy();
                        _animator.SetBool("Block", false);
                    }
                }
                else if (!isTabing)
                {

                    butterflyController.isRightClicked = true;

                    navFive.uzaklastirfive = false;
                    navFour.uzaklasyirFour = false;
                    navtwo.uzaklasyirtwo = false;
                    nav.uzaklastir = false;
                    uzaklastirSet = false;
                    navthree.uzaklasyirthree = false;
                }

                // shieldforShield.shieldTimer += Time.deltaTime / shieldforShield.totalTime;

            }
            else
            {
                @event.ishield = true;
                VfxEnding = true;
                _animator.SetBool("Block", false);
                StartCoroutine(ResetShield());

                navFive.uzaklastirfive = false;
                navFour.uzaklasyirFour = false;
                navtwo.uzaklasyirtwo = false;
                nav.uzaklastir = false;
                uzaklastirSet = false;
                navthree.uzaklasyirthree = false;

                healt.isdamage = true;


            }

        }




        IEnumerator ResetShield()
        {
           
            yield return new WaitForSeconds(0.01f);
            isBlocked = false;
            shield.ShieldDestroy();
        }


        private IEnumerator ResetBlock()
        {
            yield return new WaitForSeconds(1.5f);
          
        
            _animator.SetBool(_animIDBlock, false);

        }

        #endregion


        //aim sistemini değiştir

        private void AimChange()
        {

            float inputAngleLeft = Mathf.Atan2(_input.move.x, _input.move.y) * Mathf.Rad2Deg;

        }

        public bool AimLeftConditionmet()
        {
            float inputAngleLeft = Mathf.Atan2(_input.move.x, _input.move.y) * Mathf.Rad2Deg;

            return inputAngleLeft > -50 && inputAngleLeft < -40;


        }



        //Dash
        #region Dash Sistemi

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

        private bool JumpConditionMet()
        {
            float inputAngleJump = Mathf.Atan2(_input.move.x, _input.move.y) * Mathf.Rad2Deg;
            return inputAngleJump == 0;
        }


        private IEnumerator Dashed()
        {
            yield return

            isRightDash = false;
            _animator.SetBool(_animIDashRight, false);
        }


        #endregion


        //yazdığım 2D freeform directional animator
        #region Manuel olarak yapılmış 2D freeform directional
        private bool DashBackConditionMet()
        {
            Vector2 reverseDirection = -_input.move; // Yön vektörünün tersi
            float inputAngleBack = Mathf.Atan2(reverseDirection.x, reverseDirection.y) * Mathf.Rad2Deg;
            return Mathf.Abs(inputAngleBack) <= 45f || Mathf.Abs(inputAngleBack) >= 315f;
        }

        private bool DashRightConditionMet()
        {
            float inputAngleRight = Mathf.Atan2(_input.move.x, _input.move.y) * Mathf.Rad2Deg;
            return inputAngleRight >= 45f && inputAngleRight <= 90f;
        }

        private bool DashLeftConditionMet()
        {
            float inputAngleLeft = Mathf.Atan2(_input.move.x, _input.move.y) * Mathf.Rad2Deg;
            return (inputAngleLeft >= -90f && inputAngleLeft <= -45f) || (inputAngleLeft >= 270f && inputAngleLeft <= 315f);
        }

        private bool DashLeftBackConditionMet()
        {
            float inputAngleLeftBack = Mathf.Atan2(_input.move.x, _input.move.y) * Mathf.Rad2Deg;
            return (inputAngleLeftBack >= -135f && inputAngleLeftBack <= -90f);
        }

        private bool DashRightBackConditionMet()
        {
            float inputAngleRightBack = Mathf.Atan2(_input.move.x, _input.move.y) * Mathf.Rad2Deg;
            return (inputAngleRightBack >= 90f && inputAngleRightBack <= 135f);
        }






        private bool DashLeftBackDiagonalConditionMet()
        {
            Vector2 reverseDirection = -_input.move; // Yön vektörünün tersi
            float inputAngleBack = Mathf.Atan2(reverseDirection.x, reverseDirection.y) * Mathf.Rad2Deg;

            // Negatif açıyı pozitif bir açıya dönüştürme
            if (inputAngleBack < 0)
            {
                inputAngleBack += 360f;
            }

            // -45 ve -135 derece aralığını kontrol etme
            return inputAngleBack >= 315f && inputAngleBack <= 225f;
        }





        #endregion


        //F
        #region MagicAttack

        private void MagicAttack()
        {
            if (_input.fire && !isMagicAttack && Grounded)
            {

                cameraForward = _mainCamera.transform.forward;
                cameraForward.y = 0.0f;


                if (cameraForward != Vector3.zero)
                {
                    transform.forward = cameraForward * Time.deltaTime;
                }

                Debug.Log("F");
                _animator.SetBool(_animIDMagicAttack, true);
                isMagicAttack = true;


                StartCoroutine(ResetMagicAttack());
            }
        }
        private IEnumerator ResetMagicAttack()
        {
            yield return new WaitForSeconds(1.5f);

            isMagicAttack = false;
            _animator.SetBool(_animIDMagicAttack, false);
        }


        #endregion

        //E
        #region ProjectTile
        private void ProjectTile()
        {
            if (_input.Project_Tile && !isProjectTileAttack && Grounded)
            {

                cameraForward = _mainCamera.transform.forward;
                cameraForward.y = 0.0f;
                ESpell.InstantiateESpell();

                if (cameraForward != Vector3.zero)
                {
                    transform.forward = cameraForward * Time.deltaTime;
                }
                Debug.Log("E");

                _animator.SetBool(_animIDProjectTile, true);
                isProjectTileAttack = true;
                StartCoroutine(ResetProjectTile());
                
            }
        }


        private IEnumerator ResetProjectTile()
        {
            yield return new WaitForSeconds(2f);

            isProjectTileAttack = false;
            _animator.SetBool(_animIDProjectTile, false);
            _input.Project_Tile = false;
 
        }

        IEnumerator waitqEspell()
        {
            yield return new WaitForSeconds(5f);
            ESpell.destroyspeelE();
            
        }
        #endregion

        //Q
        #region Orbball


        private void Orball()
        {
            if (_input.Orbball && !isAttacking && Grounded)
            {
              

                speel.InstantiateQSpell();
                cameraForward = _mainCamera.transform.forward;
                cameraForward.y = 0.0f;


                if (cameraForward != Vector3.zero)
                {
                    transform.forward = cameraForward * Time.deltaTime;
                }
                Debug.Log("Q");

                _animator.SetBool(_animIDRange, true);
                isAttacking = true;

                StartCoroutine(ResetAttackFlag());
            }
        }
        private IEnumerator ResetAttackFlag()
        {

            yield return new WaitForSeconds(1.25f);


            isAttacking = false;

            _animator.SetBool(_animIDRange, false);

        }

        IEnumerator waitqspell()
        {
            yield return new WaitForSeconds(5f);
            speel.destroyspeelq();

        }



        #endregion

        //Camera Shake

        private void shakeInput()
        {
            if (_input.Orbball)
            {
                StartCoroutine(waitq());
            }
            else if (_input.Orbball == false)
            {
                shake.ShakeCamera(0, 1);
            }

        }


        IEnumerator waitq()
        {
            yield return new WaitForSeconds(0.7f);
            shake.ShakeCamera(2.5f, 2);
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
            _animIDdie = Animator.StringToHash("Die");

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





        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }



        private void InstantiateVfx()
        {
            if (vfxPrefab != null)
            {
                Vector3 fireRot = PlayerTransform.forward;
                GameObject vfxInstance = Instantiate(vfxPrefab, ProjectTilePoint.position, Quaternion.LookRotation(fireRot));
                Destroy(vfxInstance, 2.0f);
            }
            else
            {
                Debug.LogWarning("VFX Prefab not assigned in the Unity Editor!");
            }
        }

    }
}