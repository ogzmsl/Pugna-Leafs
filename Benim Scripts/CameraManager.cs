using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;

    public Transform targetTransform;
    public Transform camerapivot;
    private Vector3 CameraFollowVelocity = Vector3.zero;
    public float CameraFollwSpeed = 0.2f; //tepki süresi
    public float lookangle;
    public float pivotAngle;
    public float minpivangle=-35f;
    public float maxpivangle = 35f;

    public  Vector3 camarevectorpositons;

    public  float defaultpositions;
   public float cameracollisionpivot;
    public float cameracollisionradis=0.1f;
    public float mincoloffset = 0.2f;
    public  float cameracollisonoffset=0.2f;
    public Transform cameratransform;

    public CameraManager(Transform cameratransform)
    {
        this.cameratransform = cameratransform;
    }

    public LayerMask collisionlayers;

    public float cameraLookSpeed;
    public float cameraPivotSpeed;


    private void Awake()
    {
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        inputManager = FindObjectOfType<InputManager>();
        cameratransform = Camera.main.transform;
        defaultpositions = cameratransform.localPosition.z;

    }

    public void HandleAllCameraMove()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisons();
    }
    private void FollowTarget()
    {
        //hýz vectorü ve kuvveti yumaþýklýk saðladým
        Vector3 targetPositions = Vector3.SmoothDamp(transform.position, targetTransform.position,ref CameraFollowVelocity,CameraFollwSpeed);
        transform.position = targetPositions;
    }

    private void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetrotation;
        pivotAngle = Mathf.Clamp(pivotAngle, minpivangle, maxpivangle);
        rotation = Vector3.zero;
        rotation.y = lookangle;
       targetrotation = Quaternion.Euler(rotation);
        transform.rotation = targetrotation;
        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetrotation = Quaternion.Euler(rotation);
        camerapivot.localRotation = targetrotation;
    }

    public void HandleCameraCollisons()
    {
        float targetPositioncol = defaultpositions;
        RaycastHit hit;
        Vector3 direction = cameratransform.position - camerapivot.position;
        direction.Normalize();
        
        if (Physics.SphereCast(camerapivot.transform.position, cameracollisionradis,direction,out hit, Mathf.Abs(targetPositioncol), collisionlayers)){
            float distance = Vector3.Distance(camerapivot.position, hit.point);
            targetPositioncol =-targetPositioncol- (distance - cameracollisonoffset);
            Debug.Log("worked");

        }
        if (Mathf.Abs(targetPositioncol) < mincoloffset)
        {
            targetPositioncol = targetPositioncol - mincoloffset;
            Debug.Log("worked 1");
        }
        camarevectorpositons.z = Mathf.Lerp(cameratransform.localPosition.z, targetPositioncol, 0.2f);
        cameratransform.localPosition = camarevectorpositons;
    }
}
