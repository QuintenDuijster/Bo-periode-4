using BossFight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beeemmm : MonoBehaviour
{
    int damage = 1;
    int direction;
    Vector3 velocity = Vector3.zero; 

    void Start()
    {
        Destroy(gameObject, 4);
    }

    void SetDamage()
    {
        damage = 1;
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(gameObject.transform.position, new Vector3(Boss.player1.transform.position.x, Boss.floor.transform.position.y), ref velocity, 0.1f, 34, Time.deltaTime);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Health health = collision.gameObject.GetComponent<Health>();
			health.addHealth(-damage);
		}
	}
}
