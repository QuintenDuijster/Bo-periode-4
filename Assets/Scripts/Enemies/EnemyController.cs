using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{


    
    internal Collider2D groundEdgeDetection;

    private GameObject groundChecker;


    void Start()
    {
        groundChecker = transform.GetChild(0).gameObject;

        groundEdgeDetection = groundChecker.GetComponent<Collider2D>();
        //Debug.DrawLine(new Vector2(0.0f, 0.0f), new Vector2(140.0f, 190.0f),  Color.red, 13247.4f ,false);
    }
    //public void approachPlayer(Rigidbody2D rb, GameObject player)
    //{

    //    //Debug.Log("approachPlayer.playerPos  : " + playerPos);
    //    Vector2 movementDirection = playerPos - transform.position;
    //    movementDirection.Normalize();
    //    movementDirection.y *= 9 / 16;

    //    enemyAcceleration = Mathf.Lerp(minAcceleration, maxAcceleration, Vector3.Distance(playerPos, transform.position) * slowDownDistance);

    //    Vector2 newVelocity = rb.velocity * inheritanceFactor + movementDirection * enemyAcceleration;

    //    if (newVelocity.x < 0)
    //    {
    //        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    //        facingRight = false;
    //    }
    //    else
    //    {
    //        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
    //        facingRight = true;
    //    }
    //    if (edgeDetection())
    //    {
    //        if (MathF.Abs(newVelocity.x) > maxSpeed) newVelocity.x *= decelerationFactor;
    //        if (MathF.Abs(newVelocity.y) > maxSpeed) newVelocity.y *= decelerationFactor;
    //    }
    //    else newVelocity.x *= .1f;

    //    rb.velocity = newVelocity;

    //    //Debug.Log($"{math.abs(newVelocity.x)},  {math.abs(newVelocity.y)}  MovementDir" + movementDirection.x +"  "+ movementDirection.y);    
    //}

    //bool edgeDetection()
    //{
    //    if (facingRight)
    //    {
    //        Debug.DrawRay(transform.position, new Vector2(1f, -5f));
    //        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(1f, -5f));
    //        //Debug.Log(hit);
    //        return hit;
    //    }
    //    else
    //    {
    //        Debug.DrawRay(transform.position, new Vector2(-1f, -5f));
    //        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(-1f, -5f));
    //        //Debug.Log(hit);
    //        return hit;
    //    }
    //}

    bool edgeDectectionWCol()
    {
        if (groundEdgeDetection != null)
        {
            groundChecker = transform.GetChild(0).gameObject;
            groundEdgeDetection = groundChecker.GetComponent<Collider2D>();
        }
        //if (groundEdgeDetection.IsTouchingLayer());
        return false;
    }

    
}