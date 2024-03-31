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
    public bool isDamageBlood = false;
    public GameObject enemy;
    [SerializeField] private float deadanimationtime;

    //Golem için sayaç
    public int RangeCounter = 0;
    



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
          parentAnimator.SetTrigger("die");
            StartCoroutine(waitforDead());

        navMesh.isDestroy = true;
        }
        else if (previousHealth > currentHealth)
        {
       
          parentAnimator.SetTrigger("Damage");
            //   bloodGoblin.Play();

            RangeCounter++;
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
