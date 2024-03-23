using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpVfx : MonoBehaviour
{
    
    [SerializeField] private ParticleSystem JumpBounce;
    ParticleSystem jumpEffectInstance;

    public void JumoEffect()
    {
        ParticleSystem jumpEffectInstance = Instantiate(JumpBounce, transform.position, Quaternion.Euler(-90, 0, 0));
        Destroy(jumpEffectInstance, 2f);

    }

   
}
