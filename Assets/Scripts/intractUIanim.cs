using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intractUIanim : MonoBehaviour
{
    public ChestTwo chestTwo;
    public Animator animator;
  
    // Update is called once per frame
    void Update()
    {
        if (chestTwo.isChestTwo)
        {
           
            animator.SetBool("UIIntractTwo", true);
        }
    }
}
