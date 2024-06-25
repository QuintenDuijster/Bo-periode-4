using UnityEngine;

public class ScreenShake : MonoBehaviour
{
	 public float shake = 0f;
	[SerializeField] private float shakeAmount = 0.7f;
	[SerializeField] private float decreaseFactor = 1.0f;



	private void Update()
	{
		if (shake > 0)
		{
			transform.position = Random.insideUnitCircle * shakeAmount;
			transform.position = new Vector3 (transform.position.x, transform.position.y, -1);
			shake -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shake = 0f;
		}
	}

	public void Shake()
	{
		shake = 1;
	}
}
