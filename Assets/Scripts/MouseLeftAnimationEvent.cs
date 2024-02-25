using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLeftAnimationEvent : MonoBehaviour
{
    private Camera mainCamera;
    public RaycastHit hitInfo;
    public LayerMask layerMask; // Define the layer mask to filter which layers to interact with

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Perform raycast only against layers specified in the layerMask
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo, Mathf.Infinity, layerMask))
        {
           // Debug.Log("Target: " + hitInfo.transform.name);
        }
    }
}
