using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shield : MonoBehaviour
{
    private const float Seconds = 0.8f;
    public ParticleSystem ShieldVFX;

    public ParticleSystem shieldInstance;

   
    public GameObject player;

    public bool isDestroyShield = false;

  
    private float Timer = 0;

    void Start()
    {
        
        if (player == null)
        {
            Debug.LogError("player referansý gerek shield için");
        }
    }

    [System.Obsolete]
    public void ShieldInstantiate()
    {
       
        if (shieldInstance == null)
        {
          
            shieldInstance = Instantiate(ShieldVFX, player.transform.position, Quaternion.identity);
            shieldInstance.transform.parent = player.transform;
            StartCoroutine(ShiledSeconds());
            ShieldVFX.playbackSpeed = 1.5f;

          

        }
    }

    public void ShieldDestroy()
    {
    
        if (shieldInstance != null)
        {
            shieldInstance.Pause();
            Destroy(shieldInstance.gameObject); 
            shieldInstance = null;
            isDestroyShield = true;
        }
    }

    IEnumerator ShiledSeconds()
    {
        shieldInstance.Play();
        yield return new WaitForSeconds(Seconds);
<<<<<<< HEAD
<<<<<<< Updated upstream
<<<<<<< HEAD
<<<<<<< HEAD
=======
=======

>>>>>>> Stashed changes
        shieldInstance.Play();
    

        shieldInstance.Play();

=======
>>>>>>> parent of d93495c (revert)
        shieldInstance.Pause();
<<<<<<< HEAD
<<<<<<< Updated upstream
=======

    
       shieldInstance.Pause();

>>>>>>> Stashed changes
=======
>>>>>>> parent of 0836829 (UPTADE)

    }
}
