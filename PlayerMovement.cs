using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SampleScript : MonoBehaviour
{
    public float sensX = 100f;
    public float sensY = 100f;
    public float turnSpeed;
    float a;

    public Transform orientation;
    public playerMove playerGuy;

    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

   void Update()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x * sensX * Time.deltaTime;
        float mouseY = mouseDelta.y * sensY * Time.deltaTime;

        // --- Horizontal Rotation (Y-axis) ---
        yRotation += mouseX;

        if(playerGuy.inWheelchair){
            playerGuy.controller.stepOffset = 0.01f;
            playerGuy.controller.slopeLimit = 30f;
            float x = Input.GetAxis("Horizontal");
            a+=x*turnSpeed;
            yRotation += x*turnSpeed;
            yRotation = Mathf.Clamp(yRotation, -75f +a, 75f +a);  //X ROTATION CLAMP
            xRotation = Mathf.Clamp(xRotation, 0f, 0f); //Y ROTATION CLAMP

            if (calcMouseSpeed()>300)
        {
            // Mathf.Clamp(yRotation, yRotation, yRotation);
            playerGuy.controller.Move(transform.forward*0.1f);
        }
        } else{
            // --- Vertical Rotation (X-axis) ---
            playerGuy.controller.stepOffset = 0.5f;
            playerGuy.controller.slopeLimit = 65f;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            }

        // Apply rotations
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        
    }
    
    private float calcMouseSpeed()
    {
        float mouseSpeed = 0;
        float deltaY = Input.GetAxisRaw("Mouse Y");

        // 2. Create a movement vector (distance moved in pixels)
        Vector2 mouseDelta = new Vector2(0f, deltaY);
        
        // 3. Calculate the actual pixel distance moved
        float distanceMoved = mouseDelta.magnitude;
        
        // 4. Calculate Speed: Distance / Time
        // Time.deltaTime is the time elapsed since the last frame
        if (Time.deltaTime > 0)
        {
            mouseSpeed = distanceMoved / Time.deltaTime;
        }

        // Display the result in the console
        return mouseSpeed;
    }

}

