using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 playerScale;

    private void Start()
    {
        playerScale = new Vector3(0.9f, 0.9f, 0.9f);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            
            print("!");
            //other.gameObject.transform.SetParent(transform, true);
            other.gameObject.transform.parent = transform.parent.transform.parent;
            //playerScale = new Vector3(0.9f, 0.9f, 0.9f);
            Debug.Log(other.gameObject.transform.parent + "tseting");

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
            other.gameObject.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            //Rigidbody playerRB = other.gameObject.GetComponent<Rigidbody>();
            //playerRB.mass = 1;
        }
    }
}
