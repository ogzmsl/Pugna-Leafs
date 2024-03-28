using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueFalse : MonoBehaviour
{
    public GameObject Goblin;
    public bool isRespawm;
    void Start()
    {
        Goblin.SetActive(true);
    }

   
    void Update()
    {
        if (isRespawm)
        {
            Goblin.SetActive(false);
        }
        else if (!isRespawm)
        {
            Goblin.SetActive(true);
        }


    }
}
