using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouseClick : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Update()
    {
        // Detect left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if ray hits something with a collider
            if (Physics.Raycast(ray, out hit))
            {
                targetPosition = hit.point;
                isMoving = true;
            }
        }

        // Move towards the target position if clicked
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
            }
        }
    }
}
