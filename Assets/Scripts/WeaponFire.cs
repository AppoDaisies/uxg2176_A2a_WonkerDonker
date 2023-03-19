using System.Collections;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    public static WeaponFire instance;

    private int weaponDmg = 50;

    public float cooldown = 0.3f;

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
        if (Input.GetButtonDown("Fire1") && !isFiring)
        {
            if(GameManager.instance.gameOver != true)
                StartCoroutine(FireWeapon());
        }
    }

    private IEnumerator FireWeapon()
    {
        isFiring = true;

        this.GetComponentInChildren<ParticleSystem>().Play();

        RaycastHit hit;

        if (this.GetComponentInParent<MouseLook>().GetShootHitPos(out hit))
        {
            if (hit.collider.GetComponent<TargetScript>() != null)
            {
                GameObject hitParticles = Instantiate(particles, hit.point, Quaternion.LookRotation(hit.normal)); //Instantiate particle prefab to play at hit point.
                hit.collider.GetComponent<TargetScript>().DoHit(weaponDmg);

                yield return new WaitForSeconds(hitParticles.GetComponent<ParticleSystem>().main.startLifetime.constant); //after particle lifetime, destroy particles.
                Destroy(hitParticles);
            }
        }

        yield return new WaitForSeconds(cooldown);

        isFiring = false;
    }
}
