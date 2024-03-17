using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator animator;
    public bool intract;
    public Intractions intractions;


    private void Start()
    {
        animator=this.GetComponent<Animator>();
    }
    private void Update()
    {
        if (intract&&intractions.distanceObjct)
        {
           animator.SetBool("ChestOpen", true);
        }
       
    }




}
