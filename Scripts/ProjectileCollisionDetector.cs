using UnityEngine;

public class ProjectileCollisionDetector : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("enemy"))
        {
            Debug.Log("temas");
        }
    }
}
