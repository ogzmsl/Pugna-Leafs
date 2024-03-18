using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator animator;
    public bool intract;
    public Intractions intractions;
    public bool tetik;

    public static Chest chest;
    private void Start()
    {
        if (chest != null)
        {
            chest = this;
        }
        animator=this.GetComponent<Animator>();
    }
    private void Update()
    {
        if (intract&&intractions.distanceObjct)
        {
           animator.SetBool("ChestOpen", true);
            tetik = true;
        }
       
    }




}
