using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    public static WeaponFire instance;

    public float cooldown = 0.3f;

    private bool isFiring = false;

    public int killCount = 0;

    public TextMeshProUGUI killsText;
    // Start is called bfore the first frame update
    void Start()
    {
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {

        killsText.text = killCount.ToString(); 

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
                killCount++;
            }
        }

        this.GetComponentInChildren<ParticleSystem>().Play();

        yield return new WaitForSeconds(cooldown);

        isFiring = false;
    }
}
