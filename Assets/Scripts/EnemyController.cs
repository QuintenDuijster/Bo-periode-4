using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyController : Controller
{
    [SerializeField] GameObject player;
    Rigidbody2D rb;
    Vector3 playerPos;
    [SerializeField] float minSpeed, maxSpeed;
    float speed;
    [SerializeField] float slowDownDistance;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPos.x = player.gameObject.transform.position.x;
        playerPos.y = player.gameObject.transform.position.y;
    }
    void Update()
    {
        
    }
    void approachPlayer()
    {
        speed = Mathf.Lerp(minSpeed,maxSpeed, Vector3.Distance(playerPos, transform.position) * slowDownDistance);
        rb.MovePosition(new Vector3(playerPos.x, playerPos.y));

    }
}
