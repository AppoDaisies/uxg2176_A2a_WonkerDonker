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

    [HideInInspector] public Dictionary<WeaponType, int> currentAmmoDump = new Dictionary<WeaponType, int>();

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this);

        this.GetComponent<ReadFile>().GetData(WeaponStatStart);

        weapon = WeaponType.Pistol;

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

    public void UseAmmo()
    {
        currentAmmoDump[weapon]--;
    }
    public void WeaponStats()
    {
        switch (weapon)
        {
            case WeaponType.Pistol:

                maxAmmo = Game.GetGameData().GetWeaponByRefId("101").GetMaxAmmo();
                weaponDmg = Game.GetGameData().GetWeaponByRefId("101").GetWeaponDmg();
                fireCooldown = Game.GetGameData().GetWeaponByRefId("101").GetFireCoolDown();
                reloadTime = Game.GetGameData().GetWeaponByRefId("101").GetReloadTime();
                weaponIconIndex = Game.GetGameData().GetWeaponByRefId("101").GetWeaponIconIndex();

                break;

            case WeaponType.Rifle:

                maxAmmo = Game.GetGameData().GetWeaponByRefId("102").GetMaxAmmo();
                weaponDmg = Game.GetGameData().GetWeaponByRefId("102").GetWeaponDmg();
                fireCooldown = Game.GetGameData().GetWeaponByRefId("102").GetFireCoolDown();
                reloadTime = Game.GetGameData().GetWeaponByRefId("102").GetReloadTime();
                weaponIconIndex = Game.GetGameData().GetWeaponByRefId("102").GetWeaponIconIndex();

                break;

            case WeaponType.Grenade:

                maxAmmo = Game.GetGameData().GetWeaponByRefId("103").GetMaxAmmo();
                weaponDmg = Game.GetGameData().GetWeaponByRefId("103").GetWeaponDmg();
                fireCooldown = Game.GetGameData().GetWeaponByRefId("103").GetFireCoolDown();
                reloadTime = Game.GetGameData().GetWeaponByRefId("103").GetReloadTime();
                weaponIconIndex = Game.GetGameData().GetWeaponByRefId("103").GetWeaponIconIndex();

                break;
        }

    }
    public void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmoDump[weapon] != maxAmmo)
        {
            StartCoroutine(ReloadCoRoutine());
        }
    }

    private IEnumerator ReloadCoRoutine()
    {
        isReloading = true;

        showReload.SetActive(true);

        yield return new WaitForSeconds(reloadTime);

        currentAmmoDump[weapon] = maxAmmo;

        showReload.SetActive(false);

        isReloading = false;
    }

    public void AmmoCheck()
    {
        if (currentAmmoDump[weapon] <= 0 && !isReloading)
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

    private void WeaponStatStart() //BRUTE FORCE MADAFKER will be fixed with loading screen
    {
        currentAmmoDump.Add(WeaponType.Pistol, Game.GetGameData().GetWeaponByRefId("101").GetMaxAmmo());
        currentAmmoDump.Add(WeaponType.Rifle, Game.GetGameData().GetWeaponByRefId("102").GetMaxAmmo());
        currentAmmoDump.Add(WeaponType.Grenade, Game.GetGameData().GetWeaponByRefId("103").GetMaxAmmo());

        currentAmmoDump[weapon] = maxAmmo;
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
