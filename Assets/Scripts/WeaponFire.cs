using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    public float cooldown = 0.3f;

    private bool isFiring = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isFiring)
        {
            StartCoroutine(FireWeapon());
        }
    }

    private IEnumerator FireWeapon()
    {
        isFiring = true;

        RaycastHit hit;
        if (this.GetComponentInParent<MouseLook>().GetShootHitPos(out hit))
        {
            if (hit.collider.GetComponent<TargetScript>() != null)
            {
                hit.collider.GetComponent<TargetScript>().DoHit();
            }
        }

        this.GetComponentInChildren<ParticleSystem>().Play();

        yield return new WaitForSeconds(cooldown);

        isFiring = false;
    }
}
