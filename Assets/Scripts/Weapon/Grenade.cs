using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;

    private int grenadeDmg;

    public GameObject explosionEffect; 

    float countdown; 
    bool  hasExploded = false; 
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay; //coundown = 3f sec before exploding

    }

    // Update is called once per frame
    void Update()
    {
        if(WeaponSystem.instance.weaponID == "103")
        {
            grenadeDmg = WeaponSystem.instance.weaponDmg;
        }

        countdown -= Time.deltaTime;
        if(countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        Debug.Log("Boom");

        Instantiate(explosionEffect, transform.position, transform.rotation);

        //Detect nearby objects 
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius); 

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);

                if(rb.GetComponent<TargetScript>() != null)
                rb.GetComponent<TargetScript>().DoHit(grenadeDmg);
            }
        }
        // Add force 

        // Damage them 


        Destroy(gameObject);
        
    }
}
