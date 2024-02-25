using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HealtSystem healthSystem;

    void Start()
    {
        // Set the health system reference for this enemy
        healthSystem = GetComponent<HealtSystem>();
    }
}
