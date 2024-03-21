using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class lightControl : MonoBehaviour
{
    public Light light;

    public bool azal�yor;

    public float art�sOrani = 0.4f;
    private float azalmaS�resi = 1f; // Azalma devam etme s�resi

    private float azalmaZamanlay�c� = 0f; // Azalma zamanlay�c�

    public AudioSource audioLight;

    public EButtonEffect effect;

    void Update()
    {
        if (azal�yor)
        {
           
            light.intensity -= art�sOrani * Time.deltaTime;
            azalmaZamanlay�c� += Time.deltaTime; 
            if (azalmaZamanlay�c� >= azalmaS�resi) 
            {
                azalmaZamanlay�c� = 0f; 
                azal�yor = false; 
            }
        }
        else
        {
            if (light.intensity < 1.7f)
            {
                light.intensity += art�sOrani * Time.deltaTime;
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
