using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intractions : MonoBehaviour
{
    public float interactionDistance = 1f; 
    public GameObject character;
    public bool isChestIntract;
    public GameObject IntactU�;
    public Animator UIanimatorforIntaract;
    public bool ispressedbuttonI;




    private void Awake()
    {
        IntactU�.SetActive(false);
    }
    void Update()
    {
    
        Vector3 characterPosition = character.transform.position;

     
        GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");
        foreach (GameObject chest in chests)
        {
            float distanceToCharacter = Vector3.Distance(characterPosition, chest.transform.position);

            if (distanceToCharacter <= interactionDistance)
            {
               
                IntactU�.SetActive(true);
                UIanimatorforIntaract.SetBool("UIIntract", true);
                DestroyIntactButton();
                
            }
        }
    }

    private void DestroyIntactButton()
    {
        if (ispressedbuttonI)
        {
            IntactU�.SetActive(false);
        }

        
    }



}
