using UnityEngine;

public class Throwable : MonoBehaviour
{

	Rigidbody2D rb;
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		Vector3 force = new Vector3(rb.velocity.x, rb.velocity.y, 0);
		Vector3 pos = new Vector3(transform.position.x, transform.position.y, 0f);
		Vector3 targetPos = pos + force;
		Quaternion rotation = Quaternion.LookRotation(
		targetPos - transform.position,
		transform.TransformDirection(Vector3.up)
		);
		transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(gameObject);
	}
}
