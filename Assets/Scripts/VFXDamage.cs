using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXDamage : MonoBehaviour
{
    public ParticleSystem particleSystem;
    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    private Dictionary<GameObject, HealtSystem> enemyHealthSystems = new Dictionary<GameObject, HealtSystem>();

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();

        
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            HealtSystem healthSystem = enemy.GetComponent<HealtSystem>();
            if (healthSystem != null)
            {
                enemyHealthSystems.Add(enemy, healthSystem);
            }
        }
    }

    public void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            int events = particleSystem.GetCollisionEvents(other, collisionEvents);
            Debug.Log("Collision detected");

            
            HealtSystem healthSystem;
            if (enemyHealthSystems.TryGetValue(other.gameObject, out healthSystem))
            {
                healthSystem.TakeDamage(25);
            }
        }
    }
}
