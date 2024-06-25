using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
	[SerializeField] private int maxHealth;
	public int health { get; set; }
	public bool Dead;
	public HealthBar healthBar;

	public void addHealth(int index)
	{
		int newHealth = health;
		if (maxHealth < (newHealth + index))
		{
			health = maxHealth;
			healthBar.SetHealth(health);
		}
		else
		{
			health = health + index;
			healthBar.SetHealth(health);
		}
	}

	private void Start()
	{
		health = maxHealth;
		healthBar.SetMaxHealth(health);
	}

	private void Update()
	{
		if (health <= 0)
		{
			Debug.Log(health);
			Debug.Log("h1h1");
			DestroyImmediate(gameObject);
			SceneManager.LoadScene(2);
		}
	}
}