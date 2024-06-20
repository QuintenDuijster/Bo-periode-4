using System;
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

    [Header("Detection")]
    [SerializeField] public float DetectionRangeX = 150;
    [SerializeField] public float DetectionRangeY = 15;
    [SerializeField] public float DetectionOffsetY = 15;

    [Header("Combat")]
    [SerializeField] public int damage;
    [SerializeField] public float reach;
    [SerializeField] public float stabCooldown;
    private float stabCooldownTimer = -1.0f;

    private Animator animator;
    internal GameObject player;
    internal Vector3 playerPos;
    private GameObject groundChecker;
    internal Collider2D groundEdgeDetection;
    Rigidbody2D rb;

    void Start()
    {
        groundChecker = transform.GetChild(0).gameObject;

        groundEdgeDetection = groundChecker.GetComponent<Collider2D>();


        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        //Debug.Log(stabCooldownTimer);
        if (stabCooldownTimer > 0)
        {
            stabCooldownTimer -= Time.deltaTime;
            if (stabCooldown - stabCooldownTimer > 2.0f)
            {
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
                Health hp = search.transform.gameObject.GetComponent<Health>();
                if (hp != null)
                {
                    hp.addHealth(-damage);
                }
            }
            else
            {
                //Debug.Log((Mathf.Abs(playerPos.y - transform.position.y + DetectionOffsetY) < DetectionRangeY) + " : " + (Mathf.Abs(playerPos.x - transform.position.x) < DetectionRangeX));
                //player detection
                playerPos.x = player.transform.position.x;
                playerPos.y = player.transform.position.y;
                if (Mathf.Abs(playerPos.y - transform.position.y + DetectionOffsetY) < DetectionRangeY && Mathf.Abs(playerPos.x - transform.position.x) < DetectionRangeX)
                {
                    ResetÀnimationTriggers();
                    animator.SetTrigger("Walking");
                    approachPlayer(rb, player);
                }
                else
                {
                    ResetÀnimationTriggers();
                    animator.SetTrigger("Idle");
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
                    ResetÀnimationTriggers();
                    animator.SetTrigger("Stab");
                    stabCooldownTimer = stabCooldown;
                }
            }


            //int layer;

            //if (search)
            //{
            //    layer = search.transform.gameObject.layer;
            //}

            //RaycastHit2D search = Physics2D.Raycast(transform.position);
            //Vector2.Distance();
        }
    }
    private void ResetÀnimationTriggers()
    {
        animator.ResetTrigger("Stab");
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Walking");
    }



    private float enemyAcceleration;
    public void approachPlayer(Rigidbody2D rb, GameObject player)
    {

        //Debug.Log("approachPlayer.playerPos  : " + playerPos);
        Vector2 movementDirection = playerPos - transform.position;
        movementDirection.Normalize();
        //movementDirection.y *= 9 / 16;

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
            if (MathF.Abs(newVelocity.x) > maxSpeed) newVelocity.x *= decelerationFactor;
            if (MathF.Abs(newVelocity.y) > maxSpeed) newVelocity.y *= decelerationFactor;
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
            //Debug.Log(hit.);
            return hit;
        }
        else
        {
            Debug.DrawRay(transform.position, new Vector2(-1f, -5f));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(-1f, -5f));
            //Debug.Log(hit);
            return hit;
        }
    }
}
