using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotateAround : MonoBehaviour
{
	[SerializeField] private GameObject hitArea;

	void Update()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		Vector3 direction = mousePosition - hitArea.transform.position;

		double yDist = mousePosition.y - hitArea.transform.position.y;
		double xDist = mousePosition.x - hitArea.transform.position.x;

		Vector3 pos = direction / direction.magnitude;

		double rad = Math.Atan2(yDist, xDist);

	



		Vector3 position = new Vector3(Mathf.Cos((float)rad), Mathf.Sin((float)rad) ,0);

		hitArea.transform.position = position;
		/*
		Vector2 newPos2 = Vector2.zero;
			
		newPos2.x = -distance.x / distance.x;
		newPos2.y = -distance.y / distance.y;


		Vector3 newPos3 = transform.position;
		newPos3.x = newPos2.x;
		newPos3.y = newPos2.y;

		
		*/
	}
}
