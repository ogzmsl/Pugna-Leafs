using UnityEngine;
using System.Collections.Generic;

public class ButterflyControlerNEW : MonoBehaviour
{
    public Transform centerPoint; // Merkez noktas�
    public float radius = 5.0f; // Yar��ap
    public float moveSpeed = 10.0f;


  
    private List<Transform> butterflies;
    private List<Vector3> targetPositions;
    private List<float> angles; //  kelebek i�in farkl� bir a��
    private List<float> yAngles; //kelebek i�in farkl� bir y a��s�
    public bool isRightClicked = false;
    public ButterflyAttack butterflyAttack;
    public Transform enemy;
 

    void Start()
    {
        butterflies = new List<Transform>();
        targetPositions = new List<Vector3>();
        angles = new List<float>();
        yAngles = new List<float>();

        GameObject[] butterflyObjects = GameObject.FindGameObjectsWithTag("Butterfly");
        foreach (GameObject butterflyObject in butterflyObjects)
        {
            Transform butterflyTransform = butterflyObject.transform;
           
            butterflies.Add(butterflyTransform);

            // Rastgele a���
            float angle = Random.Range(0, 360);
            angles.Add(angle);

            // Rastgele y 
            float yAngle = Random.Range(0, 360);
            yAngles.Add(yAngle);

            Vector3 targetPosition = GetPositionFromAngles(angle, yAngle);
            targetPositions.Add(targetPosition);
        }
    }

    void Update()
    {
        if (isRightClicked)
        {
          
                MoveButterflies();         
       

        }

       else  if (butterflyAttack.RangeAttack)
        {
            isRightClicked = false;



            for (int i = 0; i < butterflies.Count; i++)
            {
                centerPoint.position = enemy.transform.position;
                Transform butterfly = butterflies[i];

                Vector3 randomDirection = Random.insideUnitSphere * radius;
                Vector3 targetPosition = centerPoint.position + randomDirection;

       
                butterfly.position = Vector3.MoveTowards(butterfly.position, targetPosition, moveSpeed * Time.deltaTime);
            }
        }
    }
    

    private void MoveButterflies()
    {
        if (!butterflyAttack.isButterFlyAttacking)
        {
            for (int i = 0; i < butterflies.Count; i++)
            {
                Transform butterfly = butterflies[i];

                Vector3 targetPosition = GetPositionFromAngles(angles[i], yAngles[i]);

                butterfly.position = Vector3.MoveTowards(butterfly.position, targetPosition, moveSpeed * Time.deltaTime);

                angles[i] += Time.deltaTime * moveSpeed / radius;
            }
        }

     
     
    }



    private Vector3 GetPositionFromAngles(float angle, float yAngle)
    {
        float radian = angle * Mathf.Deg2Rad;
        float x = centerPoint.position.x + radius * Mathf.Cos(radian);
        float z = centerPoint.position.z + radius * Mathf.Sin(radian);
        float y = centerPoint.position.y + Mathf.Sin(yAngle * Mathf.Deg2Rad); // fy eksenindeki hareket
        return new Vector3(x, y, z);
    }


}
