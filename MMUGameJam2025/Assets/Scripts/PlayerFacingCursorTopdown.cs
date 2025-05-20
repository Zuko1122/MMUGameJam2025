using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFacingCursorTopdown : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    // Update is called once per frame
    void FixedUpdate()
    {
        // Step 1: Get mouse position in world space
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        Debug.Log(mousePos);
        //Debug.Log(transform.position);

        //// Step 2: Get direction from character to mouse
        //Vector2 direction = (mousePos - transform.position);

        //// Step 3: Calculate angle (in degrees)
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //// Step 4: Apply rotation around Z axis (for 2D top-down)
        //transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
