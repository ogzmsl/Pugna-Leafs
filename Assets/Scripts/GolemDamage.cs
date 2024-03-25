using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class GolemDamage : MonoBehaviour
{
    public ThirdPersonController controller;
    public PlayerHealt healt;
    public bool isdamaged;

    public bool PlayerReaction;

    public Transform playerTransform;


    private void Update()
    {
        
    }






    private void GolemDamageMethod()
    {
        if (healt.isdamage)
        {
            healt.PlayerHealthValue -= 0.4f;
            healt.PlayerHealthImage.fillAmount = healt.PlayerHealthValue;
            healt.vinyetbool = true;

            Vector3 playerDirection = playerTransform.position - transform.position;
            playerDirection.y = 0f; 

          
            Vector3 targetPosition = playerTransform.position + playerDirection.normalized * 10f;
            Vector3 smoothedPosition = Vector3.Lerp(playerTransform.position, targetPosition, Time.deltaTime*15f);

            playerTransform.position = smoothedPosition;
        }

        




    }
}
