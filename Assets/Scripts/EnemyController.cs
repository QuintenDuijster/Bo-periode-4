using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject player;
    Rigidbody rb;
    float2 playerPos;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerPos.x = player.gameObject.transform.position.x;
        playerPos.y = player.gameObject.transform.position.y;
    }
    void Update()
    {

    }
    void approachPlayer()
    {
        //rb.velocity = Vector3.Distance();
        rb.MovePosition(new Vector3(playerPos.x, playerPos.y));
    }
}
