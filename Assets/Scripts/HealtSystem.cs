using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealtSystem : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider;  // Slider referansý

    private int maxHealth = 100;
    public int currentHealth;
    private Animator parentAnimator;
   public  bool isDead;
    NavMeshControl navMesh;

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
       
           
        healthSlider.transform.LookAt(Camera.main.transform.position, Vector3.up);
        
        
    }
    void UpdateHealthUI()
    {
      
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            parentAnimator.SetTrigger("die");

            navMesh.isDestroy = true;
        
        }

        UpdateHealthUI();
    }



}
