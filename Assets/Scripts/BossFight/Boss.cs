using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//let op action var is nog niet goed afgesteld

namespace BossFight
{
    public class Boss : MonoBehaviour
    {
        public GameObject screenBorderLeft, skybox;
        List<string> attackTypes = new List<string>() {"FistY", "FistX", "Spears", "Laser" };
        public static System.Random random = new();
        bool action = true;
        float difficultyTime = 4f;
        private int attack;
        private GameObject fist, beam;
        public static GameObject player1, floor;
        private GameObject screenBorderRight;
        Health health;
        private int bossHealth = 9;
        Animator a;

        void Start()
        {
            a = GetComponent<Animator>();

            health = gameObject.AddComponent<Health>();
            health.maxHealth = bossHealth;

            floor = GameObject.Find("GroundMesh");
            skybox = GameObject.Find("SkyBox");
            fist = Resources.Load("Prefabs/BossFight/Fist") as GameObject;
            player1 = GameObject.FindWithTag("Player");
            beam = Resources.Load("Prefabs/BossFight/Beam") as GameObject;
            screenBorderRight = GameObject.Find("ScreenBorderRight");
            screenBorderLeft = GameObject.Find("ScreenBorderLeft");

            if (player1 == null || fist == null || screenBorderLeft == null || screenBorderRight == null || beam == null)
            {
                Debug.Log("Loading in Boss.cs went wrong");
            }
            StartCoroutine(timer());

        }

        IEnumerator timer()
        {
            action = false;
            attack = random.Next(attackTypes.Count - 1);

            for(int i = 0; i < attackTypes.Count - 1; i++)
            {
                a.ResetTrigger(attackTypes[i]);
            }


            a.SetTrigger(attackTypes[attack]);

            yield return new WaitForSeconds(difficultyTime);


            action = true;

            if (!health.Dead)
            {
                actions();
                StartCoroutine("timer");

            }
            else
            {
                bossdefeat();
            }

        }


        void actions()
        {


            if (action && attack == 0)
            {
                Instantiate(fist, new Vector3(screenBorderLeft.transform.position.x, player1.transform.position.y) + Vector3.left, Quaternion.Euler(0, 0, -90));
                Instantiate(fist, new Vector3(screenBorderRight.transform.position.x, player1.transform.position.y) + Vector3.right, Quaternion.Euler(0, 0, 90));
                difficultyTime = 4.5f;
            }

            if (action && attack == 1)
            {
                Instantiate(fist, new Vector3(player1.transform.position.x, floor.transform.position.y, 0) + Vector3.down, Quaternion.Euler(0, 0, 0));
                Instantiate(fist, new Vector3(player1.transform.position.x, skybox.transform.position.y, 0) + Vector3.up, Quaternion.Euler(0, 0, 180));
                difficultyTime = 4.5f;
            }

            if (action && attack == 2)
            {
                gameObject.AddComponent<GroundSpears>();
                difficultyTime = 5.5f;
            }

            if (action && attack == 3)
            {
                Instantiate(beam, new Vector3(player1.transform.position.x, floor.transform.position.y + 2, 0), Quaternion.Euler(0, 0, 0));
                difficultyTime = 5f;
            }

            action = false;
        }

        private void bossdefeat()
        {
            Debug.Log("boss dead");
            SceneManager.LoadScene("Victory");
        }

    }
}
