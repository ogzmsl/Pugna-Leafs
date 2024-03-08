using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class QSpeel : MonoBehaviour
{

    [SerializeField] private ParticleSystem Qspell;
    [SerializeField] private Transform spellSpawnPoint;
    [SerializeField] private LayerMask terrainLayerMask;
    public Transform mainCamera;
    private RaycastHit hitInfo;
    Vector3 fireDirection;

    public void InstantiateQspell()
    {
        StartCoroutine(WaitForAnimations());
   
         
    }

    private void Update()
    {
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo, Mathf.Infinity,terrainLayerMask))
        {
            Debug.Log("Target: " + hitInfo.transform.name);
        }

        fireDirection = hitInfo.point - spellSpawnPoint.position;
        fireDirection.Normalize();
    }


    public void  destroyspeelq()
    {
        Destroy(Qspell);
    }

    private IEnumerator WaitForAnimations()
    {
        yield return new WaitForSeconds(0.5f);
    
     
            Instantiate(Qspell, spellSpawnPoint.position, Quaternion.LookRotation(fireDirection));
            Qspell.Play();

    
    }
}



