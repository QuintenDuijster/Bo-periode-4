using UnityEngine;
using UnityEngine.SceneManagement;

public class death : MonoBehaviour
{

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			SceneManager.LoadScene(3);
		}
	}
}
