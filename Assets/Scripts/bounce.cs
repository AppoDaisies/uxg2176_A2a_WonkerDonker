using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce : MonoBehaviour
{
    public float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Player")
        {
            print("BOUNCE");
            Rigidbody rbPlayer = col.gameObject.GetComponent<Rigidbody>();
            rbPlayer.velocity = transform.up * speed;
        }
    }
}
