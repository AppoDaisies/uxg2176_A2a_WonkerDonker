using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
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
