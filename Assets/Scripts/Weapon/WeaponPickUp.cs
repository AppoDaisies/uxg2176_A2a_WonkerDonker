using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponPickUp : MonoBehaviour
{
    public GameObject pickupText, weaponUI;

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            StartCoroutine(ObtainWeapon());
        }
    }

    private IEnumerator ObtainWeapon()
    {
        WeaponSystem.instance.weaponIsAvailable = true;
        pickupText.SetActive(true);

        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;

        Destroy(weaponUI);

        yield return new WaitForSeconds(1.5f);

        pickupText.SetActive(false);
        Destroy(this.gameObject);
    } 
}
