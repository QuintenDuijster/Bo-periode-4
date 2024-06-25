using System.Collections;
using UnityEngine;

//let op action var is nog niet goed afgesteld

namespace BossFight
{
    public class Boss : MonoBehaviour
    {
        public GameObject screenBorderLeft, skybox;

        public static System.Random random = new();
        bool action = true;
        float difficultyTime = 4f;
        private int attack, amountOfAttacks = 2 , aOffset = 6;
        private GameObject fist, beam;
        public static GameObject player1;    
        private GameObject screenBorderRight;
        private GameObject floor;
        bossHealth health;
        private int bossHealth = 9;

        void Start()
        {
            health = gameObject.AddComponent<bossHealth>();
            health.maxHealth = 10;
            floor = GameObject.Find("GroundMesh");
            skybox = GameObject.Find("SkyBox");
            fist = Resources.Load("Prefabs/BossFight/Fist") as GameObject;
            player1 = GameObject.FindWithTag("Player");
            beam = Resources.Load("Prefabs/BossFight/Beam") as GameObject;
            screenBorderRight = GameObject.Find("ScreenBorderRight");
            screenBorderLeft = GameObject.Find("ScreenBorderLeft"); 

            if (player1 == null)
            {
                Debug.Log("player = null");
            }

            if (fist == null)
			{
				Debug.Log("fist = null");
			}

			if (screenBorderLeft == null)
			{
				Debug.Log("screenBorderLeft = null");
			}

			if (screenBorderRight == null)
			{
				Debug.Log("screenBorderRight = null");
			}

			if (beam == null)
			{
				Debug.Log("beam = null");
			}

			StartCoroutine("timer");

            health.addHealth(bossHealth);
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


            yield return new WaitForSeconds(difficultyTime);


            attack = random.Next(amountOfAttacks);
            Debug.Log(attack);
            action = true;

            if(!health.Dead)
            {
                actions();
                StartCoroutine("timer");

            } else
            {
                bossdefeat();
            }
           
        }


        void actions()
        {
            if (action && attack == 0)
            {
                Instantiate(fist, screenBorderLeft.transform.position + Vector3.left * aOffset, Quaternion.Euler(0, 0, -90));
                Instantiate(fist, screenBorderRight.transform.position + Vector3.right * aOffset, Quaternion.Euler(0, 0, 90));
            }

            if(action && attack == 1)
            {
                Instantiate(fist, floor.transform.position + Vector3.down * aOffset, Quaternion.Euler(0, 0, 0));
                Instantiate(fist, skybox.transform.position + Vector3.up * aOffset, Quaternion.Euler(0, 0, 180));
            }

            if (action && attack == 2)
            {
                gameObject.AddComponent<GroundSpears>();
            }

            if (action && attack == 3)
            {
                Instantiate(beam, new Vector3(player1.transform.position.x, 0.5f, 0), Quaternion.Euler(0, 0, 0));
            }

            action = false;
        }

        private void bossdefeat()
        {
            Debug.Log("boss dead");
        }

    }
}
