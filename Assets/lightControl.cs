using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightControl : MonoBehaviour
{

    public Light light;

    public bool art�yor=false;
    public bool azal�yor = false;

    public float art�sOrani = 0.4f;
   

    void Update()
    {
        if (azal�yor)
        {
            light.intensity -= art�sOrani * Time.deltaTime;
           
        }
        if (art�yor)
        {
            light.intensity += art�sOrani * Time.deltaTime;
        }

    }
}
