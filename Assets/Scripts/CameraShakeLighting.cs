using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeLighting : MonoBehaviour
{
    public float shakeMagnitude = 0.1f; // Sarsýntý miktarý

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
        Invoke("StopShake", 5f); // Sarsýntýyý 5 saniye sonra durdur
        InvokeRepeating("Shake", 0, 0.1f); // 0.1 saniye aralýklarla sarsýntý yap
    }

    void Shake()
    {
        float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
        float offsetY = Random.Range(-shakeMagnitude, shakeMagnitude);
        float offsetZ = Random.Range(-shakeMagnitude, shakeMagnitude);

        transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, offsetZ);
    }

    void StopShake()
    {
        CancelInvoke("Shake"); // Sarsýntýyý durdur
        transform.localPosition = originalPosition; // Kamerayý orijinal konumuna geri getir
    }
}