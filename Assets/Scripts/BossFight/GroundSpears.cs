using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


namespace BossFight
{
    public class GroundSpears : MonoBehaviour
    {
        float distanceS = 0.5f;


        void Start()
        {
            Instantiate(gameObject, new Vector3(gameObject.transform.position.x + distanceS, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.Euler(0, 0, 0));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Stats.health -= 1;
            }
            else
            {
                Debug.Log("Not player hit - GroundSpears.cs");
            }
        }

    }


}