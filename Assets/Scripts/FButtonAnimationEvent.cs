using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FButtonAnimationEvent : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    public Transform characterTransform;
    public float orbitRadius = 5f;
    public float orbitSpeed = 0f;
    public float maxOrbitSpeed = 80f;
    public float orbitSpeedIncreaseRate = 5f;
    public float projectileSpeed = 10f;
    public float maxLifetime = 5f;
    public float particleForce = 10f;
    public MouseLeftAnimationEvent mouseLeftAnimationEvent;

    private float currentAngle = 0f;
    public bool isDamaging = false;

    void Update()
    {
        
        if (orbitSpeed < maxOrbitSpeed)
        {
            orbitSpeed += orbitSpeedIncreaseRate * Time.deltaTime;
        }
        currentAngle += orbitSpeed * Time.deltaTime;

    }

    public void InstantiatePrefabEventForFBUTTON()
    {
        Vector3 fireDirection = mouseLeftAnimationEvent.hitInfo.point - characterTransform.position;
        fireDirection.Normalize();

        Vector3 orbitPosition = characterTransform.position;

        GameObject instantiatedPrefab = Instantiate(prefabToInstantiate, orbitPosition, Quaternion.identity);

        instantiatedPrefab.transform.LookAt(characterTransform.position);

        ParticleSystem particleSystem = instantiatedPrefab.GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            particleSystem.Play();
            Rigidbody particleRigidbody = instantiatedPrefab.GetComponent<Rigidbody>();

            if (particleRigidbody == null)
            {
                particleRigidbody = instantiatedPrefab.AddComponent<Rigidbody>();
            }
            particleRigidbody.AddForce(fireDirection * particleForce, ForceMode.Impulse);
            Destroy(instantiatedPrefab, maxLifetime);
        }
        else
        {
            Debug.LogWarning("Particle System component not found on instantiatedPrefab.");
        }


        BoxCollider boxCollider = instantiatedPrefab.AddComponent<BoxCollider>();
        boxCollider.size = new Vector3(2f, 2f, 2f);
        boxCollider.isTrigger = true;


        int enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");


        RaycastHit hit;
        if (Physics.Raycast(instantiatedPrefab.transform.position, fireDirection, out hit, Mathf.Infinity) && boxCollider.isTrigger)
        {
            Debug.Log("Enemy detect oldu");
            isDamaging = true; 
        }

       
        if (isDamaging)
        {
          
            HealtSystem enemyHealthSystem = hit.collider.GetComponent<HealtSystem>();
            if (enemyHealthSystem != null)
            {
                enemyHealthSystem.TakeDamage(30);
            }
          

            // Reset the DamageOlabilir flag
            isDamaging = false;
        }
    }
}
