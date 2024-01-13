using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManagerForUI : MonoBehaviour
{
    public static InputManagerForUI _instance;

    public bool menuMenuOpenClose { get; private set; }

    private PlayerInput _playerInput;

    private InputAction _menuMenuOpenClose;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        _playerInput = GetComponent<PlayerInput>();
        _menuMenuOpenClose = _playerInput.actions["MenuOpenClose"];



    }

    
    void Update()
    {
        menuMenuOpenClose = _menuMenuOpenClose.WasPressedThisFrame();
        
    }
}
