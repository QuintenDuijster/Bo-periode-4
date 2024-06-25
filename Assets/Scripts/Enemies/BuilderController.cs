using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuilderController : MonoBehaviour
{

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

    private float throwCooldownTimer;

    internal GameObject player;
    internal Rigidbody2D rb;
    private Animator animator;
    internal Vector3 playerPos;
    private bool facingRight;


    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //debug
        playerPos = player.transform.position;

        if (Mathf.Abs(playerPos.y - transform.position.y + DetectionOffsetY) < DetectionRangeY && Mathf.Abs(playerPos.x - transform.position.x) < DetectionRangeX)
        {
            Debug.DrawRay(transform.position, playerPos - transform.position);
        }




        if (throwCooldownTimer < 0)
        {
            playerPos = player.transform.position;
            if (Mathf.Abs(playerPos.y - transform.position.y + DetectionOffsetY) < DetectionRangeY && Mathf.Abs(playerPos.x - transform.position.x) < DetectionRangeX)
            {
                ResetAllTriggers();
                animator.SetTrigger("Throw");
                StartCoroutine("throwProjectile");
                throwCooldownTimer = throwCooldown;

                //Debug.Log("player ofund");
                //Debug.DrawRay(transform.position, playerPos - transform.position);
                //RaycastHit2D hit = Physics2D.Raycast(transform.position, playerPos - transform.position);
                //Debug.Log(hit.collider.transform.tag);

                //if (hit.collider.transform.tag == "Player")
                //{
                //Vector3 spawningLocation = new Vector3(transform.position.x + (facingRight ? projectileOffset : -projectileOffset), transform.position.y, transform.position.z);
                //GameObject projectile = Instantiate(projectilePrefab, spawningLocation, Quaternion.identity);
                //Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();
                //projectileRB.velocity = (playerPos + Vector3.up * modUp - transform.position).normalized * throwSpeed;
                //}
            }
        }
        else
        {
            ResetAllTriggers();
            animator.SetTrigger("Idle");
            throwCooldownTimer -= Time.deltaTime;
        }
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
