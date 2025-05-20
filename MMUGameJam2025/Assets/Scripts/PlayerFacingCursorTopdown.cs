using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFacingCursorTopdown : MonoBehaviour
{
    [SerializeField] private FollowPlayer followPlayerScript;
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition.z = Mathf.Abs(followPlayerScript.offset.z);

        // Step 1: Get mouse position in world space
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosition);

        // Step 2: Get direction from character to mouse
        Vector2 direction = (mousePosWorld - transform.position);

        // Step 3: Calculate angle (in degrees)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Step 4: Apply rotation around Z axis (for 2D top-down)
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void OnDrawGizmosSelected()
    {
        // Get angle from rotation
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;

        // Calculate direction vector from angle
        Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);

        // Draw a red line (from character forward)
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + direction * 2f);
    }
}
