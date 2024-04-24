using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    #region variables
    private Rigidbody2D rb;

    #region veriables for ground check
    [Header("Ground check")]
    [SerializeField] private List<string> tags;
    private BoxCollider2D groundCheck;
    private bool isGrounded = false;
    #endregion

    #region veriables for movement mechanic
    [Header("Movement")]
    [SerializeField] private float acceleration;
    [SerializeField] private int maxSpeed;
    private float movementSpeed_X = 0f;
    #endregion

    #region veriables for jump mechanic
    [Header("Jump")]
    [SerializeField] private float jumpForce;
    private float jumpForceMultiplier = 1f;
    private bool isJumping = false;
    #endregion

    #region veriables for wall hang mechanic
    [Header("Climbing")]
    [SerializeField] private int maxClimbingSpeed;
    [SerializeField] private int climbingAcceleration;
    private float movementSpeed_Y = 0f;
    private bool canClimb;
    private bool isClimbing;
    #endregion
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<BoxCollider2D>();
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
        if (isGrounded && !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)))
        {
            movementSpeed_X *= 0.9f;
        }
    }

    private void HandleMove()
    {
        if (Input.GetKey(KeyCode.W) && movementSpeed_Y < maxClimbingSpeed)
        {
            movementSpeed_Y += climbingAcceleration;
        }

        if (Input.GetKey(KeyCode.A) && movementSpeed_X > -maxSpeed)
        {
            movementSpeed_X -= acceleration;
        }

        if (Input.GetKey(KeyCode.S) && movementSpeed_Y > -maxClimbingSpeed)
        {
            movementSpeed_Y -= climbingAcceleration;
        }

        if (Input.GetKey(KeyCode.D) && movementSpeed_X < maxSpeed)
        {
            movementSpeed_X += acceleration;
        }

        if (isClimbing)
        {
            if (movementSpeed_X < -maxClimbingSpeed || movementSpeed_X > maxClimbingSpeed)
            {
                movementSpeed_X = Mathf.RoundToInt(movementSpeed_X / maxClimbingSpeed) * maxClimbingSpeed;
            }

            if (movementSpeed_Y < -maxClimbingSpeed || movementSpeed_Y > maxClimbingSpeed)
            {
                movementSpeed_Y = Mathf.RoundToInt(movementSpeed_Y / maxClimbingSpeed) * maxClimbingSpeed;
            }

            rb.velocity = new Vector2(movementSpeed_X, movementSpeed_Y);
        }
        else
        {
            if (movementSpeed_X < -maxSpeed || movementSpeed_X > maxSpeed)
            {
                movementSpeed_X = Mathf.RoundToInt(movementSpeed_X / maxSpeed) * maxSpeed;
            }

            rb.velocity = new Vector2(movementSpeed_X, rb.velocity.y);
        }
    }

    private void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
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
            }
        }
    }

    private void HandleWallHang()
    {
        if (Input.GetKey(KeyCode.E) && canClimb)
        {
            isClimbing = true;
            rb.gravityScale = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Climbable": canClimb = true;
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