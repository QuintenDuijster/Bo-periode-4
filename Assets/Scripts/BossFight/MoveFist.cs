using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


namespace BossFight
{
    public class MoveFist : MonoBehaviour
    {
        float speed = 0.1f;
        int damage = 1;

        private void Start()
        {
            Destroy(gameObject, 4);
        }

        void Update()
        {
            gameObject.transform.Translate(Vector2.up * speed, Space.Self);
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
}