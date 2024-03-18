using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControllerPhy : MonoBehaviour
{
    public float eðimGücü = 10f; // Platformun eðilme hýzý
    private Dictionary<Transform, Vector3> sonKonumlar = new Dictionary<Transform, Vector3>(); // Platformlarýn son konumlarý
    private Dictionary<Transform, Quaternion> sonRotasyonlar = new Dictionary<Transform, Quaternion>(); // Platformlarýn son rotasyonlarý

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sonKonumlar[transform] = other.transform.position; // Platformýn son konumunu sakla
            sonRotasyonlar[transform] = other.transform.rotation; // Platformýn son rotasyonunu sakla
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

                // Eðim gücü vektörünü hesapla ve platforma uygula
                Vector3 eðimGücüVektörü = fark.normalized * eðimGücü;
                platform.GetComponent<Rigidbody>().AddForce(eðimGücüVektörü, ForceMode.Force);

                // Platformu yavaþça eski konum ve rotasyonuna döndür
                platform.position = Vector3.Lerp(platform.position, yeniKonum, Time.deltaTime);
                platform.rotation = Quaternion.Lerp(platform.rotation, yeniRotasyon, Time.deltaTime);
            }
        }
    }
}
