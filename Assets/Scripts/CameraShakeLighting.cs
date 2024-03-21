using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeLighting : MonoBehaviour
{
    public float shakeMagnitude = 0.1f; // Sars�nt� miktar�

    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
        Invoke("StopShake", 5f); // Sars�nt�y� 5 saniye sonra durdur
        InvokeRepeating("Shake", 0, 0.1f); // 0.1 saniye aral�klarla sars�nt� yap
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
        CancelInvoke("Shake"); // Sars�nt�y� durdur
        transform.localPosition = originalPosition; // Kameray� orijinal konumuna geri getir
    }
}