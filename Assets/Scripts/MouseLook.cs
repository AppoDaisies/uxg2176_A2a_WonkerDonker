using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 80f;
    public Transform playerBody;

    public bool flipXRotation = false;
    public bool flipYRotation = false;
    public float yLookMin = -80f;
    public float yLookMax = 80f;

    private float xRotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.gameOver == false)
        {
            PlayerLook();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        
    }

    public bool GetShootHitPos(out RaycastHit hit)
    {
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit);
    }

    public void PlayerLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Vertical Look
        xRotation += flipYRotation ? mouseY : -mouseY;
        xRotation = Mathf.Clamp(xRotation, yLookMin, yLookMax);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        //Horizontal Look
        playerBody.Rotate(Vector3.up, flipXRotation ? -mouseX : mouseX);
    }
}
