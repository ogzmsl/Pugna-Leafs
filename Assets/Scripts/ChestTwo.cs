using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestTwo : MonoBehaviour
{

    public Animator animator;

    public bool isChestTwo;

    public bool Intract;

    public Button UI;
    

    private void Start()
    {
        isChestTwo = false;
    }

    private void Update()
    {
        if (isChestTwo&&Intract)
        {
            animator.SetBool("ChestOpen1", true);
          
        }
        

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isChestTwo = true;
            UI.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UI.gameObject.SetActive(false);
        }
    }
}
