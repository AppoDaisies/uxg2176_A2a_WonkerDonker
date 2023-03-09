using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject playerScale;

    private void Start()
    {
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            
            print("!");
            other.gameObject.transform.parent = transform;

            //Rigidbody playerRB = other.gameObject.GetComponent<Rigidbody>();
            //playerRB.mass = 5;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            print("unparent");
            other.gameObject.transform.parent = null;
            //Rigidbody playerRB = other.gameObject.GetComponent<Rigidbody>();
            //playerRB.mass = 1;
        }
    }
}
