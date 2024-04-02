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
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
=======
>>>>>>> parent of 4e6e600 (NewPlamece)
    public GameObject character;
    public float maxDistance = 5f; // Maksimum uzaklýk
    public float minDistance = 1f; // Minimum uzaklýk

    public float sphereRadius = 2f; // Küre yarýçapý
    [SerializeField] private float damageAmount = 50f;

    public Image QSpellImage;
    public ThirdPersonController controller;

    public bool isQWait=true;

    public bool sarsizni;

<<<<<<< HEAD

    
<<<<<<< HEAD
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of d93495c (revert)
=======
    public bool isDoit;

    public float distanceToCharacter;
    public float offset;
    public AudioSource QspeelAudio;


    public void PlaySoundsQ()
    {
        QspeelAudio.Play();
    }


>>>>>>> parent of 4e6e600 (NewPlamece)

    public void InstantiateQSpell()
    {
       // StartCoroutine(WaitForAnimations());
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo, Mathf.Infinity, terrainLayerMask))
        {
            // Debug.Log("Target: " + hitInfo.transform.name);
        }  
        //bekleme süresi 20 saniye
            QSpellImage.fillAmount += Time.fixedDeltaTime / 5;
        if (QSpellImage.fillAmount >= 0.98f)
        {
            QSpellImage.fillAmount = 1;
        }
     
      
    }

    private void Update() { 
        distanceToCharacter = Vector3.Distance(character.transform.position, hitInfo.point);


        if (distanceToCharacter >= minDistance && distanceToCharacter <= maxDistance)
        {

            Vector3 spawnPosition = new Vector3(hitInfo.point.x, 1f, hitInfo.point.z);
            RaycastHit groundHit;
            isDoit = true;
        
         
            if (Physics.Raycast(new Vector3(spawnPosition.x, 100, spawnPosition.z), Vector3.down, out groundHit, Mathf.Infinity, terrainLayerMask))
            {
                spawnPosition.y = groundHit.point.y;
            }
        }
        else
        {
            isDoit = false;
       


        }
    }
    public void instantiateQ()
    {
      

        float distanceToCharacter = Vector3.Distance(character.transform.position, hitInfo.point);


        if (distanceToCharacter >= minDistance && distanceToCharacter <= maxDistance)
        {
      
            Vector3 spawnPosition = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
            RaycastHit groundHit;
            if (Physics.Raycast(new Vector3(spawnPosition.x, 100, spawnPosition.z), Vector3.down, out groundHit, Mathf.Infinity, terrainLayerMask))
            {
                spawnPosition.y = groundHit.point.y+offset; 
            }

            Instantiate(qSpell, spawnPosition, Quaternion.LookRotation(mainCamera.forward));
            qSpell.Play();




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

   
}
