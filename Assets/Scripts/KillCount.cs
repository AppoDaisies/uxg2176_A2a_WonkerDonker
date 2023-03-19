using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillCount : MonoBehaviour
{
    public static KillCount instance;

    public int killCount;
    public int maxKillCount;
    public TextMeshProUGUI killsText;


    public void Awake() //needs to be on Awake for maxkill count in Button Script.
    {
        instance = this;

        killCount = 0;
        maxKillCount = 0;
    }
    private void Update()
    {
        killsText.text = "Kill Count : " + killCount + " / " + maxKillCount;
    }

}
