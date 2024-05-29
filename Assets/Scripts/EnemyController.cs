using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{


    [Header("Movement")]
    [SerializeField] float minAcceleration;
    [SerializeField] float maxAcceleration;
    [SerializeField] float maxSpeed;
    [SerializeField] float decelerationFactor;
    [SerializeField] float inheritanceFactor = .4f;
    [SerializeField] float slowDownDistance;
    private bool facingRight;

    Rigidbody2D rb;
    private GameObject player;
    private Vector3 playerPos;
    private float enemyAcceleration;
    private bool isJumping = false;
    private bool canJump = false;

    void Start()
    {
        //Debug.DrawLine(new Vector2(0.0f, 0.0f), new Vector2(140.0f, 190.0f),  Color.red, 13247.4f ,false);
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        // makes reading easier by removing the p.go.t.pos boilerplate
        playerPos.x = player.gameObject.transform.position.x;
        playerPos.y = player.gameObject.transform.position.y;
    }
    void Update()
    {
        playerPos.x = player.gameObject.transform.position.x;
        playerPos.y = player.gameObject.transform.position.y;

        approachPlayer();
    }
    public void approachPlayer()
    {
        Vector2 movementDirection = playerPos - transform.position;
        movementDirection.Normalize();
        movementDirection.y *= 9 / 16;

        enemyAcceleration = Mathf.Lerp(minAcceleration, maxAcceleration, Vector3.Distance(playerPos, transform.position) * slowDownDistance);

        Vector2 newVelocity = rb.velocity * inheritanceFactor + movementDirection * enemyAcceleration;

        if (newVelocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            facingRight = false;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            facingRight = true;
        }
        if (edgeDetection())
        {
            if (math.abs(newVelocity.x) > maxSpeed) newVelocity.x *= decelerationFactor;
            if (math.abs(newVelocity.y) > maxSpeed) newVelocity.y *= decelerationFactor;
        }
        else newVelocity.x *= .1f;

        rb.velocity = newVelocity;

        //Debug.Log($"{math.abs(newVelocity.x)},  {math.abs(newVelocity.y)}  MovementDir" + movementDirection.x +"  "+ movementDirection.y);
    }

    bool edgeDetection()
    {
        if (facingRight)
        {
            Debug.DrawRay(transform.position, new Vector2(1f, -5f));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(1f, -5f));
            return hit;
        }
        else
        {
            Debug.DrawRay(transform.position, new Vector2(-1f, -5f));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(-1f, -5f));
            return hit;
        }
    }
}