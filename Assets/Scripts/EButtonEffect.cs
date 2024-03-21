using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EButtonEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem eSpell;
    [SerializeField] private Transform spellSpawnPoint;
    [SerializeField] private LayerMask terrainLayerMask;
    public Transform mainCamera;
    private RaycastHit hitInfo;
    public GameObject character;
    public float maxDistance = 5f; // Maksimum uzaklýk
    public float minDistance = 1f; // Minimum uzaklýk

    public float sphereRadius = 2f; // Küre yarýçapý
    [SerializeField] private float damageAmount = 100f;


    public bool isRange;


    public bool isWaitLight;
   
    public bool isSpellingLight;
    public Image Lightimage;

    private void Start()
    {
        isWaitLight = true;
    }



    public void InstantiateESpell()
    {
        StartCoroutine(WaitForAnimations());
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo, Mathf.Infinity, terrainLayerMask))
        {
            // Debug.Log("Target: " + hitInfo.transform.name);
        }

        if (isWaitLight)
        {

            Lightimage.fillAmount += Time.fixedDeltaTime/30;

        }

        ResetTimer();



    }


    private void ResetTimer()
    {
        if (Lightimage.fillAmount>=1)
        {
            isWaitLight = false;

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
                spawnPosition.y = groundHit.point.y; // Zeminin yüksekliðiyle eþleþtir.
            }

            Instantiate(eSpell, spawnPosition, Quaternion.Euler(-90, 0, 0));
            eSpell.Play();

            isRange = true;


            DamageEnemiesNear(hitInfo.point, sphereRadius, damageAmount);
        }
        else
        {
            isRange = false;
        }
     
    }

    private void DamageEnemiesNear(Vector3 center, float radius, float damageAmount)
    {
        Collider[] colliders = Physics.OverlapSphere(center, radius);

        float dividedDamage = damageAmount;

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Enemy"))
            {
                HealtSystem enemyHealth = col.GetComponentInChildren<HealtSystem>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage((int)dividedDamage);
                }
            }
        }
    }

    public void destroyspeelE()
    {
        Destroy(eSpell);
    }
}
