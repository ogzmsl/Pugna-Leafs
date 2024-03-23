using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamShake : MonoBehaviour
{
    private CinemachineVirtualCamera CinemachineVirtualCamera;
    private float ShakeInstentity = 0.3f;
    private float ShakeTime = 0.08f;



    private float timer;

    public bool isShking;

    private CinemachineBasicMultiChannelPerlin perlin;


    private void Start()
    {
        StopShake();
    }


    private void Awake()
    {
        CinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

    }


    public void ShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin perlin = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain =ShakeInstentity;
        perlin.m_FrequencyGain = 1;
        timer = ShakeTime;
  
    
    }

    void StopShake()
    {
        CinemachineBasicMultiChannelPerlin perlin = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = 0;
        timer = 0;
        perlin.m_FrequencyGain = 0;
        isShking = false;
    }

    private void Update()
    {
        if (isShking)
        {
            Invoke("ShakeCamera", 0.65f);
        }



        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                StopShake();
               
            }
        }
    }


}
