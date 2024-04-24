using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Ground check")]
    [SerializeField] private BoxCollider2D groundCheck;
    [SerializeField] private List<string> tags;
    private bool isGrounded = false;

    [Header("Movement")]
    [SerializeField] private float acceleration;
    [SerializeField] private int maxSpeed;
    private float movementSpeed = 0f;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
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
    }


    private void ApplyFriction()
    {
        GameObject groundMaterial = Physics2D.OverlapArea(groundCheck.bounds.min, groundCheck.bounds.max).gameObject;

        if (!tags.Contains(groundMaterial.tag)) 
        {
            isGrounded = false; 
            return;
        }

        isGrounded = true;
        if (groundMaterial.tag == "Ground" && !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)))
        {
            isGrounded = true;
            movementSpeed *= 0.9f;
        }
    }

    private void HandleMove()
    {
        if (Input.GetKey(KeyCode.D) && movementSpeed < maxSpeed)
        {
            movementSpeed += acceleration;
        }

        if (Input.GetKey(KeyCode.A) && movementSpeed > -maxSpeed)
        {
            movementSpeed -= acceleration;
        }

        if (movementSpeed < -maxSpeed || movementSpeed > maxSpeed)
        {
            movementSpeed = Mathf.RoundToInt(movementSpeed / maxSpeed) * maxSpeed;
        }

        rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
    }

    private void HandleJump()
    {
        Debug.Log($"{isGrounded} : {isJumping} : {jumpForce} : {jumpForceMultiplier}");

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
}