using UnityEngine;

public class Throwable : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(gameObject);
	}
}
