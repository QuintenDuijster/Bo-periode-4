using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	private Rigidbody2D RB;
	private float horizontal;
	private float speed = 1000;

	void Start()
	{
		RB = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		horizontal = Input.GetAxis("Horizontal");
	}

	private void FixedUpdate()
	{
		move();
	}

	private void move()
	{
		Vector2 velocity = new Vector2(horizontal, 0f) * speed;
		RB.AddForce(velocity * Time.deltaTime);
	}
}
