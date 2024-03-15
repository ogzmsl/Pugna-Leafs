using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformKontrol : MonoBehaviour
{
    public Transform character; // Karakterin Transform 
    public float tiltSpeed = 5f;
    public float characterHeightThreshold = 1.0f;
    public Animator RockAnimator;

    void Update()
    {
        if (character.position.y > transform.position.y + characterHeightThreshold)
        {
            Vector3 moveDirection = character.position - transform.position;
            moveDirection.y = 0f;
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, tiltSpeed * Time.deltaTime);
            RockAnimator.SetBool("SecondRock", true);
        }
        else
        {
            RockAnimator.SetBool("SecondRock", false);
        }
    }
}
