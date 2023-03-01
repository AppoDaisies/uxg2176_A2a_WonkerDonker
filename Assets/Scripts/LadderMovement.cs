using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;

public class LadderMovement : MonoBehaviour
{
    public Transform chController;
    bool inside = false;
    public float speedUpDown = 3.2f;
    public PlayerMovement FPSInput;

    void Start()
    {
        FPSInput = GetComponent<PlayerMovement>();
        inside = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            FPSInput.enabled = false;
            inside = !inside;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            FPSInput.enabled = true;
            inside = !inside;
        }
    }

    void Update()
    {
        if (inside == true && Input.GetKey("w"))
        {
            chController.transform.position += Vector3.up * speedUpDown * Time.deltaTime;
        }

        if (inside == true && Input.GetKey("s"))
        {
            chController.transform.position += Vector3.down * speedUpDown * Time.deltaTime;
        }
    }
}
