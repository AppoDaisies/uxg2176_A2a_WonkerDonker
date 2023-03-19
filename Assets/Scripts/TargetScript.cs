using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    private int maxHP;
    private int currentHP;


    private void Start()
    {
        currentHP = 100;
        maxHP = currentHP;
    }


    public void DoHit(int dmg)
    {
        currentHP -= dmg;

        Debug.Log("Target is hit for " + dmg + "Damage");

        Death();
    }

    public void Death()
    {
        if(currentHP <= 0)
        {
            gameObject.SetActive(false);

            KillCount.instance.killCount++;
        }
    }
}
