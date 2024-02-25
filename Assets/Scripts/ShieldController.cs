using UnityEngine;
using System;
using System.Collections;

public class ShieldController : MonoBehaviour
{
    private ParticleSystem particleSystem;


    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        StartCoroutine(WaitAndDestroy(0.33f));
    }

    IEnumerator WaitAndDestroy(float waitTime)
    {
     
        yield return new WaitForSeconds(waitTime);

  
        particleSystem.Play();

       
        yield return new WaitForSeconds(2.0f); 

      
        particleSystem.Stop();
        yield return new WaitForSeconds(particleSystem.main.duration);
        Destroy(gameObject);
    }
}
