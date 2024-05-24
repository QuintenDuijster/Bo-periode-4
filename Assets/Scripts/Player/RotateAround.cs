using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotateAround : MonoBehaviour
{
	[SerializeField] private GameObject hitArea;

	public Camera cam;


	void Awake()
	{
		cam = Camera.main;
	}


	void Update()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		double yDist = mousePosition.y - hitArea.transform.position.y;
		double xDist = mousePosition.x - hitArea.transform.position.x;

		double rad = Math.Atan2(yDist, xDist);

		Vector3 position = new Vector3(Mathf.Cos((float)rad), Mathf.Sin((float)rad), 0);

		Debug.Log(Vector3.Distance(mousePosition, transform.position) + " : " + Vector3.Distance(hitArea.transform.position, transform.position));

		if (Vector3.Distance(mousePosition, transform.position) > Vector3.Distance(hitArea.transform.position, transform.position))
		{
			hitArea.transform.position = position;
		}

		float AngleRad = Mathf.Atan2(mousePosition.y - hitArea.transform.position.y, mousePosition.x - hitArea.transform.position.x);
		float angle = (180 / Mathf.PI) * AngleRad;

		hitArea.transform.rotation = Quaternion.Euler(0, 0, angle);
	}
}