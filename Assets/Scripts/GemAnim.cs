using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemAnim : MonoBehaviour
{

    [SerializeField] private float UpSpeed;
    [SerializeField] private float RotSpeed;
    private Vector3 LastPos;

    [SerializeField] private float TargetToSpeed;

    private float timer=0;

    public Chest chest;


    private void Update()
    {
        UpAndRotate();
    }


    private void UpAndRotate()
    {

        if (chest.tetik)
        {
         
          transform.Rotate(Vector3.up * RotSpeed * Time.deltaTime);

            if (timer < 3)
            {
                transform.Translate(Vector3.up * UpSpeed * Time.deltaTime);
                timer += Time.deltaTime;
            }

        }
        if (timer >2.9f)
        {
            transform.Translate(new Vector3(321, 182, 0) * Time.deltaTime * TargetToSpeed);
           
        }


    }




}
