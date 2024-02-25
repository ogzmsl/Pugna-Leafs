using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    public GameObject prefabToInstantiate;
    public Transform ProjectTilePoint;
    public Transform PlayerTransform;
    public bool AimChange;
    public MouseLeftAnimationEvent mouseLeftAnimationEvent;

    public void InstantiatePrefabEvent()
    {
        if (mouseLeftAnimationEvent != null && mouseLeftAnimationEvent.hitInfo.collider != null)
        {
            Vector3 fireDirection = mouseLeftAnimationEvent.hitInfo.point - ProjectTilePoint.position;
            fireDirection.Normalize();
            GameObject instantiatedPrefab = Instantiate(prefabToInstantiate, ProjectTilePoint.position, Quaternion.LookRotation(fireDirection));
        }
    }

    public void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("çalýþtý");
        }
    }
}
