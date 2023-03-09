using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovementTutorial : MonoBehaviour
{
    public static PlayerMovementTutorial instance;

    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float dashCooldown = 10f;
    public float dashSpeed;

    private float stamina = 5f;
    private float maxStamina;
    private float sprintSpeed;
    private float initialSpeed;
    private bool isSprinting;

    public Slider staminaBar;

    bool readyToJump;
    private int jumpsRemaining = 1;

    bool haveStamina = true;

    [HideInInspector] public float walkSpeed;


    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    public bool onLadder = false;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        instance = this;

        readyToJump = true;

        sprintSpeed = moveSpeed + 6;
        initialSpeed = moveSpeed;

        maxStamina = stamina;
        staminaBar.maxValue = maxStamina;
    }

    private void Update()
    {
        dashCooldown -= Time.deltaTime;

        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        MyInput();
        SpeedControl();

        // handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;


        StaminaBar();

        if (!onLadder)
        {
            Sprint();
        }
        
    }

    private void FixedUpdate()
    {
        if (!onLadder)
        {
            MovePlayer();
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if(Input.GetKey(jumpKey) && readyToJump && jumpsRemaining > 0) //&& grounded)
        {
            readyToJump = false;
            jumpsRemaining -= 1;
            Jump();
            Invoke("ResetJump", jumpCooldown);

            Debug.Log("jumps remaining = " + jumpsRemaining);
        }

        if (grounded)
        {
            jumpsRemaining = 1;
        }
    }

    private void MovePlayer()
    {

        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        if (Input.GetKey(KeyCode.E) && dashCooldown <= 0)
        {
            Debug.Log("Dashing");
            rb.velocity = transform.forward * dashSpeed;
            dashCooldown = .5f;
        }

    }

    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && haveStamina && grounded)
        {
            moveSpeed = sprintSpeed; //set moveSpeed to Sprint speed
            stamina -= Time.deltaTime; //Decrease stamina according to time.deltatime.
            isSprinting = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || !haveStamina || !grounded)
        {
            moveSpeed = initialSpeed; //set back to original speed which was set at Start()
            isSprinting = false;
        }
    }

    private void StaminaBar()
    {
        if (stamina < maxStamina && !isSprinting)
        {
            stamina += Time.deltaTime; //If stamina is less than 20 while not sprinting, slowly regenerate

            if (stamina > maxStamina)
            {
                stamina = maxStamina; //caps stamina at 20.
            }
        }

        if (stamina >= 0f)
        {
            haveStamina = true;
        }
        else
        {
            haveStamina = false;
        }

        staminaBar.value = stamina;
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limit velocity if needed
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

}