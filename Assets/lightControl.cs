using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class lightControl : MonoBehaviour
{
    public Light light;

    public bool azalıyor;

    public float artısOrani = 0.4f;
    private float azalmaSüresi = 1f; // Azalma devam etme süresi

    private float azalmaZamanlayıcı = 0f; // Azalma zamanlayıcı

    public AudioSource audioLight;

    public EButtonEffect effect;

    void Update()
    {
        if (azalıyor)
        {
           
            light.intensity -= artısOrani * Time.deltaTime;
            azalmaZamanlayıcı += Time.deltaTime; 
            if (azalmaZamanlayıcı >= azalmaSüresi) 
            {
                azalmaZamanlayıcı = 0f; 
                azalıyor = false; 
            }
        }
        else
        {
            if (light.intensity < 1.7f)
            {
                light.intensity += artısOrani * Time.deltaTime;
            }
        }

        if (effect.Lightimage.fillAmount >= 0.98f)
        {
            StartCoroutine(wait());
        }
    }


    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.8f);
        audioLight.Play();
    }
}
