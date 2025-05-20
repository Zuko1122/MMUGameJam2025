using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFacingMovementTopdown : MonoBehaviour
{
    private FollowPlayer followPlayerScript;
    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector3 mousePosition = Input.mousePosition;
        ////followPlayerScript.offset.z
        //mousePosition.z = 100;

        //created facing cursor script to rotate the player towards player movement
        // Step 1: Get mouse position in world space
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Step 2: Get direction from character to mouse
        Vector2 direction = (mousePosWorld - transform.position);

        // Step 3: Calculate angle (in degrees)
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Step 4: Apply rotation around Z axis (for 2D top-down)
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
