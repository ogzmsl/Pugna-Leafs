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
    public GameObject character;
    public float maxDistance = 5f; // Maksimum uzaklýk

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

       
        float distanceToCharacter = Vector3.Distance(character.transform.position, hitInfo.point);

      
        if (distanceToCharacter <= maxDistance)
        {
            Vector3 spawnPosition = new Vector3(hitInfo.point.x, spellSpawnPoint.position.y - 1f, hitInfo.point.z);
            Instantiate(qSpell, spawnPosition, Quaternion.LookRotation(mainCamera.forward));
            qSpell.Play();
        }
        else
        {
            Debug.Log("Karakterden uzaklýk 5 birimden fazla.");
        }
    }

    public void destroyspeelq()
    {
        Destroy(qSpell);
    }
}
