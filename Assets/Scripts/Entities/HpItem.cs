using UnityEngine;

public class HpItem : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			Health health = collision.gameObject.GetComponent<Health>();
			health.health = health.maxHealth;
			Destroy(gameObject);
		}
	}
}