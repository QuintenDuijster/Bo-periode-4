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
        GameObject fist, spear, player, screenBorderLeft, screenBorderRight, floor, skybox;
        [SerializeField]
        bool action = true;
        float difficultyTime = 2f;
        float offset = 4;
        private int attack;

        void Start()
        {
            fist = Resources.Load("Prefabs/BossFight/Fist") as GameObject;
            spear = Resources.Load("Prefabs/BossFight/Spear") as GameObject;
            player = GameObject.FindWithTag("Player");
            screenBorderRight = GameObject.Find("ScreenBorderLeft");
            screenBorderLeft = GameObject.Find("ScreenBorderRight");

            if (player == null || fist == null || screenBorderLeft == null || screenBorderRight == null)
            {
                Debug.Log("Loading in Boss.cs went wrong");
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
            yield return new WaitForSeconds(difficultyTime);

            System.Random random = new();
            attack = random.Next(7);
            action = true;
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
                GameObject groundSpears = Instantiate(spear, new Vector3(screenBorderLeft.transform.position.x, floor.transform.position.y, 1), Quaternion.Euler(0,0,0));
            }
            action = false;
        }

        void Update()
        {
            bossTimer();
            actions();
            
        }
    }
}
