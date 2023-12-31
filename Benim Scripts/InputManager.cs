using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    OyuncuKontrolleri playercontrols; //input aksiyonları için değişken tanımladım
    animatorManager animatormanager;
    public Vector2 MoveInput; //kullanıcı girişine göre harekt vector'ü tanımladım
    public float MoveAmount;


    public float verticalInput; //W-S girişleri
    public float HorizontalInput;//A-D girişleri
    public bool b_input;
    PlayerLocomotions playerLocomotions;

    public bool jumpInput;
    public bool canJump = true;



    private void Awake()
    {
        animatormanager = GetComponent<animatorManager>();
        playerLocomotions = GetComponent<PlayerLocomotions>();
    }

    private void OnEnable()
    {

        if (playercontrols == null)
        {
            playercontrols = new OyuncuKontrolleri(); //yeni bir oyuncu kontrolü oluşturdum eğer henüz oluşturulmamış ise
            playercontrols.OyuncuHareketleri.Hareket.performed += i => MoveInput = i.ReadValue<Vector2>(); //tetikleyici
            playercontrols.OyuncuAksiyonları.B.performed += i => b_input = true;
            playercontrols.OyuncuAksiyonları.B.canceled += i => b_input = false;


            playercontrols.OyuncuAksiyonları.Jump.performed += i => jumpInput = true;
            playercontrols.OyuncuAksiyonları.Jump.canceled += i => jumpInput = false;

        }
        playercontrols.Enable(); //aktiflik sağladım(kullanılabilir)
    }

    private void OnDisable()
    {
        playercontrols.Disable(); //bu scripts devre dışı bırakr
    }
    public void HandleAllInputs()
    {
        HandleMoveInput();
        HandleSprintingInput();

        
        HandleJumpInput();

    }



    private void HandleMoveInput()
    {
        verticalInput = MoveInput.y;
        HorizontalInput = MoveInput.x;


        MoveAmount = Mathf.Clamp01(Mathf.Abs(HorizontalInput) + Mathf.Abs(verticalInput));
        animatormanager.UptadeAnimatorValues(0, MoveAmount, playerLocomotions.isspriting);
    }
    private void HandleSprintingInput()
    {
        if (b_input && MoveAmount > 0.5f)
        {
            playerLocomotions.isspriting = true;
        }
        else
        {
            playerLocomotions.isspriting = false;
        }
    }


    

    private void HandleJumpInput()
    {
        if (jumpInput)
        {
            canJump = true;
            playerLocomotions.HandleJumping();
           
        }


    }

}
