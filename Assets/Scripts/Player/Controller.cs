using Unity.VisualScripting;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UIElements;

public class Controller : MonoBehaviour
{
    [SerializeField] private float acceleration;
    [SerializeField] private int maxSpeed;

    private Rigidbody2D rb;
    private float movementSpeed = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplyFriction();
        HandleMove();
    }


    private void ApplyFriction()
    {
        GameObject groundMaterial = Physics2D.OverlapCircle(transform.position, 1f).gameObject;

        if (groundMaterial != null)
        {
            if (groundMaterial.tag == "Ground" && !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)))
            {
                movementSpeed *= 0.9f;
            }
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
}