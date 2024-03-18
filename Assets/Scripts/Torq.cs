using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Torq : MonoBehaviour
{
    public float torkAmount = 10f; // Uygulanacak tork miktar�
    private Vector3 initialPosition; // Ba�lang�� konumu
    private Quaternion initialRotation;//ba�lang�c rot
 

    private void Start()
    {
        initialPosition = transform.position; // Ba�lang�� konumunu kaydet
        initialRotation = transform.rotation;
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody platformRigidbody = GetComponent<Rigidbody>();

            if (platformRigidbody != null)
            {
                Vector3 deltaPosition = transform.position - initialPosition;
                float platformHeight = deltaPosition.y;
                Vector3 torkVector = Vector3.up * torkAmount;
                platformRigidbody.AddTorque(torkVector, ForceMode.Impulse);

                Vector3 newPosition = transform.position;
                newPosition.y = initialPosition.y + platformHeight;
                transform.position = newPosition;
            }
        }
        if (collision.gameObject.CompareTag("qSpellArea"))
        {
            ResetToInitialPosition(); // Ba�lang�� konumuna geri d�n
        }
    }

 
    private void ResetToInitialPosition()
    {
        transform.rotation = initialRotation;
    }
}
