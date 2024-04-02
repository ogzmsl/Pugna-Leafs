using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource leftFootGrass;
    [SerializeField] private AudioSource rightFootGrass;
    [SerializeField] private AudioSource leftFootStone;
    [SerializeField] private AudioSource rightFootStone;
    [SerializeField] private AudioSource jumpFoot;

    private bool hasPlayedStepSound = false; 

    public LayerMask stone, grass;

    public void RightStepSounds()
    {
        if (!hasPlayedStepSound)
        {
            leftFootGrass.Play();
            hasPlayedStepSound = true; 
        }
    }

    public void LeftStepSounds()
    {
        if (!hasPlayedStepSound)
        {
            rightFootGrass.Play();
            hasPlayedStepSound = true; 
        }
    }

    public void JumpSounds()
    {
        jumpFoot.Play();
        hasPlayedStepSound = false; 
    }
}
