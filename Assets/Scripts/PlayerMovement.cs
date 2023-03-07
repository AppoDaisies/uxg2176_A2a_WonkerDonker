using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 4f;

    public float gravity = -9.81f;
    public float jumpHeight = 0.01f;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask; // checking if surface layer is ground
    public LayerMask trampolineMask;
    public float groundStop = -0.1f;


    bool inside = false;
    public float speedUpDown = 3.2f;
    public PlayerMovement FPSInput;

    private Vector3 velocity;
    private bool isGrounded = false;
    private bool isTrampoline = false;
    public float bounceForce = 0.015f;

    // Start is called before the first frame update
    void Start()
    {
        FPSInput = GetComponent<PlayerMovement>();
        inside = false;
        FPSInput.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(inside == true)
        {
            Climb();
        }
        else
        {
            Movement();

            //Gravity
            velocity.y += gravity * Time.deltaTime * Time.deltaTime;

            //Ground Check
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            isTrampoline = Physics.CheckSphere(groundCheck.position, groundDistance, trampolineMask);
            if (isGrounded && velocity.y < 0 || isTrampoline && velocity.y < 0)
            {
                velocity.y = groundStop;
            }

            Jump();
        }
        
    }

    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(Vector3.ClampMagnitude(move, 1f) * speed * Time.deltaTime);

    }

    private void Jump()
    {
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

    private void Climb()
    {
        if (inside == true && Input.GetKey("w"))
        {
            controller.transform.position += Vector3.up * speedUpDown * Time.deltaTime;
        }

        if (inside == true && Input.GetKey("s"))
        {
            controller.transform.position += Vector3.down * speedUpDown * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            StartCoroutine(cooldown(0.1f));
        }
        else if (col.gameObject.tag == "Trampoline" && isGrounded == false)
        {
            velocity.y = bounceForce;
        }

        if(col.gameObject.tag == "Platform")
        {
            Debug.Log("TestPlatform");
            gameObject.transform.parent = col.gameObject.transform;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Ladder")
        {
            StartCoroutine(cooldown(0.1f));
        }
    }

    IEnumerator cooldown(float delay)
    {
        yield return new WaitForSeconds(delay);
        //FPSInput.enabled = !FPSInput.enabled;
        inside = !inside;
    }

}
