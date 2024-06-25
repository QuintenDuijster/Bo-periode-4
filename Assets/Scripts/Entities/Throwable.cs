using UnityEngine;

public class Throwable : MonoBehaviour
{

    internal int damage;
	void Start()
	{
		Destroy(gameObject ,2);
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
