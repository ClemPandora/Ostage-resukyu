using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    
    public float mouseSensitivity = 100f;

    public CharacterController controller;
    
    public float speed = 15f;
    
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float acceleration;
    public float airControl;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    
    private Vector3 velocity;
    private bool isGrounded;
    
    public Transform playerBody;
    public Camera camera;

    private float xRotation = 0f;
    
    
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        MouseLook();
        
        // check if player is on ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // lock the increase of down velocity if player is on ground
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //set inputs values
        Vector2 inputs;
        inputs.x = Input.GetAxis("Horizontal");
        inputs.y = Input.GetAxis("Vertical");
        inputs.Normalize();
        
        if (isGrounded)// pre calculate velocity on ground
        {
            if (inputs.x < -0.1 || inputs.x > 0.1)
            {
                velocity.x = Mathf.Lerp(velocity.x, inputs.x, Time.deltaTime * acceleration);
            }
            else
            {
                velocity.x = Mathf.Lerp(velocity.x, 0, Time.deltaTime * acceleration);
            }
            
            if (inputs.y < -0.1 || inputs.y > 0.1)
            {
                velocity.z = Mathf.Lerp(velocity.z, inputs.y, Time.deltaTime * acceleration);
            }
            else
            {
                velocity.z = Mathf.Lerp(velocity.z, 0, Time.deltaTime * acceleration);
            }
        }
        else // pre calculate air control velocity
        {
            if (inputs.x < -0.1 || inputs.x > 0.1)
            {
                velocity.x = Mathf.Lerp(velocity.x, inputs.x, Time.deltaTime * airControl);
            }
            
            if (inputs.y < -0.1 || inputs.y > 0.1)
            {
                velocity.z = Mathf.Lerp(velocity.z, inputs.y, Time.deltaTime * airControl);
            }
        }
        
        
        // move with calculated velocity
        Vector3 move = transform.right * velocity.x + transform.forward * velocity.z;
        controller.Move(move * speed * Time.deltaTime);
        
        
        // pre calculate jump velocity
        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // move with calculated jump velocity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    
    // fps mouse control function
    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        camera.transform.localRotation = Quaternion.Euler(xRotation, 0f,0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
