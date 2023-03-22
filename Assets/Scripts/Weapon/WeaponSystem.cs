using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{

    public static WeaponSystem instance;

    public GameObject showReload, showNoAmmo, showWeaponNoHave;

    

    [HideInInspector] public int weaponDmg;
    [HideInInspector] public int maxAmmo;
    [HideInInspector] public int currentAmmo;
    [HideInInspector] public int weaponIconIndex;
    [HideInInspector] public float fireCooldown;
    [HideInInspector] public float reloadTime;
    [HideInInspector] public string weaponID;

    [HideInInspector] public bool noAmmo;
    [HideInInspector] public bool isReloading = false;
    [HideInInspector] public bool weaponIsAvailable = false;

    [HideInInspector] public Dictionary<string, int> currentAmmoDump = new Dictionary<string, int>();

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

        weaponID = "101";

    }

    private void Start()
    {
        Invoke("SetAmmoOnStart", 0.001f);

        if (showReload == null || showWeaponNoHave == null)
        {
            showReload = GameObject.Find("ReloadingText");
            showWeaponNoHave = GameObject.Find("WeaponNotAvailableText");
        }
    }

    private void Update()
    {
        WeaponStats();
        SwitchWeapon();
        AmmoCheck();
        Reload();
    }

    public void SwitchWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponID = "101";

            Debug.Log(weaponID);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (weaponIsAvailable)
            {
                weaponID = "102";

                Debug.Log(weaponID);
            }
            else
            {
                StartCoroutine(WeaponNotAvailable());
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            weaponID = "103";

            Debug.Log(weaponID);
        }
    }

    public void UseAmmo()
    {
        currentAmmoDump[weaponID]--;
    }
    public void WeaponStats()
    {
        Weapon currentWeapon = Game.GetGameData().GetWeaponByRefId(weaponID); //Get Weapon Data according to weapon ID


        //Setting Data accordingly
        maxAmmo = currentWeapon.GetMaxAmmo(); 
        weaponDmg = currentWeapon.GetWeaponDmg();
        fireCooldown = currentWeapon.GetFireCoolDown();
        reloadTime = currentWeapon.GetReloadTime();
        weaponIconIndex = currentWeapon.GetWeaponIconIndex();
    }
    public void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmoDump[weaponID] != maxAmmo)
        {
            StartCoroutine(ReloadCoRoutine());
        }
    }

    private IEnumerator ReloadCoRoutine()
    {
        isReloading = true;

        showReload.SetActive(true);

        yield return new WaitForSeconds(reloadTime);

        currentAmmoDump[weaponID] = maxAmmo;

        showReload.SetActive(false);

        isReloading = false;
    }

    public void AmmoCheck()
    {
        if (currentAmmoDump[weaponID] <= 0 && !isReloading)
        {
            if (showNoAmmo != null)
            {
                noAmmo = true;
                showNoAmmo.SetActive(true);
            }
        }
        else
        {
            if (showNoAmmo != null)
            {
                noAmmo = false;
                showNoAmmo.SetActive(false);
            }
        }

        if(showNoAmmo == null)
        {
            showNoAmmo =  GameObject.Find("NoAmmoText");
        }

    }

    private void WeaponStatStart() //BRUTE FORCE MADAFKER will be fixed with loading screen
    {
        foreach (Weapon weapon in Game.GetGameData().GetWeaponList())
        {
            currentAmmoDump.Add(weapon.GetId(), weapon.GetMaxAmmo());
        }

    }

    private void SetAmmoOnStart()
    {
        currentAmmoDump[weaponID] = maxAmmo;
    }

    private IEnumerator WeaponNotAvailable()
    {
        showWeaponNoHave.SetActive(true);

        yield return new WaitForSeconds(1f);

        showWeaponNoHave.SetActive(false);
    }
}
