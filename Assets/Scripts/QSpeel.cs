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
<<<<<<< Updated upstream
<<<<<<< HEAD
<<<<<<< HEAD
=======
=======

>>>>>>> Stashed changes
    public GameObject character;
    public float maxDistance = 5f; // Maksimum uzakl�k
    public float minDistance = 1f; // Minimum uzakl�k

    public float sphereRadius = 2f; // K�re yar��ap�
    [SerializeField] private float damageAmount = 50f;

    public Image QSpellImage;
    public ThirdPersonController controller;

    public bool isQWait=true;

    public bool sarsizni;


<<<<<<< Updated upstream
    
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



>>>>>>> Stashed changes

    public void InstantiateQSpell()
    {
        StartCoroutine(WaitForAnimations());
    }

    private void Update()
    {
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo, Mathf.Infinity, terrainLayerMask))
        {
            Debug.Log("Target: " + hitInfo.transform.name);
        }
    }

    private IEnumerator WaitForAnimations()
    {
        yield return new WaitForSeconds(0.5f);

        Vector3 spawnPosition = new Vector3(hitInfo.point.x, spellSpawnPoint.position.y-1.25f, hitInfo.point.z);
        Instantiate(qSpell, spawnPosition, Quaternion.LookRotation(mainCamera.forward));
        qSpell.Play();
    }

    public void destroyspeelq()
    {
        Destroy(qSpell);
    }
}
