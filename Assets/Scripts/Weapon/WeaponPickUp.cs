using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponPickUp : MonoBehaviour
{
    public GameObject pickupText;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            StartCoroutine(ObtainRifle());
        }
    }

    private IEnumerator ObtainRifle()
    {
        WeaponSystem.instance.weaponIsAvailable = true;
        pickupText.SetActive(true);

        yield return new WaitForSeconds(1f);

        pickupText.SetActive(false);
        Destroy(this.gameObject);
    } 
}
