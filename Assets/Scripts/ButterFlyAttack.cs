using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterFlyAttack : MonoBehaviour
{
    
    public LayerMask enemyLayer;
    public Transform raycastOrigin;
    public float RayCastDistance;
    public bool isButterFlyAttacking = false;
    public bool RangeAttack = false;
    public int numberOfRays = 36; 

    private void Update()
    {
        
        for (int i = 0; i < numberOfRays; i++)
        {
          
            float angle = i * 360f / numberOfRays;

            
            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.up) * raycastOrigin.forward;

         
            RaycastHit hit;
            if (Physics.Raycast(raycastOrigin.position, direction, out hit, RayCastDistance, enemyLayer))
            {
                Debug.Log("DÜÞMAN ÝLE TEMAS YAPILDI");
               // isButterFlyAttacking = true;
                RangeAttack = true;
            }

        
            Debug.DrawRay(raycastOrigin.position, direction * RayCastDistance, Color.green);
        }
    }
}