using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//let op action var is nog niet goed afgesteld

namespace BossFight
{
    public class Boss : MonoBehaviour
    {
        public GameObject screenBorderLeft, skybox;

        System.Random random = new();
        bool action = true;
        float difficultyTime = 2f;
        float offset = 1;
        private int attack, amountOfAttacks = 2;
        private GameObject fist;
        public GameObject player;    
        private GameObject screenBorderRight;
        private GameObject floor;

        void Start()
        {
            floor = GameObject.Find("GroundMesh");
            skybox = GameObject.Find("SkyBox");
            fist = Resources.Load("Prefabs/BossFight/Fist") as GameObject;
            player = GameObject.FindWithTag("Player");
            screenBorderRight = GameObject.Find("ScreenBorderRight");
            screenBorderLeft = GameObject.Find("ScreenBorderLeft");

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
                Instantiate(fist, screenBorderLeft.transform.position + Vector3.left, Quaternion.Euler(0, 0, 0));
                Instantiate(fist, screenBorderRight.transform.position + Vector3.right, Quaternion.Euler(0, -90, 0));
            }

            if(action && attack == 1)
            {
                Instantiate(fist, floor.transform.position + Vector3.down, Quaternion.Euler(0, 180, 0));
                Instantiate(fist, skybox.transform.position + Vector3.up, Quaternion.Euler(0, 90, 0));
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
