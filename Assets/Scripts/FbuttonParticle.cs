using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FbuttonParticle : MonoBehaviour
{
    public ParticleSystem Particle_System;
    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    public bool fbuttondamege = false;
    HealtSystem healt;
  

    private void Start()
    {
       
        Particle_System = GetComponent<ParticleSystem>();
    }

    public void OnParticleCollision(GameObject other)
    {
        if (!fbuttondamege)
        {
            int events = Particle_System.GetCollisionEvents(other, collisionEvents);
            Debug.Log("Çalýþtý");
            
                fbuttondamege = true;
            

           
            Invoke("ResetFbuttonDamage", 0.0001f); // 1 saniye sonra fbuttondamege'yi false yap
           
        }
    }

    void ResetFbuttonDamage()
    {
        fbuttondamege = false;
    }
}
