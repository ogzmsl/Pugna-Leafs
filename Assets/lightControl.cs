using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class lightControl : MonoBehaviour
{
    public Light light;

    public bool azalýyor;

    public float artýsOrani = 0.4f;
    private float azalmaSüresi = 1f; // Azalma devam etme süresi

    private float azalmaZamanlayýcý = 0f; // Azalma zamanlayýcý

    public AudioSource audioLight;

    public EButtonEffect effect;

    void Update()
    {
        if (azalýyor)
        {
           
            light.intensity -= artýsOrani * Time.deltaTime;
            azalmaZamanlayýcý += Time.deltaTime; 
            if (azalmaZamanlayýcý >= azalmaSüresi) 
            {
                azalmaZamanlayýcý = 0f; 
                azalýyor = false; 
            }
        }
        else
        {
            if (light.intensity < 1.7f)
            {
                light.intensity += artýsOrani * Time.deltaTime;
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
