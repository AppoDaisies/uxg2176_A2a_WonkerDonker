using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon
{
    private string id;
    private string name;
    private int weapondmg;
    private int maxammo;
    private float firecooldown;
    private float reloadtime;
    private int weaponiconindex;

    public Weapon(string id, string name, int weapondmg, int maxammo, float firecooldown, float reloadtime, int weaponiconindex)
    {
        this.id = id;
        this.name = name;
        this.weapondmg = weapondmg;
        this.maxammo = maxammo;
        this.firecooldown = firecooldown;
        this.reloadtime = reloadtime;
        this.weaponiconindex = weaponiconindex;

    }

    public string GetId() => id;
    public string GetName() => name;

    public int GetWeaponDmg() => weapondmg;

    public int GetMaxAmmo() => maxammo;

    public float GetFireCoolDown() => firecooldown;

    public float GetReloadTime() => reloadtime;

    public int GetWeaponIconIndex() => weaponiconindex;


}