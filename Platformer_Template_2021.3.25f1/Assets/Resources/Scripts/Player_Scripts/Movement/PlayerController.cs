using UnityEngine;

/// <summary>
///
/// License:
/// Copyrighted to Brian "VerzatileDev" Lätt ©2024.
/// Do not copy, modify, or redistribute this code without prior consent.
/// All rights reserved.
///
/// </summary>
/// <remarks>
/// Player Movement / Jump / Grounded Check
/// </remarks>
public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    private class Properties
    {
        [Header("Player Components")]
        public CharacterController controller;
        public Transform groundCheck;
        public LayerMask groundLayer;

        [Header("Player Properties")]
        public Vector3 direction;
        public float playerSpeed = 5;
        public float playerSprintSpeed = 7;
        public float jumpForce = 10;
        public float gravityForce = -4.8f;
        public float groundCheckDistance = 1.2f;

        [Header("Debug Components")]
        public bool enableGroundCheckDebug = true;
        public bool enableDirectionDebug = true;

        // Direction Elements
        public GameObject playerDirectionDebug;
        public GameObject playerAimDirectionDebug;
    }
    [SerializeField] private Properties properties = new Properties();

    private bool isGrounded; // Flag to determine if the player is grounded.
    private bool isSprinting; // Flag to determine if the player is sprinting.

    void Update()
    {
        CheckGrounded();
        PlayerMovement();
        PlayerJump();
        CheckSprintInput();
        TogglePlayerDebug(); // Enable/Disable Debug If requested
    }

    void PlayerMovement()
    {
        // Get the input for the player's movement.
        float left_right_input = Input.GetAxis("Horizontal");
        // Determine current speed based on sprinting state.
        float currentSpeed = isSprinting ? properties.playerSprintSpeed : properties.playerSpeed;
        // Calculate movement direction.
        properties.direction.x = left_right_input * currentSpeed;
        // Move the player.
        properties.controller.Move(properties.direction * Time.deltaTime);
    }

    void CheckGrounded()
    {
        // Perform a raycast to check if the player is grounded.
        // See the Element Attached to Under player, Start from middle to the given Lenght
        // Note the Raycast will be bigger than the distance, which is defined as groundCheckDistance
        isGrounded = Physics.Raycast(properties.groundCheck.position, Vector3.down, out _, properties.groundCheckDistance, properties.groundLayer);
    }

    void PlayerJump()
    {
        // Jump only when grounded & Defined in settings
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            properties.direction.y = properties.jumpForce;
        }
        else if (!isGrounded)
        {
            properties.direction.y += properties.gravityForce * Time.deltaTime;
        }
    }

    void CheckSprintInput()
    {
        // Toggle sprint on/off with Left Shift ( change in Project Settings > Input Manager )
        if (Input.GetButton("Sprint"))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
    }

    void TogglePlayerDebug()
    {
        // Draw a ray to visualize the ground check in the Scene view
        if (properties.enableGroundCheckDebug)
        {
            Debug.DrawRay(properties.groundCheck.position, Vector3.down * properties.groundCheckDistance, Color.blue);
        }

        // Enable/Disable Debug Elements
        properties.playerDirectionDebug.SetActive(properties.enableDirectionDebug);
        properties.playerAimDirectionDebug.SetActive(properties.enableDirectionDebug);
    }
}
