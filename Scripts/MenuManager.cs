using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _setmainmenuActive;
    private bool isPaused;

    void Start()
    {
        _setmainmenuActive.SetActive(false);
    }
    void Update()
    {
        if (InputManagerForUI._instance.menuMenuOpenClose)
        {
            if (!isPaused)
            {
                Paused();
            }
            else
            {
                UnPaused();
            }
        }
    }

    public void Paused()
    {
        isPaused = true;
        Time.timeScale = 0;
        openMainMenu();
    }

    public void UnPaused()
    {
        isPaused = false;
        Time.timeScale = 1;


        CloseAllMenu();

    }

    public void openMainMenu()
    {
        _setmainmenuActive.SetActive(true);
        
    }
    public void CloseAllMenu()
    {
        _setmainmenuActive.SetActive(false);
    }
}