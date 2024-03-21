using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class QSpeel : MonoBehaviour
{
    [SerializeField] private ParticleSystem qSpell;
    [SerializeField] private Transform spellSpawnPoint;
    [SerializeField] private LayerMask terrainLayerMask;
    public Transform mainCamera;
    private RaycastHit hitInfo;
    public GameObject character;
    public float maxDistance = 5f; // Maksimum uzaklýk
    public float minDistance = 1f; // Minimum uzaklýk

    public float sphereRadius = 2f; // Küre yarýçapý
    [SerializeField] private float damageAmount = 50f;

    public Image QSpellImage;
    public ThirdPersonController controller;

    public bool isQWait=true;

    public bool sarsizni;
    
    

    public void InstantiateQSpell()
    {
        StartCoroutine(WaitForAnimations());
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo, Mathf.Infinity, terrainLayerMask))
        {
            // Debug.Log("Target: " + hitInfo.transform.name);
        }  
            QSpellImage.fillAmount += Time.fixedDeltaTime / 20;
        if (QSpellImage.fillAmount >= 0.98f)
        {
            QSpellImage.fillAmount = 1;
        }
     
      
    }

    private IEnumerator WaitForAnimations()
    {
        yield return new WaitForSeconds(0.5f);

        float distanceToCharacter = Vector3.Distance(character.transform.position, hitInfo.point);


        if (distanceToCharacter >= minDistance && distanceToCharacter <= maxDistance)
        {
            
            Vector3 spawnPosition = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
            RaycastHit groundHit;
            if (Physics.Raycast(new Vector3(spawnPosition.x, 100, spawnPosition.z), Vector3.down, out groundHit, Mathf.Infinity, terrainLayerMask))
            {
                spawnPosition.y = groundHit.point.y; 
            }

            Instantiate(qSpell, spawnPosition, Quaternion.LookRotation(mainCamera.forward));
            qSpell.Play();


            yield return new WaitForSeconds(0.5f);



            DamageEnemiesNear(hitInfo.point, sphereRadius, damageAmount);
        }

     
    }

    private void DamageEnemiesNear(Vector3 center, float radius, float damageAmount)
    {
        Collider[] colliders = Physics.OverlapSphere(center, radius);

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Enemy"))
            {
                HealtSystem enemyHealth = col.GetComponentInChildren<HealtSystem>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage((int)damageAmount);
                }
            }
        }
    }

    public void destroyspeelq()
    {
        Destroy(qSpell);
    }
}
