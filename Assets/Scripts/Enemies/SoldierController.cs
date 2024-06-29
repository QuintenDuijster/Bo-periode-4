using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class SoldierController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] public float minAcceleration;
    [SerializeField] public float maxAcceleration;
    [SerializeField] public float maxSpeed;
    [SerializeField] public float decelerationFactor;
    [SerializeField] public float inheritanceFactor = .4f;
    [SerializeField] public float slowDownDistance;
    internal bool facingRight;
    private GameObject groundChecker;

    [Header("Detection")]
    [SerializeField] public float DetectionRangeX = 150;
    [SerializeField] public float DetectionRangeY = 15;
    [SerializeField] public float DetectionOffsetY = 15;


    [Header("Combat")]
    [SerializeField] public int damage;
    [SerializeField] public float stabCooldown;
    [SerializeField] public float warningTime;
    private float stabCooldownTimer = -1.0f;
    private GameObject stabChecker;
    private Collider2D stabCollider;

    private Vector3 playerPos;
    private Animator animator;
    private GameObject player;
    private Rigidbody2D rb;
    private bool shouldStab;
    private bool shouldWalk;

    // Start is called before the first frame update
    void Start()
    {
        groundChecker = transform.GetChild(0).gameObject;
        stabChecker = transform.GetChild(1).gameObject;
        stabCollider = stabChecker.GetComponent<Collider2D>();

        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();


        //Debug                  Debug.log();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider2D hitStab = Physics2D.OverlapBox(stabChecker.transform.position, stabChecker.transform.localScale, stabChecker.transform.rotation.z);
        Collider2D hitGround = Physics2D.OverlapBox(groundChecker.transform.position, groundChecker.transform.localScale, groundChecker.transform.rotation.z);

        if (hitStab != null)
        {
            shouldStab = hitStab.CompareTag("Player");
            //Debug.Log($"{hit.tag}, {shouldStab}");
        }

        if (hitGround != null)
        {
            playerPos = player.transform.position;
            if (Mathf.Abs(playerPos.y - transform.position.y + DetectionOffsetY) < DetectionRangeY && Mathf.Abs(playerPos.x - transform.position.x) < DetectionRangeX)
            {
                shouldWalk = hitGround.CompareTag("Ground");
            }
        }

        if (stabCooldownTimer > 0)
        {
            stabCooldownTimer -= Time.deltaTime;
            if (stabCooldown - stabCooldownTimer > warningTime)
            {
                if (shouldStab)
                {





                    //TODO: damage this gameobject, the player
                    Health quintenGo = hitStab.gameObject.GetComponent<Health>();
                    quintenGo.addHealth(-damage);




                    stabCooldownTimer = -1.0f;
                }
            }
        }
        else
        {
            ResetÀnimationTriggers();
            if (shouldWalk)
            {
                animator.SetTrigger("Walking");
                    approachPlayer(rb, player);
            }
            else
            {
                animator.SetTrigger("Idle");
            }

            if (shouldStab)
            {
                ResetÀnimationTriggers();
                animator.SetTrigger("Stab");
                stabCooldownTimer = stabCooldown;
            }
        }

    }

    private float enemyAcceleration;

    public void approachPlayer(Rigidbody2D rb, GameObject player)
    {

        Vector2 movementDirection = playerPos - transform.position;
        movementDirection.Normalize();
        //movementDirection.y *= 9 / 16;

        enemyAcceleration = Mathf.Lerp(minAcceleration, maxAcceleration, Vector3.Distance(playerPos, transform.position) * slowDownDistance);

        Vector2 newVelocity = rb.velocity * inheritanceFactor + movementDirection * enemyAcceleration;


        if (true)
        {
            if (MathF.Abs(newVelocity.x) > maxSpeed) newVelocity.x *= decelerationFactor;
            newVelocity.y = -MathF.Abs(newVelocity.y);
        }
        else
        {
            newVelocity.x *= .1f;
            newVelocity.y = -MathF.Abs(newVelocity.y);
        }


        rb.velocity = newVelocity;

        //Debug.Log($"{math.abs(newVelocity.x)},  {math.abs(newVelocity.y)}  MovementDir" + movementDirection.x +"  "+ movementDirection.y);    
    }
    private void ResetÀnimationTriggers()
    {
        animator.ResetTrigger("Stab");
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Walking");
    }

    private void DamageGameObject(Collider2D cooll)
    {
        throw new NotImplementedException();
    }
}
