using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bossHealth : MonoBehaviour
{
	[SerializeField] private int maxHealth;
	public int health { get; set; }
	public bool Dead;
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
		if (health <= 0)
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			Destroy(player);
			SceneManager.LoadScene(1);
		}
	}
}
