using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;


    private void Start()
    {
        instance = this;

    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        ResetAmmo();
        SceneManager.LoadScene(1);
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetAmmo()
    {
        WeaponSystem weapon = WeaponSystem.instance;
        weapon.currentAmmoDump[weapon.weaponID] = weapon.maxAmmo;
        weapon.weaponIsAvailable = false;
        
    }
}
