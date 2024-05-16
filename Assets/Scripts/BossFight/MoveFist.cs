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
        int orientation = 1;
        Rigidbody2D rb;
        float speed = 4;
        bool h = false;
        GameObject bossRefrence;
        Boss bossSRefrence;
        bool facingRight;

        int x = 0;
        int y = 0;

        void Start()
        {
            bossRefrence = GameObject.Find("BossEObj");
            bossSRefrence = bossRefrence.GetComponent<Boss>();

            Debug.Log($"y: {gameObject.transform.rotation.eulerAngles.y}");
            if (gameObject.transform.rotation.eulerAngles.y == 180)
            {
                Debug.Log("test1");
                y = 1;
            }

            if (gameObject.transform.rotation.eulerAngles.y == 90)
            {
                Debug.Log("test2");
                y = -1;
                h = true;
            }

            if (gameObject.transform.rotation.eulerAngles.y == -90)
            {
                Debug.Log("test3");
                x = 1;
                h= true;
            }
            rb = GetComponent<Rigidbody2D>();

            Destroy(gameObject, 5);
        }

        void Update()
        {
            //rb.AddForce(new Vector2(x, y) * speed * Time.deltaTime);
            Debug.Log($"X: {x}, Y: {y}");
            transform.position += new Vector3(x, y, 0) * speed * Time.deltaTime;
        }
    }
}