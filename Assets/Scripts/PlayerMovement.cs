using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 4f;

    public float gravity = -9.81f;
    public float jumpHeight = 0.03f;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask; // checking if surface layer is ground
    public LayerMask trampolineMask;
    public float groundStop = -0.1f;

    private Vector3 velocity;
    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(Vector3.ClampMagnitude(move, 1f) * speed * Time.deltaTime);

        //Gravity
        velocity.y += gravity * Time.deltaTime * Time.deltaTime;

        //Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = groundStop;
        }

        //Jump
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                //Cancel gravity so can jump
                velocity.y = jumpHeight;
            }
            
        }

        controller.Move(velocity);
    }

}
