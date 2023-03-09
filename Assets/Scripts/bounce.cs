using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce : MonoBehaviour
{
    public float bounceForce;

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Player")
        {
            print("BOUNCE");
            Rigidbody rbPlayer = col.gameObject.GetComponent<Rigidbody>();
            rbPlayer.velocity = transform.up * bounceForce;
        }
    }
}
