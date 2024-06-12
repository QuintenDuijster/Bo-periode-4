using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoldierController : EnemyController
{

    [Header("Detection")]
    [SerializeField] float DetectionRangeX = 150;
    [SerializeField] float DetectionRangeY = 15;
    [SerializeField] float DetectionOffsetY = 15;

    [Header("Combat")]
    [SerializeField] int damage;
    [SerializeField] float reach;

    private Animator animator;

    Rigidbody2D rb;
    


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }


    void Update()
    {
        //player detection
        if (Mathf.Abs(playerPos.y - transform.position.y + DetectionOffsetY) < DetectionRangeY && Mathf.Abs(playerPos.x) < DetectionRangeX)
        {
            approachPlayer(rb, player);
        }

        //
        RaycastHit2D search;
        if (facingRight)
        {
            Debug.DrawRay(transform.position, Vector2.right * reach, Color.green);
            search = Physics2D.Raycast(transform.position, Vector2.right, reach);
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.left * reach, Color.green);
            search = Physics2D.Raycast(transform.position, Vector2.left, reach);
        }

        if (search && search.transform.gameObject.tag == "Player")
        {
            Health hp = search.transform.gameObject.GetComponent<Health>();
            if (hp != null)
            {
                Stab(hp);
            }
        }

        

        int layer;

        if (search)
        {
            layer = search.transform.gameObject.layer;
        }

        //RaycastHit2D search = Physics2D.Raycast(transform.position);
        //Vector2.Distance();
    }

    private void Stab(Health hp)
    {
        ResetÀnimationTriggers();
        animator.SetTrigger("Stab");
        hp.addHealth(-damage);
    }


    private void ResetÀnimationTriggers()
    {
        animator.ResetTrigger("Stab");
        animator.ResetTrigger("Idle");
    }

    
}
