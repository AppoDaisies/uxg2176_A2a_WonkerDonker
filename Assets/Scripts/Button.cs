using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    //display the UI text
    public GameObject txtToDisplay;

    public List<GameObject> Enemies;

    //check if the player is in trigger
    private bool PlayerInZone;                  

    private void Start()
    {

        PlayerInZone = false; //player not in zone                          
        txtToDisplay.SetActive(false);
    }

    private void Update()
    {
        if (PlayerInZone && Input.GetKeyDown(KeyCode.F)) //if in zone and press F key           
        {
            for(int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].SetActive(true);
                Debug.Log("Testing" + i);
                WeaponFire.instance.killCount = 0;
            }  

            //lightorobj.SetActive(!lightorobj.activeSelf);
            gameObject.GetComponent<AudioSource>().Play();
            gameObject.GetComponent<Animator>().Play("Switch");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") //if player in zone
        {
            txtToDisplay.SetActive(true);
            PlayerInZone = true;
        }
    }


    private void OnTriggerExit(Collider other) //if player exit zone
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerInZone = false;
            txtToDisplay.SetActive(false);
        }
    }
}
