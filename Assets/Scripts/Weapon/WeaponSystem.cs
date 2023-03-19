using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public static WeaponSystem instance;

    public GameObject showReload, showNoAmmo, showWeaponNoHave;

    public WeaponType weapon;

    [HideInInspector] public int weaponDmg;
    [HideInInspector] public int maxAmmo;
    [HideInInspector] public int currentAmmo;
    [HideInInspector] public float fireCooldown;
    [HideInInspector] public float reloadTime;
    [HideInInspector] public int weaponIconIndex;

    [HideInInspector] public bool noAmmo;
    [HideInInspector] public bool isReloading = false;
    [HideInInspector] public bool weaponIsAvailable = false;

    private void Start()
    {
        instance = this;

        weapon = WeaponType.Pistol;

        StartCoroutine(WeaponStatStart());
    }

    private void Update()
    {
        SwitchWeapon();
        WeaponStats();
        AmmoCheck();
        Reload();
        
    }

    public void SwitchWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapon = WeaponType.Pistol;

            Debug.Log(weapon);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (weaponIsAvailable)
            {
                weapon = WeaponType.Rifle;

                Debug.Log(weapon);
            }
            else
            {
                StartCoroutine(WeaponNotAvailable());
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weapon = WeaponType.Grenade;

            Debug.Log(weapon);
        }
    }

    public void WeaponStats()
    {
        switch (weapon)
        {
            case WeaponType.Pistol:

                maxAmmo = 12;
                weaponDmg = 25;
                fireCooldown = 0.2f;
                reloadTime = 1.5f;
                weaponIconIndex = 1;

            break;

            case WeaponType.Rifle:

                maxAmmo = 30;
                weaponDmg = 50;
                fireCooldown = 0.1f;
                reloadTime = 3f;
                weaponIconIndex = 2;

                break;

            case WeaponType.Grenade:

                maxAmmo = 1;
                weaponDmg = 100;
                fireCooldown = 2f;
                reloadTime = 6f;
                weaponIconIndex = 3;

                break;
        }
            
    }
    public void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo != maxAmmo)
        {
            StartCoroutine(ReloadCoRoutine());
        }
    }

    private IEnumerator ReloadCoRoutine()
    {
        isReloading = true;

        showReload.SetActive(true);

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;

        showReload.SetActive(false);

        isReloading = false;
    }

    public void AmmoCheck()
    {
        if (currentAmmo <= 0 && !isReloading)
        {
            noAmmo = true;
            showNoAmmo.SetActive(true);
        }
        else
        {
            noAmmo = false;
            showNoAmmo.SetActive(false);
        }
    }

    private IEnumerator WeaponStatStart() //BRUTE FORCE MADAFKER
    {
        yield return new WaitForSeconds(0.0001f);
        currentAmmo = maxAmmo;
    }

    private IEnumerator WeaponNotAvailable()
    {
        showWeaponNoHave.SetActive(true);

        yield return new WaitForSeconds(1f);

        showWeaponNoHave.SetActive(false);
    }


    public enum WeaponType
    {
        Pistol,
        Rifle,
        Grenade
    }


}
