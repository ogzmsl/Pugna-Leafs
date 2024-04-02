using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QSpeel : MonoBehaviour
{
    [SerializeField] private ParticleSystem qSpell;
    [SerializeField] private Transform spellSpawnPoint;
    [SerializeField] private LayerMask terrainLayerMask;
    public Transform mainCamera;
    private RaycastHit hitInfo;
<<<<<<< HEAD
=======
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
>>>>>>> parent of c739808 (TapinakFinish)

    public void InstantiateQSpell()
    {
        StartCoroutine(WaitForAnimations());
    }

    private void Update()
    {
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo, Mathf.Infinity, terrainLayerMask))
        {
<<<<<<< HEAD
            Debug.Log("Target: " + hitInfo.transform.name);
        }
=======
            // Debug.Log("Target: " + hitInfo.transform.name);
        }  
            QSpellImage.fillAmount += Time.fixedDeltaTime / 40;
        if (QSpellImage.fillAmount >= 0.98f)
        {
            QSpellImage.fillAmount = 1;
        }
     
      
>>>>>>> parent of c739808 (TapinakFinish)
    }

    private IEnumerator WaitForAnimations()
    {
        yield return new WaitForSeconds(0.5f);

<<<<<<< HEAD
        Vector3 spawnPosition = new Vector3(hitInfo.point.x, spellSpawnPoint.position.y-1.25f, hitInfo.point.z);
        Instantiate(qSpell, spawnPosition, Quaternion.LookRotation(mainCamera.forward));
        qSpell.Play();
=======
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

     
>>>>>>> parent of c739808 (TapinakFinish)
    }

    public void destroyspeelq()
    {
        Destroy(qSpell);
    }
}
