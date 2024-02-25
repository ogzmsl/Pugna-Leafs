using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{


    public ParticleSystem LeftFootStep;
    public ParticleSystem RightFootStep;
   void FootstepEvent(int whichfoot)
    {

        if (whichfoot == 0)
        {
            LeftFootStep.Play();
        }
        else if(whichfoot==1)
        {
            RightFootStep.Play();
        }
        else
        {
            LeftFootStep.Stop();
            RightFootStep.Stop();
        }
    }
}
