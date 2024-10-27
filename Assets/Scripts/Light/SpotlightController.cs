using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightController : MonoBehaviour
{
    public Light2D flashlight;            // Reference to the flashlight Light2D component
    public Transform player;              // Reference to the player's Transform
    public float movementThreshold = 0.1f; // Minimum speed to detect as "moving"
    public float rotationSpeed = 5f;      // Speed of flashlight rotation

    private Vector3 lastPlayerPosition;   // To track player movement
    private Quaternion targetRotation;    // Target rotation for the flashlight

    void Start()
    {
        // Ensure flashlight is assigned
        if (flashlight == null)
        {
            flashlight = GetComponent<Light2D>();
            if (flashlight == null)
            {
                Debug.LogError("Light2D component not found. Please assign it in the Inspector.");
            }
        }

        // Initialize last position of the player
        lastPlayerPosition = player.position;
    }

    void Update()
    {
        // Ensure player is assigned
        if (player == null)
        {
            Debug.LogError("Player reference not assigned. Please assign it in the Inspector.");
            return;
        }

        // Check if directional keys are being pressed
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))) // Point flashlight north-west
        {
            targetRotation = Quaternion.Euler(0, 0, 45);
        }
        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))) // Point flashlight north-east
        {
            targetRotation = Quaternion.Euler(0, 0, -45);
        }
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))) // Point flashlight south-west
        {
            targetRotation = Quaternion.Euler(0, 0, 135);
        }
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))) // Point flashlight south-east
        {
            targetRotation = Quaternion.Euler(0, 0, -135);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // Point flashlight right
        {
            targetRotation = Quaternion.Euler(0, 0, -90);
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) // Point flashlight up
        {
            targetRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) // Point flashlight down
        {
            targetRotation = Quaternion.Euler(0, 0, 180);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // Point flashlight left
        {
            targetRotation = Quaternion.Euler(0, 0, 90);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // Point flashlight right
        {
            targetRotation = Quaternion.Euler(0, 0, -90);
        }
        else
        {
            // Position the flashlight at the player's position
            transform.position = player.position;

            // Check if the player is moving by comparing positions
            Vector3 playerMovement = player.position - lastPlayerPosition;
            bool isMoving = playerMovement.magnitude > movementThreshold;

            if (isMoving)
            {
                //Debug.Log("Player is moving.");
                // When moving, fix flashlight in the direction of movement
                float angle = Mathf.Atan2(playerMovement.y, playerMovement.x) * Mathf.Rad2Deg - 90f;
                targetRotation = Quaternion.Euler(0, 0, angle);
            }
            else
            {
                //Debug.Log("Player is stationary.");
                // When not moving, rotate flashlight toward the mouse
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;  // Keep it in 2D space

                Vector3 direction = mousePosition - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                targetRotation = Quaternion.Euler(0, 0, angle);
            }
        }

        // Smoothly rotate the flashlight towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // Update the last known position of the player
        lastPlayerPosition = player.position;
    }
}
