using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
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
			Debug.Log(health);
			Debug.Log("h1h1");
			DestroyImmediate(gameObject);
			SceneManager.LoadScene(2);
		}
	}
}