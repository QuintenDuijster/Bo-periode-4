using UnityEngine;
using System.Collections;

public class DoRotateAround : MonoBehaviour
{

	public float speed;
	public Transform target;

	private Vector3 zAxis = new Vector3(0, 0, 1);

	void FixedUpdate()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 direction = mousePosition - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

		transform.RotateAround(target.position, new Vector2(angle, 0), speed);
	}
}