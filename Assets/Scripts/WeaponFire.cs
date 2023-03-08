using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    public static WeaponFire instance;

    public float cooldown = 0.3f;

    private bool isFiring = false;

    public int killCount;

    public TextMeshProUGUI killsText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && !isFiring)
        {
            StartCoroutine(FireWeapon());
        }
        killsText.text = killCount.ToString();
    }

    private IEnumerator FireWeapon()
    {
        isFiring = true;

        RaycastHit hit;
        if (this.GetComponentInParent<MouseLook>().GetShootHitPos(out hit))
        {
            if (hit.collider.GetComponent<TargetScript>() != null)
            {
                Debug.Log(killCount);
                killCount++;
                hit.collider.GetComponent<TargetScript>().DoHit();
            }
        }

        this.GetComponentInChildren<ParticleSystem>().Play();

        yield return new WaitForSeconds(cooldown);

        isFiring = false;
    }
}
