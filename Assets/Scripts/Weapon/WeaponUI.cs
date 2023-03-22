using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public GameObject reloadObject;

    public Image weaponIcon;

    public Sprite pistolIcon, rifleIcon, grenadeIcon;

    public TextMeshProUGUI ammoUI,weaponName;

    private void Start()
    {
        Invoke("DeactivateUI", 0.001f);
    }
    private void Update()
    {
        ammoUI.text = WeaponSystem.instance.currentAmmoDump[WeaponSystem.instance.weaponID].ToString() + " / " + WeaponSystem.instance.maxAmmo;
        weaponName.text = Game.GetGameData().GetWeaponByRefId(WeaponSystem.instance.weaponID).GetName();

        WeaponIcon();
    }
    public void WeaponIcon()
    {
        switch (WeaponSystem.instance.weaponIconIndex)
        {
            case 1:
                weaponIcon.overrideSprite = pistolIcon;
                break;

            case 2:
                weaponIcon.overrideSprite = rifleIcon;
                break;

            case 3:
                weaponIcon.overrideSprite = grenadeIcon;
                break;
        }
    }

    public void DeactivateUI()
    {
        WeaponSystem system = WeaponSystem.instance;
        system.showReload.SetActive(false);
        system.showNoAmmo.SetActive(false);
        system.showWeaponNoHave.SetActive(false);
    }
}
