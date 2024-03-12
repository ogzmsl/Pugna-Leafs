using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intractions : MonoBehaviour
{
    public float interactionDistance = 1.5f;
    public GameObject character;
    public GameObject IntactUI;
    public Animator UIanimatorforIntaract;
    public bool ispressedbuttonI;
    public bool distanceObjct;

    public Chest chestref;
 

    private void Awake()
    {
        IntactUI.SetActive(false);
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
                distanceObjct = true;
                IntactUI.SetActive(true);
                UIanimatorforIntaract.SetBool("UIIntract", true);
                ispressedbuttonI = true;
            
     
            }
            else if (distanceToCharacter >= interactionDistance)
            {

                chestref.intract = false;
                distanceObjct = false;
                IntactUI.SetActive(false);
            }
        }
    }
}
