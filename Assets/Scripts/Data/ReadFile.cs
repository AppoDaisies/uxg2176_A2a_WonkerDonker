using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

[System.Serializable]
public class ReadFile : MonoBehaviour
{
    public ServerData serverData;

    public void GetData(System.Action onComplete)
    {
        StartCoroutine(Read(onComplete));
    }

    IEnumerator Read(System.Action onComplete)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/uxgwonker/GetJson.php");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success) // Checking if the URL is working if not debug error
        {
            Debug.Log(www.error);
        }
        else
        {
            
            string jsonString = www.downloadHandler.text; // Printing results into a string

            System.IO.File.WriteAllText(Application.dataPath + "/GameData.json", jsonString);

            ProcessGameData(jsonString);

            onComplete?.Invoke();

        }
        www.Dispose(); // In case there is a memory leak

    }

    public void ProcessGameData(string jsonString)
    {
        serverData = JsonUtility.FromJson<ServerData>(jsonString);

        GameData gameData = Game.GetGameData();

        List<Weapon> weaponList = new List<Weapon>();

        foreach(RefWeaponData weaponData in serverData.RefWeaponData)
        {
            weaponList.Add(new Weapon(weaponData.id, weaponData.name, weaponData.weapondmg, weaponData.maxammo, weaponData.firecooldown, 
                weaponData.reloadtime, weaponData.weaponiconindex));
        }

        gameData.SetWeaponList(weaponList);
    }
}
