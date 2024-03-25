using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FButtonEffectDistance : MonoBehaviour
{
    public ParticleSystem Fparticle;

    private ParticleSystem instantiatedParticle;

    public bool isDistanceFButton = false;

    public void InstantiateFbuttonparticle()
    {
        instantiatedParticle = Instantiate(Fparticle, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);
        isDistanceFButton = true;
        StartCoroutine(DestroyParticleAfterDelay(5f));
        StartCoroutine(ResetBool());
        StartCoroutine(DestroyWithTagAfterDelay("leaf", 5f)); // "leaf" etiketine sahip nesneleri 5 saniye sonra yok et
    }

    private IEnumerator ResetBool()
    {
        yield return new WaitForSeconds(0.2f);
        isDistanceFButton = false;
    }

    private IEnumerator DestroyParticleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(instantiatedParticle.gameObject);
    }

    private IEnumerator DestroyWithTagAfterDelay(string tag, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objectsWithTag)
        {
            Destroy(obj);
        }
    }
}
