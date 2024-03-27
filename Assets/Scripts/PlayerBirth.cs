using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerBirth : MonoBehaviour
{
    public PlayerHealt health;
    public Animator playerAnimator;
    public Transform playerTransform;
    public Transform birthTransform;

    public bool hasRespawned = false;
    public bool dieControl = false;
    private ThirdPersonController controller;
    public GolemNavMesh navMesh;

    public bool Spawn = false;
    public bool goblinOneSpawn = false;
    public bool goblinTwoSpawn = false;
    public bool goblinThreeSpawn = false;
    public bool goblinFourSpawn = false;
    public bool goblinFiveSpawn = false;
    private ParticleSystem instantiatedParticle;

    public ParticleSystem birthParticlePrefab;

    public bool  CamWait=false;
 

    private void Awake()
    {
        controller = GetComponent<ThirdPersonController>();
    }
    private void Update()
    {
        if (health.die && !hasRespawned)
        {
            hasRespawned = true;
            Spawn = true;
            goblinOneSpawn = true;
            goblinTwoSpawn = true;
            goblinThreeSpawn = true;
            goblinFourSpawn = true;
            goblinFiveSpawn = true;
            Invoke("RespawnPlayer", 1f);
            CamWait = true;
            StartCoroutine(CamWaiter());

        }


    }



    private IEnumerator CamWaiter()
    {
        yield return new WaitForSeconds(4f);
        CamWait = false;
    }



    private void RespawnPlayer()
    {
        dieControl = true;
        playerTransform.position = birthTransform.position;
        playerTransform.rotation = birthTransform.rotation;
        navMesh.isAtRandomPoint = false;

        playerAnimator.SetTrigger("Birth");
        instantiatedParticle = Instantiate(birthParticlePrefab, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);


        StartCoroutine(DestroyWithTagAfterDelay("leaf", 5f));


    }


    private IEnumerator DestroyWithTagAfterDelay(string tag, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objectsWithTag)
        {
            Destroy(obj);
        }
    }
}
