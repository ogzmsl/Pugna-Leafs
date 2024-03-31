using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemTetikleme : MonoBehaviour
{
    public bool GolemTetiklemeBool = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GolemTetiklemeBool = true;
        }
    }


}
