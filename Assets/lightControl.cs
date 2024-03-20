using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightControl : MonoBehaviour
{

    public Light light;

    public bool artýyor=false;
    public bool azalýyor = false;

    public float artýsOrani = 0.4f;
   

    void Update()
    {
        if (azalýyor)
        {
            light.intensity -= artýsOrani * Time.deltaTime;
           
        }
        if (artýyor)
        {
            light.intensity += artýsOrani * Time.deltaTime;
        }

    }
}
