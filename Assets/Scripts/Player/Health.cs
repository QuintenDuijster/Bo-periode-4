using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField] private int maxHealth;
	private int health;
	bool dead = false;

    public int Healthgetset { get => health; set => health = value; }
    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public bool Dead { get => dead; }

    public void addHealth(int index)
	{
		int newHealth = health;
		if (MaxHealth < (newHealth + index))
		{
			health = MaxHealth;
		}
		else
		{
			health = health + index;
		}
	}

	private void Start()
	{
		health = MaxHealth;
	}

	private void Update()
	{
		if (health < 0)
		{
			dead = true;
			Destroy(gameObject);
		}
	}
}
