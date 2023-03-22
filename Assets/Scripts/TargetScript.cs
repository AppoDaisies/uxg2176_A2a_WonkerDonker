using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetScript : MonoBehaviour
{
    [HideInInspector] public int maxHP;
    [HideInInspector] public int currentHP;

    public TextMeshProUGUI hpText;

    private void Start()
    {
        currentHP = 100;
        maxHP = currentHP;
    }
    private void Update()
    {
        hpText.text = currentHP.ToString();
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
