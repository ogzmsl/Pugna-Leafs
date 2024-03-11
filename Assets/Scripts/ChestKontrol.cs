using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestKontrol : MonoBehaviour
{

    public Animator chestanimator;
    public Intractions intract;



    void Start()
    {
    
    }

    private void Update()
    {

        if (intract.isChestIntract)
        {
            chestanimator.SetBool("ChestOpen", true);
           
        }
    }





}
