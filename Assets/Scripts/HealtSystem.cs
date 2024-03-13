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
    public ParticleSystem bloodGoblin;
    public bool isDamageBlood;
    public GameObject enemy;
    [SerializeField] private float deadanimationtime;

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
        int previousHealth = currentHealth;

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
        parentAnimator.SetTrigger("die");
            StartCoroutine(waitforDead());

        //navMesh.isDestroy = true;
        }
        else if (previousHealth > currentHealth)
        {
       
           // parentAnimator.SetTrigger("Damage");
         //   bloodGoblin.Play();
           // isDamageBlood = true;
            StartCoroutine(waitforblood());
        }

        UpdateHealthUI();
    }

    IEnumerator waitforblood()
    {
        yield return new WaitForSeconds(0.3f);
        isDamageBlood = false;
    } IEnumerator waitforDead()
    {
        yield return new WaitForSeconds(deadanimationtime);
        Destroy(enemy);
    }


}
