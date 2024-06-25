using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuilderController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float minAcceleration;
    [SerializeField] public float maxAcceleration;
    [SerializeField] public float maxSpeed;
    [SerializeField] public float decelerationFactor;
    [SerializeField] public float inheritanceFactor = .4f;
    [SerializeField] public float slowDownDistance;

    [Header("Detection")]
    [SerializeField] public float DetectionRangeX = 150;
    [SerializeField] public float DetectionRangeY = 15;
    [SerializeField] public float DetectionOffsetY = 15;


    [Header("Combat")]
    [SerializeField] public int damage;
    [SerializeField] public float throwCooldown;
    [SerializeField] public float throwSpeed;
    [SerializeField] public float projectileOffset;
    [SerializeField] public GameObject projectilePrefab;
    [SerializeField] public float throwAniLen;
    private float modUp = 5.0f;



    [SerializeField]public ContactFilter2D ContactFilter;
    internal Collider2D groundEdgeDetection;
    private GameObject groundChecker;
    public bool isGrounded => groundEdgeDetection.IsTouching(ContactFilter);

    private float throwCooldownTimer;

    internal GameObject player;
    internal Rigidbody2D rb;
    private Animator animator;
    internal Vector3 playerPos;
    private bool facingRight;


    // Start is called before the first frame update
    void Start()
    {
        groundChecker = transform.GetChild(0).gameObject;
        groundEdgeDetection = groundChecker.GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //debug
        Debug.Log(groundEdgeDetection.IsTouching(ContactFilter));
        Debug.Log(transform.GetChild(0).gameObject.transform.position.y);
        playerPos = player.transform.position;

        if (Mathf.Abs(playerPos.y - transform.position.y + DetectionOffsetY) < DetectionRangeY && Mathf.Abs(playerPos.x - transform.position.x) < DetectionRangeX)
        {
            Debug.DrawRay(transform.position, playerPos - transform.position);
        }





        if (transform.position.x < playerPos.x)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            facingRight = true;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            facingRight = false;
        }

        HandleAnimation();
    }

    private void HandleAnimation()
    {
        ResetAllTriggers();
        if (throwCooldownTimer < 0)
        {
            playerPos = player.transform.position;
            if (Mathf.Abs(playerPos.y - transform.position.y + DetectionOffsetY) < DetectionRangeY && Mathf.Abs(playerPos.x - transform.position.x) < DetectionRangeX)
            {
                animator.SetTrigger("Throw");
                StartCoroutine("throwProjectile");
                throwCooldownTimer = throwCooldown;
            }
        }
        else
        {
            throwCooldownTimer -= Time.deltaTime;
            if (isGrounded)
            {
                animator.SetTrigger("Walk");
                approachPlayer(rb, player);
            }
            else
            {
                animator.SetTrigger("Idle");
            }
        }
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

        //if (newVelocity.x < 0)
        //{
        //    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        //    facingRight = false;
        //}
        //else
        //{
        //    transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        //    facingRight = true;
        //}
        if (isGrounded)
        {
            if (MathF.Abs(newVelocity.x) > maxSpeed) newVelocity.x *= decelerationFactor;
            //if (MathF.Abs(newVelocity.y) > maxSpeed) newVelocity.y *= decelerationFactor;
            newVelocity.y = -MathF.Abs(newVelocity.y);
        }
        else newVelocity.x *= .1f;
   

        rb.velocity = newVelocity;

        //Debug.Log($"{math.abs(newVelocity.x)},  {math.abs(newVelocity.y)}  MovementDir" + movementDirection.x +"  "+ movementDirection.y);    
    }

    IEnumerator throwProjectile()
    {
        yield return new WaitForSeconds(throwAniLen);

        Vector3 spawningLocation = new Vector3(transform.position.x + (facingRight ? projectileOffset : -projectileOffset), transform.position.y, transform.position.z);
        GameObject projectile = Instantiate(projectilePrefab, spawningLocation, Quaternion.identity);
        Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();
        projectileRB.velocity = (playerPos + Vector3.up * modUp - transform.position).normalized * throwSpeed;
        //throwCooldownTimer = throwCooldown;
        ResetAllTriggers();
    }

    private void ResetAllTriggers()
    {
        animator.ResetTrigger("Throw");
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Walk");
    }
}
