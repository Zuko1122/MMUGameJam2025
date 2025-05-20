using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float smoothTime = 0.1f;
    public float jumpForce = 5f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    public float dashForce = 5f;
    // Time allowed between taps to count as a double tap
    public float doubleTapTime = 0.5f; 
    private float lastLeftTap = -1f;
    private float lastRightTap = -1f;

    private bool isGrounded = true;


    [SerializeField] private Rigidbody2D rb;

    // set vector2 to zero
    private Vector2 currentVelocity = Vector2.zero;

    // maybe use fixedUpdate
    // Update is called once per frame
    void Update()
    {
        jump();
        dash();

        float moveHori = Input.GetAxisRaw("Horizontal");

        // velocity already has time delta time applied to it, so we don't need to multiply by Time.deltaTime again
        Vector2 targetPosition = new Vector2(moveHori * moveSpeed, rb.velocity.y);

        // rb.velocity is rate of change of rigidbody position with respect to time
        // current position / velocity, target position, current velocity (value is modified by the function every time you call it.), smooth time (Approximately the time it will take to reach the target)
        // smoothdamp accepted velocity as the first value because we are returning a new velocity value, smoothly adjust the current velocity until it reaches the target velocity
        // smoothdamp smooths any vector (position, velocity, acceleration, scale) over tiime
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetPosition, ref currentVelocity, smoothTime);
        //Debug.Log(rb.velocity);

        //Use Lerp when you want fixed-rate interpolation or very basic smoothing.
        //Use SmoothDamp when you want natural-feeling, gradual motion that slows down near the target — especially for movement, UI, or camera.

    }

    void jump()
    {
        // Check if player is grounded
        // Physics2D.OverlapCircle, check if the circle overlap with the collider of object that is within groundlayer
        // first peri is centre of the circle, second is radius, third is the layer mask
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        //fyi, remember to set contraints on the rigidbody2d component to freeze rotation on the z axis, so that the player doesn't rotate when jumping or colliding with objects

        // Jump when grounded and spacebar pressed
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Reset Y velocity to prevent double-jump boosting
            //if you jump while still falling, the jump height might be shorter or feel mushy, because Unity adds the jump force on top of the existing velocity.
            //rb.velocity = new Vector2(rb.velocity.x, 0); // Reset Y velocity to prevent double-jump boosting

            // Apply an instant force upward (0, 1) multiplied by how strong I want the jump to be.
            // ForceMode2D.Impulse,  This mode depends on the mass of rigidbody so more force must be applied to move higher-mass objects the same amount as lower-mass objects.
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

    }

    void dash()
    {
        //int dashCount = 0;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if((Time.time - lastLeftTap) < doubleTapTime)
            {
                dashToward(Vector2.left);
                lastLeftTap = -1f; // Reset the last tap time to prevent further dashes
                // using -1f instead 0f to prevent false positive like if game started and player pressed left key then directly trigger the dash without double tap
            }
            else
            {
                lastLeftTap = Time.time;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if ((Time.time - lastRightTap) < doubleTapTime)
            {
                dashToward(Vector2.right);
                lastRightTap = -1f; // Reset the last tap time to prevent further dashes
                // using -1f instead 0f to prevent false positive like if game started and player pressed left key then directly trigger the dash without double tap
            }
            else
            {
                lastRightTap = Time.time;
            }
        }
    }

    void dashToward(Vector2 direction)
    {
        //rb.velocity = Vector2.zero; // Optional: reset velocity
        rb.AddForce(direction * dashForce, ForceMode2D.Impulse);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
