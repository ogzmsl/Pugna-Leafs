using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControllerPhy : MonoBehaviour
{
    public float e�imG�c� = 10f; // Platformun e�ilme h�z�
    private Dictionary<Transform, Vector3> sonKonumlar = new Dictionary<Transform, Vector3>(); // Platformlar�n son konumlar�
    private Dictionary<Transform, Quaternion> sonRotasyonlar = new Dictionary<Transform, Quaternion>(); // Platformlar�n son rotasyonlar�

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sonKonumlar[transform] = other.transform.position; // Platform�n son konumunu sakla
            sonRotasyonlar[transform] = other.transform.rotation; // Platform�n son rotasyonunu sakla
        }
    }

    void FixedUpdate()
    {
        foreach (var platform in sonKonumlar.Keys)
        {
            Vector3 sonKonum = sonKonumlar[platform];
            Quaternion sonRotasyon = sonRotasyonlar[platform];

            if (sonKonum != Vector3.zero)
            {
                Vector3 yeniKonum = sonKonum;
                Quaternion yeniRotasyon = sonRotasyon;
                sonKonumlar[platform] = Vector3.zero;

                Vector3 fark = yeniKonum - platform.position;
                fark.y = 0f;

                // E�im g�c� vekt�r�n� hesapla ve platforma uygula
                Vector3 e�imG�c�Vekt�r� = fark.normalized * e�imG�c�;
                platform.GetComponent<Rigidbody>().AddForce(e�imG�c�Vekt�r�, ForceMode.Force);

                // Platformu yava��a eski konum ve rotasyonuna d�nd�r
                platform.position = Vector3.Lerp(platform.position, yeniKonum, Time.deltaTime);
                platform.rotation = Quaternion.Lerp(platform.rotation, yeniRotasyon, Time.deltaTime);
            }
        }
    }
}
