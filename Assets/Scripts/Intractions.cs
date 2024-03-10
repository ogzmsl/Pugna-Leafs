using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intractions : MonoBehaviour
{
    void Update()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

       
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Çarptýðý obje: " + hit.collider.gameObject.name);
        }
    }
}
