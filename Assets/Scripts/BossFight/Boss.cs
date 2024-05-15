using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

//let op action var is nog niet goed afgesteld

namespace BossFight
{
    public class Boss : MonoBehaviour
    {
        [SerializeField]
        GameObject fist, player, screenBorderLeft, screenBorderRight, floor, skybox;
        [SerializeField]
        bool action = true;
        float difficultyTime = 2f;
        float offset = 4;
        private int attack, amountOfAttacks = 3;
        System.Random random = new();

        void Start()
        {
            
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
                GameObject fistLeft = Instantiate(fist, new Vector3(screenBorderLeft.transform.position.x - offset, player.transform.position.y - offset, 0f), Quaternion.Euler(0, 0, 0));
                GameObject fistRight = Instantiate(fist, new Vector3(screenBorderLeft.transform.position.x + offset, player.transform.position.y + offset, 0f), Quaternion.Euler(0, 180, 0));
            }

            if(action && attack == 1)
            {
                GameObject fistDown = Instantiate(fist, new Vector3(floor.transform.position.y - offset, player.transform.position.x - offset, 0f), Quaternion.Euler(0, 270, 0));
                GameObject fistUp = Instantiate(fist, new Vector3(skybox.transform.position.y + offset, player.transform.position.x + offset, 0f), Quaternion.Euler(0, 90, 0));

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
