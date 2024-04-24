using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Rigidbody2D rb;

    private bool isGrounded = false;

    [Header("Movement")]
    [SerializeField] private float acceleration;
    [SerializeField] private int maxSpeed;
    private Vector2 movementDirection = new Vector2();

    [Header("Movement")]
    [SerializeField] private int climbingAcceleration;
    [SerializeField] private int maxClimbingSpeed;
    private bool canClimb;
    private bool isClimbing;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float dragDownForce;
    private float jumpForceMultiplier = 1f;
    private bool isJumping = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplyFriction();
        HandleMove();
        HandleJump();
        HandleWallHang();
    }

    private void ApplyFriction()
    {
        if ((isGrounded || isClimbing) && !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)))
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.9f, rb.velocity.y);
        }
        if (isClimbing && !(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.9f);
        }
    }

    private void HandleMove()
    {
        movementDirection.x = 0f;
        movementDirection.y = 0f;

        if (Input.GetKey(KeyCode.W) && isClimbing)
        {
            movementDirection.y = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementDirection.x = -1;
        }
        if (Input.GetKey(KeyCode.S) && isClimbing)
        {
            movementDirection.y = -1; 
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementDirection.x = 1; 
        }

        Vector2 accelerationVector = movementDirection * (isClimbing ? climbingAcceleration : acceleration);
        Vector2 newVelocity = rb.velocity + accelerationVector;

        float maxSpeedToUse = isClimbing ? maxClimbingSpeed : maxSpeed;
        newVelocity.x = Mathf.Clamp(newVelocity.x, -maxSpeedToUse, maxSpeedToUse);
        newVelocity.y = Mathf.Clamp(newVelocity.y, -maxSpeedToUse, maxSpeedToUse);

        rb.velocity = newVelocity;
    }

    private void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space) && (isGrounded || isClimbing))
        {
            isJumping = true;
            if (jumpForceMultiplier < 2f)
            {
                jumpForceMultiplier += Time.fixedDeltaTime;
            }
            else
            {
                jumpForceMultiplier = 2f;
            }
        }
        else
        {
            if (isJumping)
            {
                isJumping = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * jumpForceMultiplier);
                jumpForceMultiplier = 1f;

                if (isClimbing)
                {
                    isClimbing = false;
                    rb.gravityScale = 1f;
                }
            }
        }

        if (!isGrounded && !isClimbing)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - dragDownForce);
        }
        else if(isGrounded && !isClimbing && rb.velocity.y < 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
    }

    private void HandleWallHang()
    {
        if (Input.GetKey(KeyCode.E) && canClimb)
        {
            isClimbing = true;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
        else if ((!canClimb && isClimbing) ||
                (Input.GetKey(KeyCode.E) && isClimbing))
        {
            isClimbing = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Climbable":
                canClimb = true;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Climbable":
                canClimb = false;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                isGrounded = true;
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                isGrounded = false;
                break;
        }
    }
}