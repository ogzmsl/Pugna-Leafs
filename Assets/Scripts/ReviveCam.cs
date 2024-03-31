using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ReviveCam : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    public PlayerBirth birth;
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        virtualCamera.Priority = 9;
    }

    private void Update()
    {
        ReviveCamEffect();
    }

    private void ReviveCamEffect()
    {
        if (birth.CamWait)
        {
            virtualCamera.Priority = 11;
        }

        if (!birth.CamWait) {
            virtualCamera.Priority = 9;
            }


    }

 


}
