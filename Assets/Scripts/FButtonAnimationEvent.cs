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
    private bool isDamaging = false; // New flag to indicate if the projectile is currently damaging an enemy

    void Update()
    {
        // orbitSpeed'i zamanla arttýr
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

        Vector3 orbitPosition = characterTransform.position + Quaternion.Euler(0, currentAngle, 0) * (Vector3.right * orbitRadius);

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
        boxCollider.size = new Vector3(1f, 1f, 1f);
        boxCollider.isTrigger = true;


        int enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");


        RaycastHit hit;
        if (Physics.Raycast(instantiatedPrefab.transform.position, fireDirection, out hit, Mathf.Infinity, enemyLayerMask) && boxCollider.isTrigger)
        {
            Debug.Log("Enemy detect oldu");
            isDamaging = true; // Set flag to indicate that the projectile is currently damaging an enemy
        }

        // If damage is possible and the projectile is currently damaging an enemy, deduct 50 health from the enemy
        if (isDamaging)
        {
            // Find the HealtSystem component of the hit enemy
            HealtSystem enemyHealthSystem = hit.collider.GetComponent<HealtSystem>();
            if (enemyHealthSystem != null)
            {
                enemyHealthSystem.TakeDamage(50);
            }

            // Reset the DamageOlabilir flag
            isDamaging = false;
        }
    }
}
