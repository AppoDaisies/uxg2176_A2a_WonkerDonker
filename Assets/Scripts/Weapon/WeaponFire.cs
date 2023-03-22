using System.Collections;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    public static WeaponFire instance;

    //public float cooldown = 0.3f;

    private bool isFiring = false;

    public GameObject particles;
    // Start is called bfore the first frame update
    void Start()
    {
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponSystem.instance.weaponID == "101") //for pistols
        {
            if (Input.GetButtonDown("Fire1") && !isFiring && !WeaponSystem.instance.noAmmo && !WeaponSystem.instance.isReloading)
            {
                if (GameManager.instance.gameOver != true)
                    StartCoroutine(FireWeapon());
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && !isFiring && !WeaponSystem.instance.noAmmo && !WeaponSystem.instance.isReloading) //for rifles
            {
                if (GameManager.instance.gameOver != true)
                    StartCoroutine(FireWeapon());
            }
        }
        
    }

    private IEnumerator FireWeapon()
    {
        if (WeaponSystem.instance.weaponID != "103") //if not grenade
        {
            isFiring = true;

            WeaponSystem.instance.UseAmmo();

            //WeaponSystem.instance.currentAmmo -= 1;

            this.GetComponentInChildren<ParticleSystem>().Play();

            RaycastHit hit;

            if (this.GetComponentInParent<MouseLook>().GetShootHitPos(out hit))
            {
                if (hit.collider.GetComponent<TargetScript>() != null)
                {
                    GameObject hitParticles = Instantiate(particles, hit.point, Quaternion.LookRotation(hit.normal)); //Instantiate particle prefab to play at hit point.

                    hit.collider.GetComponent<TargetScript>().DoHit(WeaponSystem.instance.weaponDmg);

                    StartCoroutine(DestroyParticles(hitParticles));
                }
                else if(hit.collider.GetComponent<ShootButton>() != null)
                {
                    TransitionManager.instance.LoadGame();
                }
            }

            yield return new WaitForSeconds(WeaponSystem.instance.fireCooldown);

            isFiring = false;
        }
        else
        {
            isFiring = true;

            WeaponSystem.instance.UseAmmo();

            //throw Grenade;

            yield return new WaitForSeconds(WeaponSystem.instance.fireCooldown);

            isFiring = false;
        }
        
    }

    private IEnumerator DestroyParticles(GameObject particles)
    {
        yield return new WaitForSeconds(particles.GetComponent<ParticleSystem>().main.startLifetime.constant);
        Destroy(particles);
    }
}
