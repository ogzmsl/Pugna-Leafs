using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public Transform oyuncu;
    public float takipMesafesi = 10f;
    public float takipHizi = 0.05f;
    public float dolasmaAraligi = 30f;

    private bool takipDurumunda = false;
    private Vector3 baslangicPozisyonu;
    public bool GoblinDamage;

    void Start()
    {

        baslangicPozisyonu = transform.position;
    }

    void Update()
    {
        float mesafeOyuncu = Vector3.Distance(transform.position, oyuncu.position);

        if (mesafeOyuncu < takipMesafesi && !GoruntudeOyuncu())
        {
            takipDurumunda = true;
            TakipEt();
        }
        else
        {
            takipDurumunda = false;
            Dolas();
        }
    }

    void TakipEt()
    {
        transform.LookAt(oyuncu);
        transform.position = Vector3.Lerp(transform.position, oyuncu.position, takipHizi * Time.deltaTime);
    }

    void Dolas()
    {
        float sinAralik = Mathf.Sin(Time.time * dolasmaAraligi);
        float cosAralik = Mathf.Cos(Time.time * dolasmaAraligi);

        if (!takipDurumunda)
        {
            float newX = baslangicPozisyonu.x + sinAralik;
            float newZ = baslangicPozisyonu.z + cosAralik;

            Vector3 yeniPozisyon = new Vector3(newX, transform.position.y, newZ);
            transform.position = Vector3.Lerp(transform.position, yeniPozisyon, takipHizi * Time.deltaTime);
        }
    }

    bool GoruntudeOyuncu()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, (oyuncu.position - transform.position).normalized, out hit, takipMesafesi))
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }
}