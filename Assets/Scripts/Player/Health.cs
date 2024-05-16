using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField] private int maxHealth;
	private int health { get; set; }

	public void addHealth(int index)
	{
		int newHealth = health;
		if (maxHealth < (newHealth + index))
		{
			health = maxHealth;
		}
		else
		{
			health = health + index;
		}
	}

	private void Start()
	{
		health = maxHealth;
	}

	private void Update()
	{
		if (health < 0)
		{

		}
	}
}
