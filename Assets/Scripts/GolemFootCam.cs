using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GolemFootCam : MonoBehaviour
{
    public CinemachineVirtualCamera CinemachineVirtualCamera;
    private float ShakeInstentity = 1.2f;
    private float ShakeTime = 0.12f;



    private float timer;

    public bool isShking;

    public CinemachineBasicMultiChannelPerlin perlin;


    private void Start()
    {
        StopShake();
    }



    public void ShakeCameraFoot()
    {
        CinemachineBasicMultiChannelPerlin perlin = CinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = ShakeInstentity;
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
            Invoke("ShakeCamera", 0.24f);
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
