
using UnityEngine.UI;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Health health;
    private HealthBar healtBar;
    [SerializeField] private GameObject hitArea;

    [Header("Movement")]
    [SerializeField] private float acceleration;
    [SerializeField] private int maxSpeed;
    private Vector2 movementDirection = new Vector2();

    [Header("Climbing")]
    [SerializeField] private int climbingAcceleration;
    [SerializeField] private int maxClimbingSpeed;
    [SerializeField] private int maxClimbingDistance;
    [SerializeField] private float climbingCooldown;
	private bool isClimbing;
    private float climbingCooldownTimer;
    private bool canClimb;
	private float distanceClimbed;
    private Vector3 lastLocation;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float dragDownForce;
    [SerializeField] private float extraJumpTime;
    private bool isJumping = false;
	private bool isGrounded = false;
    private float extraJumpTimer;

	[Header("Dash")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashCooldown;
    private float dashCooldownTimer;
    private bool canDash = true;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
    }

    private void FixedUpdate()
    {
		 GameObject hpObject = GameObject.FindGameObjectWithTag("HpDisplay");
		 hpObject.GetComponent<HealthBar>().SetHealth(health.health);
		 hpObject.GetComponent<HealthBar>().SetMaxHealth(health.health);

  //       text.text = health.health.ToString();
		HandleGravity();
        HandleFriction();
        HandleMove();
        HandleRotation();
        HandleJump();
        HandleClimb();
        HandleDash();
        
    }

    private void HandleFriction()
    {
        if ((isGrounded || isClimbing) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
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
			isClimbing = false;
			canClimb = false;
		}
        if (Input.GetKey(KeyCode.S) && isClimbing)
        {
            movementDirection.y = -1; 
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementDirection.x = 1;
			isClimbing = false;
			canClimb = false;
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
        if (Input.GetKey(KeyCode.Space))
        {
            if (isGrounded || isClimbing && !isJumping)
            {
				rb.velocity = new Vector2(rb.velocity.x, jumpForce);
				isJumping = true;

                if (isClimbing)
                {
                    isClimbing = false;
                    canClimb = false;
                    climbingCooldownTimer = climbingCooldown;
                }

				extraJumpTimer = extraJumpTime;
            }
			if (isJumping)
			{
				if (extraJumpTimer > 0)
				{
					rb.AddForce(new Vector2(0f, jumpForce * extraJumpTime));
					extraJumpTimer -= Time.deltaTime;
				}
				else
				{
					isJumping = false;
				}
			}
		}
        else
        {
			isJumping = false;
		}
    }

    private void HandleClimb()
    {
		if (climbingCooldownTimer > 0)
		{
			climbingCooldownTimer -= Time.deltaTime;
		}
		else
		{
			canClimb = true;
		}

        if (isClimbing)
        {
			if (distanceClimbed > maxClimbingDistance)
			{
				isClimbing = false;
				canClimb = false;
				climbingCooldownTimer = climbingCooldown;
            }
            else if (lastLocation != null)
			{
                distanceClimbed += Vector2.Distance(transform.position, lastLocation);
            }
		}

        lastLocation = transform.position;
	}

    private void HandleDash()
    {
		if (dashCooldownTimer > 0)
		{
			dashCooldownTimer -= Time.deltaTime;
		}
		else
		{
			canDash = true;
		}

        if (Input.GetKey(KeyCode.J) && canDash)
        {
            Vector2 force = new Vector2(dashForce, 0f);

            if (transform.rotation == Quaternion.Euler(0f, 0f, 0f))
            {
                force = -force;   
            }

            rb.AddRelativeForce(force);

            dashCooldownTimer = dashCooldown;
            canDash = false;
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

	private void OnCollisionStay2D(Collision2D collision)
    {
		Vector3 collisionNormal = collision.contacts[0].normal;

		if (!isClimbing && (collisionNormal.x == 1 || collisionNormal.x == -1) && canClimb)
		{
            rb.velocity = new Vector3(0f, 0f, 0f);
			isClimbing = true;
		}

        if (collisionNormal.y == 1)
        {
            canClimb = true;
            climbingCooldownTimer = 0;
            distanceClimbed = 0f;
			isGrounded = true;
		}
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isClimbing = false;
		isGrounded = false;
    }
}