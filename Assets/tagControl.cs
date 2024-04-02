using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tagControl : MonoBehaviour
{

    public bool Taged;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Taged = true;
        }
     

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Taged = false;
        }


    }
}
