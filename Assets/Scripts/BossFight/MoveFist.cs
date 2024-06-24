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

        public int Damage { get => damage; }

        private void Start()
        {
            Destroy(gameObject, 4);
        }

        void Update()
        {
            gameObject.transform.Translate(Vector2.up * speed, Space.Self);
        }
    }
}