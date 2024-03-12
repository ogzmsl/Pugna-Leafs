using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intractions1 : MonoBehaviour
{
    public float interactionDistance = 1f; 
    public GameObject character;
    public bool isChestIntract;
    public GameObject IntactUý;
    public Animator UIanimatorforIntaract;
    public bool ispressedbuttonI;
    public bool intractsecondery;




    private void Awake()
    {
        IntactUý.SetActive(false);
    }
    void Update()
    {
    
        Vector3 characterPosition = character.transform.position;

     
        GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest1");
        foreach (GameObject chest in chests)
        {
            float distanceToCharacter = Vector3.Distance(characterPosition, chest.transform.position);

            if (distanceToCharacter <= interactionDistance)
            {
               
                IntactUý.SetActive(true);
                UIanimatorforIntaract.SetBool("UIIntract", true);
                DestroyIntactButton();
                intractsecondery = true;
              
                
            }
        }
    }

    private void DestroyIntactButton()
    {
        if (ispressedbuttonI)
        {
            IntactUý.SetActive(false);
        }

        
    }



}
