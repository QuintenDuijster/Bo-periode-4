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

    [Header("Climbing")]
    [SerializeField] private int climbingAcceleration;
    [SerializeField] private int maxClimbingSpeed;
    [SerializeField] private int maxClimbingDistance;
    private bool canClimb;
    private bool isClimbing;
    private float distanceClimbed;
    private Vector3 lastLocation;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float dragDownForce;
    private float jumpForceMultiplier = 1f;
    private bool isJumping = false;


    [Header("Keys")]
    [SerializeField] private KeyCode up;
    [SerializeField] private KeyCode down;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;
    [SerializeField] private KeyCode jump;
    [SerializeField] private KeyCode climb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleGravity();
        HandleFriction();
        HandleMove();
        HandleRotation();
        HandleJump();
        HandleWallHang();
    }

    private void HandleFriction()
    {
        if ((isGrounded || isClimbing) && !(Input.GetKey(right) || Input.GetKey(left)))
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.9f, rb.velocity.y);
        }
        if (isClimbing && !(Input.GetKey(up) || Input.GetKey(down)))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.9f);
        }
    }

    private void HandleMove()
    {
        movementDirection.x = 0f;
        movementDirection.y = 0f;

        if (Input.GetKey(up) && isClimbing)
        {
            movementDirection.y = 1;
        }
        if (Input.GetKey(left))
        {
            movementDirection.x = -1;
        }
        if (Input.GetKey(down) && isClimbing)
        {
            movementDirection.y = -1; 
        }
        if (Input.GetKey(right))
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

    private void HandleRotation()
    {
        if (movementDirection.x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (movementDirection.x > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    private void HandleJump()
    {
        if (Input.GetKey(jump) && (isGrounded || isClimbing))
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
                isClimbing = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * jumpForceMultiplier);
                jumpForceMultiplier = 1f;
            }
        }
    }

    private void HandleGravity()
    {
        if (!isGrounded && !isClimbing)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - dragDownForce);
        }
        else if (isGrounded && !isClimbing && rb.velocity.y < 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
    }

    private void HandleWallHang()
    {
		//Debug.Log($"{distanceClimbed} + {Vector3.Distance(transform.position, lastLocation)} : {isClimbing} : {isGrounded}");

		if (Input.GetKey(climb) && canClimb && distanceClimbed < maxClimbingDistance)
        {
            isClimbing = true;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            lastLocation = transform.position;
        }
        else if ((!canClimb && isClimbing) ||
                (Input.GetKey(climb) && isClimbing))
        {
            isClimbing = false;
        }

        if (isClimbing)
        {
            if (distanceClimbed < maxClimbingDistance)
            {
				distanceClimbed += Vector3.Distance(transform.position, lastLocation);
            }
            else
            {
				isClimbing = false;
			}

			lastLocation = transform.position;
		}

        if (isGrounded)
        {
            distanceClimbed = 0f;
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