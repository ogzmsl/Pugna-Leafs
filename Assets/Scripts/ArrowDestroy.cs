using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDestroy : MonoBehaviour
{

    public bool isCorrect=false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isCorrect = true;
            Destroy(this.gameObject);
        }
    }
}
