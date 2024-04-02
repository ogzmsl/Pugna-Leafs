using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{

    public Image StaminaImage;

    public bool Staminabool;

    public bool isSprint;



   
    void FixedUpdate()
    {
        if (Staminabool)
        {
            StaminaImage.fillAmount -= Time.fixedDeltaTime/50;
           
        }
        else if (!Staminabool&&StaminaImage.fillAmount<1)
        {
            StaminaImage.fillAmount += Time.fixedDeltaTime / 10;
            
        }



        if (StaminaImage.fillAmount <= 0.01)
        {
            isSprint = false;
        }
        else if(StaminaImage.fillAmount >= 0.01f)
        {
            isSprint = true;
        }

    }
}
