using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScripts : MonoBehaviour
{

    public  Button button;

    public void Quit()
    {
        Application.Quit();
    }
    void Start()
    {
        button.onClick.AddListener(Quit);
     
    }


}
