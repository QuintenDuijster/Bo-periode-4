using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
	public int maxHealth;
	public int health;
	public bool Dead;
	HealthBar healthBar;

	public void addHealth(int index)
	{
		int newHealth = health;
		if (maxHealth < (newHealth + index))
		{
			health = maxHealth;
			healthBar.SetMaxHealth(maxHealth);
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
		healthBar = GameObject.FindGameObjectWithTag("HpDisplay").GetComponent<HealthBar>();
		healthBar.SetMaxHealth(health);
	}

	private void Update()
	{
		if (health <= 0)
		{
			DestroyImmediate(gameObject);
			SceneManager.LoadScene(2);
		}
	}
}