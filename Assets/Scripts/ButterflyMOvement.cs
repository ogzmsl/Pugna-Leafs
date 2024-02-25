using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ButterflyMOvement : MonoBehaviour
{
    public Transform target; // Karakterinizin konumunu alacak transform
    public LayerMask layerMask; // Raycast ile kontrol edilecek katman
    public float moveSpeed = 5f; // Hareket hýzý

    private bool isMoving = false; // Hareket halinde olup olmadýðýný kontrol etmek için bir bayrak
    private Vector3 holdPosition; // Nesnenin hareket edeceði tutma pozisyonu

    void Update()
    {
        
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                holdPosition = hit.point; // Ýsabet eden noktayý al
                isMoving = true; // Hareket baþladý
                Debug.Log(hit.transform.name);
            }
        }


        if (isMoving)
        {
            MoveTowardsTarget(); // Hedefe doðru hareket et
        }
    }

    void MoveTowardsTarget()
    {
        // Küpün mevcut konumunu, tutma pozisyonuna doðru yumuþak bir þekilde hareket ettir
        transform.position = Vector3.Lerp(transform.position, holdPosition, moveSpeed * Time.deltaTime);
    }
}
