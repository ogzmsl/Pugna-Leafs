using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    OyuncuKontrolleri playercontrols; //input aksiyonlarý için deðiþken tanýmladým
    animatorManager animatormanager;
    public Vector2 MoveInput; //kullanýcý giriþine göre harekt vector'ü tanýmladým
    public float MoveAmount;


    public float verticalInput; //W-S giriþleri
    public float HorizontalInput;//A-D giriþleri
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
            playercontrols = new OyuncuKontrolleri(); //yeni bir oyuncu kontrolü oluþturdum eðer henüz oluþturulmamýþ ise
            playercontrols.OyuncuHareketleri.Hareket.performed += i => MoveInput = i.ReadValue<Vector2>(); //tetikleyici
            playercontrols.OyuncuAksiyonlarý.B.performed += i => b_input = true;
            playercontrols.OyuncuAksiyonlarý.B.canceled += i => b_input = false;


            playercontrols.OyuncuAksiyonlarý.Jump.performed += i => jumpInput = true;
            playercontrols.OyuncuAksiyonlarý.Jump.canceled += i => jumpInput = false;

        }
        playercontrols.Enable(); //aktiflik saðladým(kullanýlabilir)
    }

    private void OnDisable()
    {
        playercontrols.Disable(); //bu scripts devre dýþý býrakr
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
