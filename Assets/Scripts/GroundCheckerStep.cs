using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GroundCheckerStep : MonoBehaviour
{

  public   GameObject footvfx;
    public bool isfootvfxplay = true;


    private void FixedUpdate()
    {
        if (isfootvfxplay)
        {
            footvfx.SetActive(true);
        }
        else
        {
            footvfx.SetActive(false);
        }

    }



}
