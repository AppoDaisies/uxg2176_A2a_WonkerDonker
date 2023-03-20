using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameData
{
    private List<Weapon> weaponList;
    

    public List<Weapon> GetWeaponList()
    {
        return weaponList;
    }

    public Weapon GetWeaponByRefId(string aId)
    {
        return weaponList.Find(x => x.GetId() == aId);
    }

    public void SetWeaponList(List<Weapon> aList)
    {
        weaponList = aList;
    }
}
