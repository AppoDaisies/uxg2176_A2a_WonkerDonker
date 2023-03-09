using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public bool inside;
    public float speedUpDown;
    public Rigidbody rbPlayer;

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (inside == true)
        {
            Climb();
        }
    }
    public void Climb()
    {
        if (Input.GetKey("w"))
        {
            Debug.Log("Going Up");
            rbPlayer.transform.position += Vector3.up * speedUpDown;
        }

        if (Input.GetKey("s"))
        {
            Debug.Log("Going Down");
            rbPlayer.transform.position += Vector3.down * speedUpDown;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            inside = true;
            rbPlayer = other.gameObject.GetComponent<Rigidbody>();
            rbPlayer.useGravity = false;
            PlayerMovementTutorial.instance.onLadder = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            inside = false;
            rbPlayer.useGravity = true;
            PlayerMovementTutorial.instance.onLadder = false;
            rbPlayer = null;
            Debug.Log("Exit");
        }
    }
}
