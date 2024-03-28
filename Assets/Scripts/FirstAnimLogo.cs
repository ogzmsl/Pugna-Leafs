using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAnimLogo : MonoBehaviour
{
    public GameObject Logo;
    void Start()
    {
        Logo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(waitLogoanim());

        Destroy(Logo, 15);
    }





    IEnumerator waitLogoanim()
    {
        yield return new WaitForSeconds(0.3f);
        Logo.SetActive(true);
    }

    
}
