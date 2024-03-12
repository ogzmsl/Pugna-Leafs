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

    private Chest currentChest;

    private void Awake()
    {
        IntactUI.SetActive(false);
    }

    void Update()
    {
        Vector3 characterPosition = character.transform.position;
        GameObject[] chests = GameObject.FindGameObjectsWithTag("Chest");

        foreach (GameObject chestGO in chests)
        {
            Chest chest = chestGO.GetComponent<Chest>(); // Chest bileþenini al

            if (chest != null)
            {
                float distanceToCharacter = Vector3.Distance(characterPosition, chestGO.transform.position);

                if (distanceToCharacter <= interactionDistance)
                {
                    distanceObjct = true;
                    IntactUI.SetActive(true);
                    UIanimatorforIntaract.SetBool("UIIntract", true);
                    ispressedbuttonI = true;
                    currentChest = chest; 
                }
                else
                {
                    distanceObjct = false;
                    IntactUI.SetActive(false);
                }
            }
        }

     
    }
}