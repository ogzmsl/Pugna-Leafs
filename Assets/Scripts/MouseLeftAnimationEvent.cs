using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLeftAnimationEvent : MonoBehaviour
{
    private Camera mainCamera;
    public RaycastHit hitInfo;
    public LayerMask layerMask; 

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
       
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo, Mathf.Infinity))
        {
           // Debug.Log("HEDEF: " + hitInfo.transform.name);
        }
    }
}
