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
/// Controls (rotation and flipping) of a Gun and Player, based on the position of the mouse cursor.<br></br>
/// This script calculates the direction of the gun by tracking the position of the mouse cursor. <br></br>
/// It adjusts the gun's rotation to align with the cursor position and also modifies the player's orientation. <br></br>
/// Additionally, it sets the sorting order of the gun's sprite renderer to ensure proper rendering in the scene. <br></br>
/// </remarks>
public class GunPivotDirection : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;

    // Enumeration defining the sorting order of the gun relative to the character.
    private enum GunSortingOrder
    {
        Behind = -1,
        InFront = 2
    }

    private void Update()
    {
        SpriteRenderer gunSpriteRenderer = GetGunSpriteRenderer();

        // Calculate the direction from the gun to the mouse cursor.
        Vector3 difference = GetMouseWorldPosition() - transform.position;
        difference.Normalize();

        // Calculate the rotation angle and set the gun rotation accordingly.
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        // Adjust character flipping, gun sorting order, and local rotation based on the rotationZ angle.
        if (rotationZ < -90 || rotationZ > 90)
        {
            FlipCharacter(true);
            SetGunSortingOrder(GunSortingOrder.Behind);

            // Adjust local rotation based on player's global rotation.
            float playerRotationY = playerObject.transform.eulerAngles.y;
            if (playerRotationY == 0f)
            {
                transform.localRotation = Quaternion.Euler(180f, 0f, -rotationZ);
            }
            else if (playerRotationY == 180)
            {
                transform.localRotation = Quaternion.Euler(180, 180, -rotationZ);
            }
        }
        else
        {
            FlipCharacter(false);
            SetGunSortingOrder(GunSortingOrder.InFront);
        }
    }

    // Retrieve the SpriteRenderer component for the gun graphics.
    private SpriteRenderer GetGunSpriteRenderer()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            if (renderer.gameObject != gameObject)
            {
                return renderer;
            }
        }

        return null;
    }

    // Convert the mouse position to world coordinates.
    private Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Set the sorting order for all SpriteRenderers in the gun hierarchy.
    private void SetGunSortingOrder(GunSortingOrder sortingOrder)
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            renderer.sortingOrder = (int)sortingOrder;
        }
    }

    // Flip the character's orientation based on the 'flip' parameter.
    private void FlipCharacter(bool flip)
    {
        float rotationY = flip ? 180f : 0f;
        playerObject.transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
    }
}
