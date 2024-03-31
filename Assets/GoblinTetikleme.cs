using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinTetikleme : MonoBehaviour
{
    public bool GoblinTetiklemeBool = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GoblinTetiklemeBool = true;
        }
    }
}
