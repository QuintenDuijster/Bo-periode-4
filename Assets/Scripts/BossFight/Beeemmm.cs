using BossFight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beeemmm : MonoBehaviour
{
    int damage;
    int direction;
    float time = 0;
    Health health;
    Vector3 velocity = Vector3.zero;

    void Start()
    {
	    health = Boss.player1.GetComponent<Health>();
        Destroy(gameObject, 4);
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(gameObject.transform.position, new Vector3(Boss.player1.transform.position.x, Boss.floor.transform.position.y +2), ref velocity, 0.1f, 34, Time.deltaTime);
    }

	private void OnTriggerStay2D(Collider2D collision)
	{
		Debug.Log(collision);
		if (collision.gameObject == Boss.player1)
		{
			time -= Time.deltaTime;
			if (time <= 0)
			{
				damage = 1;
				time = 1;
				health.addHealth(-damage);
				damage = 0;
			}
			Debug.Log("Damage?");
		}
	}

	public int Damage { get => damage; }
}
