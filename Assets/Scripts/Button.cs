using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    //display the UI text
    public GameObject txtToDisplay;
    public List<Vector3> originalPos;
    public List<GameObject> Enemies;

    //check if the player is in trigger
    private bool PlayerInZone;                  

    private void Start()
    {
        foreach(GameObject gameObject in Enemies)
        {
            originalPos.Add(gameObject.transform.position);

            KillCount.instance.maxKillCount += 1;
        }

        PlayerInZone = false; //player not in zone                          
        txtToDisplay.SetActive(false);
    }

    private void Update()
    {
        if (PlayerInZone && Input.GetKeyDown(KeyCode.F)) //if in zone and press F key           
        {
            for(int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].transform.position = originalPos[i];
                Enemies[i].SetActive(true);
            }

            KillCount.instance.killCount = 0;

            if(SceneManager.GetActiveScene().buildIndex != 0)
            {
                Objective.instance.m_timeToComplete = 80f;
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
