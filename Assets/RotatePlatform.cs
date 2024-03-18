using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlatform : MonoBehaviour
{

    public float RotationSpeed;
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
    }



}
