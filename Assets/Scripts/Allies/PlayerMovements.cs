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
    
    public Vector3 velocity;
    private bool isGrounded;
    
    public Transform playerBody;
    public Camera camera;

    private float xRotation = 0f;
    
    
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        MouseLook();
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector2 inputs;
        inputs.x = Input.GetAxis("Horizontal");
        inputs.y = Input.GetAxis("Vertical");
        
        if (isGrounded)
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
        else
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
        
        inputs.Normalize();
        
        Vector3 move = transform.right * velocity.x + transform.forward * velocity.z;
        
        controller.Move(move * speed * Time.deltaTime);
        
        
        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
    
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
