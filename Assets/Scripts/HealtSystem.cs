using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class HealtSystem : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider;  // Slider referansý

    [SerializeField] private float maxHealth = 100;
    public float currentHealth;
    private Animator parentAnimator;
   public  bool isDead;
    NavMeshControl navMesh;
    public ParticleSystem bloodGoblin;
<<<<<<< HEAD
<<<<<<< HEAD
    public bool isDamageBlood;
=======
=======
>>>>>>> parent of 4e6e600 (NewPlamece)
    public bool isDamageBlood = false;
    public GameObject enemy;
    [SerializeField] private float deadanimationtime;

<<<<<<< HEAD
    


>>>>>>> parent of c739808 (TapinakFinish)
=======
    //Golem için sayaç
    public int RangeCounter = 0;
    


>>>>>>> parent of 4e6e600 (NewPlamece)

    void Start()
    {
        navMesh = transform.parent.GetComponent<NavMeshControl>();
        parentAnimator = transform.parent.GetComponent<Animator>();
        currentHealth = maxHealth;
        UpdateHealthUI();
        isDead = true;
    }


    private void Update()
    {
        if (isDamageBlood == true||currentHealth<=0)
        {
            StartCoroutine(ResetWait());
        }
           
        healthSlider.transform.LookAt(Camera.main.transform.position, Vector3.up);

       
    }
    void UpdateHealthUI()
    {
      
        healthSlider.value = currentHealth;
    }
    public void TakeDamage(int damageAmount)
    {
        float previousHealth = currentHealth;

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
<<<<<<< HEAD
<<<<<<< HEAD
            parentAnimator.SetTrigger("die");
            navMesh.isDestroy = true;
=======
          parentAnimator.SetTrigger("die");
            StartCoroutine(waitforDead());

        //navMesh.isDestroy = true;
<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of c739808 (TapinakFinish)
=======
          parentAnimator.SetTrigger("die");
            StartCoroutine(waitforDead());

        navMesh.isDestroy = true;
>>>>>>> parent of 4e6e600 (NewPlamece)
        }
        else if (previousHealth > currentHealth)
        {
       
<<<<<<< HEAD
<<<<<<< HEAD
            parentAnimator.SetTrigger("Damage");
            bloodGoblin.Play();
=======
          parentAnimator.SetTrigger("Damage");
            //   bloodGoblin.Play();


<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of c739808 (TapinakFinish)
=======
>>>>>>> parent of c739808 (TapinakFinish)
=======
          parentAnimator.SetTrigger("Damage");
            //   bloodGoblin.Play();

            RangeCounter++;
>>>>>>> parent of 4e6e600 (NewPlamece)
            isDamageBlood = true;


        }

        UpdateHealthUI();
    }


    IEnumerator ResetWait()
    {
        yield return new WaitForSeconds(1.5f);
        isDamageBlood = false;
    }



    
    
    IEnumerator waitforDead()
    {
        yield return new WaitForSeconds(deadanimationtime);
        Destroy(enemy);
    }


}
