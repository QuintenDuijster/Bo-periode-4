using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


namespace BossFight
{
    public class MoveFist : Boss
    {
        int orientation = 1;
        Rigidbody2D rb;
        float speed = 0.1f;
        bool h = false;
        
        
        void Start()
        {
            

            if (gameObject.transform.rotation.eulerAngles.y == 180)
            {
                orientation = -1;
            }

            if (gameObject.transform.rotation.eulerAngles.y == 90)
            {
                orientation = -1;
                h = true;
            }

            if (gameObject.transform.rotation.eulerAngles.y == 270)
            {
                orientation = 1;
                h= true;
            }
            rb = GetComponent<Rigidbody2D>();

            Destroy(gameObject, 5);
        }

        void Update()
        {
            if (!h)
            {
                Vector3 move = new Vector3(transform.position.x * orientation, rb.transform.position.y, rb.transform.position.z);
                transform.position = Vector3.MoveTowards(rb.position, move, Time.deltaTime * speed);
            }
            if(h)
            {
                Vector3 move = new Vector3(anyBorder.transform.position.x , rb.transform.position.y * orientation, rb.transform.position.z);
                transform.position = Vector3.MoveTowards(rb.position, move, Time.deltaTime * speed);
            }
        }
    }
}