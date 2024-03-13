using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpVfx : MonoBehaviour
{
    //jump effects
    [SerializeField] private ParticleSystem JumpBounce;
    ParticleSystem jumpEffectInstance;

    public void JumoEffect()
    {
        ParticleSystem jumpEffectInstance = Instantiate(JumpBounce, transform.position, Quaternion.Euler(-90, 0, 0));

    }

   
}
