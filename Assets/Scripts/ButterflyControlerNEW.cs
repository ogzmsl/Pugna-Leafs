using UnityEngine;
using System.Collections.Generic;

public class ButterflyControlerNEW : MonoBehaviour
{
    public Transform centerPoint; // Merkez noktasý
    public Transform newcenterpoint;
    public Transform currentcenterpoint;
    public float radius = 5.0f; // Yarýçap
    public float moveSpeed = 10.0f;

    private List<Transform> butterflies;
    private List<Vector3> targetPositions;
    private List<float> angles; //  kelebek için farklý bir açý
    private List<float> yAngles; //kelebek için farklý bir y açýsý
    public bool isRightClicked = false;
    public ButterFlyAttack butterFlyattack;

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

            // Rastgele açýö
            float angle = Random.Range(0, 360);
            angles.Add(angle);

            // Rastgele y 
            float yAngle = Random.Range(0, 360);
            yAngles.Add(yAngle);

            Vector3 targetPosition = GetPositionFromAngles(angle, yAngle);
            targetPositions.Add(targetPosition);
        }
    }

    private void MoveButterfliesInSphere()
    {
        for (int i = 0; i < butterflies.Count; i++)
        {
            Transform butterfly = butterflies[i];

            Vector3 targetPosition = GetRandomPointInSphere();

            butterfly.position = Vector3.MoveTowards(butterfly.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    private Vector3 GetRandomPointInSphere()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f); 
        float yAngle = Random.Range(0f, Mathf.PI); 

        
        float x = centerPoint.position.x + radius * Mathf.Sin(yAngle) * Mathf.Cos(angle);
        float y = centerPoint.position.y + radius * Mathf.Cos(yAngle);
        float z = centerPoint.position.z + radius * Mathf.Sin(yAngle) * Mathf.Sin(angle);

        return new Vector3(x, y, z);
    }




    void Update()
    {
        if (isRightClicked && !butterFlyattack.isButterFlyAttacking)
        {
            MoveButterflies();
            centerPoint.position = currentcenterpoint.position;

        }
        else if (isRightClicked && butterFlyattack.isButterFlyAttacking)
        {
            MoveButterfliesInSphere();
            centerPoint.position = newcenterpoint.position;
        }
    }


    private void MoveButterflies()
    {
        for (int i = 0; i < butterflies.Count; i++)
        {
            Transform butterfly = butterflies[i];

            Vector3 targetPosition = GetPositionFromAngles(angles[i], yAngles[i]);

            butterfly.position = Vector3.MoveTowards(butterfly.position, targetPosition, moveSpeed * Time.deltaTime);

            angles[i] += Time.deltaTime * moveSpeed / radius;
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
