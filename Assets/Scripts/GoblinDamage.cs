using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;


public class GoblinDamage : MonoBehaviour
{
    public ThirdPersonController controller;
    public PlayerHealt healt;
    public bool isdamaged;

    
    private void GoblinDamageEvent()
    {
        if (healt.isdamage)
        {
            healt.PlayerHealthValue -= 0.1f;
            healt.PlayerHealthImage.fillAmount = healt.PlayerHealthValue;
            healt.vinyetbool = true;

        }





    }
    

  

}
