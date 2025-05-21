using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFacingCursorTopdown : MonoBehaviour
{
    [SerializeField] private FollowPlayer followPlayerScript;
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = calculateAngle();

        // Atan2 is better than Atan because Atan if division by zero when x = 0 → crash or NaN. And cannot deferentiate the quadrant (1, 1) and (-1, -1)
        // Radians is the length of the arc between two points on a circle
        // Mathf.Rad2Deg turn radians into degrees
        // Step 3: Calculate angle (in degrees)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Step 4: Apply rotation around Z axis (for 2D top-down)
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private Vector2 calculateAngle()
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition.z = Mathf.Abs(followPlayerScript.offset.z);

        // Step 1: Get mouse position in world space
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mousePosition);

        // transform.position = (2, 2)
        // mousePosWorld = (5, 5)
        // then direction = (3, 3), which is diagonally up right
        // Step 2: Get direction from character to mouse
        return (mousePosWorld - transform.position);
    }

    void OnDrawGizmosSelected()
    {
        Vector2 direction = calculateAngle();

        // drawline (from, to)
        // normalized * 2f keeps it a reasonable length, like 2 units long
        // Draw a red line (from character forward)
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)direction.normalized * 30f);
    }
}
