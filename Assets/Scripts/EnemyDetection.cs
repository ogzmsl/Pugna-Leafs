using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public string enemyTag = "Enemy"; 
    public float detectionDistance = 5f; 

    private Transform player;

    public bool isDetect;

    private void Start()
    {
        player = transform; 
    }

    private void Update()
    {
     
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

 
        foreach (GameObject enemy in enemies)
        {
 
            float distance = Vector3.Distance(player.position, enemy.transform.position);

         
            if (distance <= detectionDistance)
            {
                isDetect = true;
            }
            else if (distance >= detectionDistance)
            {
                isDetect = false;
            }
        }
    }
}
