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
/// The following is used to create a cinematic camera that follows the player/ target. <br />
/// Attach this script to the main camera in the scene. <br />
/// The camera will follow the player/target with a smooth effect if enabled. <br />
/// </remarks>
public class CinematicCamera : MonoBehaviour
{
    [Header("Target Info")]
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 targetPosition;

    [Header("Camera Info")]
    [SerializeField] private Vector3 offset;
    [SerializeField] private float maxSmoothSpeed = 5f;
    [SerializeField, Range(0f, 100f)] private float deadZonePercentage = 1f;

    [Header("Camera Actions")]
    [SerializeField] private bool enableSmoothCamera = true;
    [SerializeField] private bool enableCursorHide = true;

    // Inner variables Do not expose
    private float deadZoneWidth;
    private Vector3 currentVelocity;
    private float adjustedSmoothSpeed;

    private void Start()
    {
        if (enableCursorHide)
            Cursor.visible = false;
    }

    private void Update()
    {
        CameraMovement();

        // Convert dead zone percentage to a corresponding value
        deadZoneWidth = Mathf.Clamp01(deadZonePercentage / 100f);
    }

    private void CameraMovement()
    {
        if (player != null)
        {
            // Calculate the target position by adding the offset to the player's position
            targetPosition = player.position + offset;
            targetPosition.z = transform.position.z;

            // Adjust smooth speed based on dead zone percentage
            adjustedSmoothSpeed = Vector3.Slerp(Vector3.zero, new Vector3(maxSmoothSpeed, 0f, 0f), 1f - deadZoneWidth).x;

            // Smoothly move the camera towards the target position with damping effect if enabled
            if (enableSmoothCamera)
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, adjustedSmoothSpeed, Mathf.Infinity, Time.deltaTime);
            }
            else
            {
                // Instantly move the camera to the target position without smoothing
                transform.position = targetPosition;
            }
        }
    }
}
