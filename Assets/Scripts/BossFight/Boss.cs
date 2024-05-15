using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//let op action var is nog niet goed afgesteld

namespace BossFight
{
    public class Boss : MonoBehaviour
    {
        System.Random random = new();
        private GameObject skybox;
        bool action = true;
        float difficultyTime = 2f;
        float offset = 1;
        private int attack, amountOfAttacks = 2;
        private GameObject fist;
        private GameObject player;
        private GameObject screenBorderLeft;
        private GameObject screenBorderRight;
        private GameObject floor;

        void Start()
        {
            floor = GameObject.Find("GroundMesh");
            skybox = GameObject.Find("SkyBox");
            fist = Resources.Load("Prefabs/BossFight/Fist") as GameObject;
            player = GameObject.FindWithTag("Player");
            screenBorderRight = GameObject.Find("ScreenBorderLeft");
            screenBorderLeft = GameObject.Find("ScreenBorderRight");

            if (player == null || fist == null || screenBorderLeft == null || screenBorderRight == null)
            {
                Debug.Log("Loading in Boss.cs went wrong");
            } else
            {
                bossTimer();
            }
            
        }

        void bossTimer()
        {
            if (action)
            {
                action = false;
                if (!action)
                {
                    StartCoroutine("timer");
                }
            }
        }

        IEnumerator timer()
        {
            action = false;
            Debug.Log(action);

            yield return new WaitForSeconds(difficultyTime);
            
            
            attack = random.Next(amountOfAttacks);
            action = true;
            Debug.Log(action);
            Debug.Log(attack);
        }

        void actions()
        {
            if (action && attack == 0)
            {
                Instantiate(fist, transform.position, Quaternion.Euler(0, 0, 0));
                Instantiate(fist, transform.position, Quaternion.Euler(0, 0, -90));
                
            }

            if(action && attack == 1)
            {
                Instantiate(fist, transform.position, Quaternion.Euler(0, 0, 180));
                Instantiate(fist, transform.position, Quaternion.Euler(0, 0, 90));
            }

            if (action && attack == 2)
            {
                gameObject.AddComponent<GroundSpears>();
            }
            action = false;
        }

        void Update()
        {
            
            actions();
            
        }
    }
}
