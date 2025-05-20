using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// OOP Inheritance
//Its monobehavior as didnt inherit functions or varaibles from another class
public class PlayerMovementTopDown2D : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float smoothTime = 0.1f;

    // OOP Encapsulation
    // Didnt allow other scripts to access/change the value so thats why its private, while serializefield allows it to be tweaked in the unity inspector
    [SerializeField] private Rigidbody2D rb;

    // set vector2 to zero
    private Vector2 currentVelocity = Vector2.zero;

    // maybe use fixedUpdate
    // Update is called once per frame
    void Update()
    {
        //For most common scenarios, especially action games where the user's input should have a continuous effect on an in-game character,
        //Polling is usually simpler and easier to implement.
        //poll the current value of an Action
        //var move = m_Controls.gameplay.move.ReadValue<Vector2>();
        //Move(move);

        // Input is a legacy system
        float moveHori = Input.GetAxisRaw("Horizontal");
        float moveVer = Input.GetAxisRaw("Vertical");
        //Debug.Log(moveHori);
        //Debug.Log(moveVer);

        //rb.velocity = new Vector2(moveHori * moveSpeed, moveVer * moveSpeed);

        // velocity already has time delta time applied to it, so we don't need to multiply by Time.deltaTime again
        Vector2 targetPosition = new Vector2(moveHori * moveSpeed, moveVer * moveSpeed);

        // rb.velocity is rate of change of rigidbody position with respect to time
        // current position / velocity, target position, current velocity (value is modified by the function every time you call it.), smooth time (Approximately the time it will take to reach the target)
        // smoothdamp accepted velocity as the first value because we are returning a new velocity value, smoothly adjust the current velocity until it reaches the target velocity
        // smoothdamp smooths any vector (position, velocity, acceleration, scale) over tiime
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetPosition, ref currentVelocity, smoothTime);
        //Debug.Log(rb.velocity);

        //Use Lerp when you want fixed-rate interpolation or very basic smoothing.
        //Use SmoothDamp when you want natural-feeling, gradual motion that slows down near the target — especially for movement, UI, or camera.
    }

    //private void Move(Vector2 direction)
    //{
    //    // Check if the direction is small enough to ignore
    //    // this is a common optimization to avoid unnecessary calculations
    //    // Need testing??
    //    //if (direction.sqrMagnitude < 0.01)
    //    //    return;

    //    // movement is scaled properly and is smooth and consistent across different frame rates
    //    // so if framerates are low, the movement will be slower, thats why use delta time
    //    var scaledMoveSpeed = moveSpeed * Time.deltaTime;

    //    // For simplicity's sake, we just keep movement in a single plane here. Rotate
    //    // direction according to world Y rotation of player.

    //    //Quaternion is a struct that represents a rotation in 3D space
    //    //Euler angles are a way to represent 3D rotations using three angles (pitch, yaw, roll)
    //    //Quaternion creates rotation from euler angles
    //    //Unity uses quaternions for rotation because they avoid gimbal lock and handle smooth interpolation better than Euler angles.
    //    // var move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);

    //    // Vector2 is a struct that represents a point in 2D space
    //    var move = new Vector2(direction.x, direction.y);
    //    // smooth, incremental movement / apply movement relative to facing direction.
    //    transform.Translate(move * scaledMoveSpeed);

    //    //transform.position is for teleporting objects
    //}
}
