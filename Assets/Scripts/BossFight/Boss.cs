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
        private GameObject fistLeft, fistRight, beam;
        public static GameObject player1, floor;
        private GameObject screenBorderRight;
        Health health;
        private int bossHealth = 9;
        Animator a;

        public float moveSpeed = 20f;
        public Vector3[] patrolWaypoints = { new Vector3(-30, -7, 0), new Vector3(2, -7, 0), new Vector3(30, -7, 0), new Vector3(1, 9, 0) };
        public Vector3[] attackWaypoints = { new Vector3(-30, -7, 0), new Vector3(2, -7, 0), new Vector3(30, -7, 0), new Vector3(1, 9, 0) };
        private int currentPatrolWaypointIndex = 0;
        private Vector3 currentAttackWaypoint;
        private bool movingToAttack = false;
        Rigidbody2D rb;

        void Start()
        {
            a = GetComponent<Animator>();
            rb = gameObject.GetComponent<Rigidbody2D>();
            health = gameObject.AddComponent<Health>();
            health.maxHealth = bossHealth;

            floor = GameObject.Find("GroundMesh");
            skybox = GameObject.Find("SkyBox");
            fistLeft = Resources.Load("Prefabs/BossFight/FistLeft") as GameObject;
            fistRight = Resources.Load("Prefabs/BossFight/FistRight") as GameObject;
            player1 = GameObject.FindWithTag("Player");
            beam = Resources.Load("Prefabs/BossFight/Beam") as GameObject;
            screenBorderRight = GameObject.Find("ScreenBorderRight");
            screenBorderLeft = GameObject.Find("ScreenBorderLeft");

            if (player1 == null || fistLeft == null || screenBorderLeft == null || screenBorderRight == null || beam == null)
            {
                Debug.Log("Loading in Boss.cs went wrong");
            }
            StartCoroutine(timer());

        }

        void Update()
        {
            if (action == false)
            {
                MoveToAttackPosition();
            } else
            {
                Patrol();
            }
        }

        void MoveToAttackPosition()
        {
            if (!movingToAttack)
            {
                if (attackWaypoints.Length == 0)
                {
                    action = false;
                    return;
                }

                currentAttackWaypoint = attackWaypoints[Random.Range(0, attackWaypoints.Length)];
                movingToAttack = true;
            }

            Vector3 direction = (currentAttackWaypoint - transform.position).normalized;
            Vector2 newPosition = rb.position + new Vector2(direction.x, direction.y) * (moveSpeed * Time.deltaTime);

            rb.MovePosition(newPosition);

            if (Vector3.Distance(transform.position, currentAttackWaypoint) < 0.1f)
            {
                movingToAttack = false;
                StartCoroutine(timer());
            }
        }

        void Patrol()
        {
            if (patrolWaypoints.Length == 0)
            {
                return;
            }

            Vector3 targetWaypoint = patrolWaypoints[currentPatrolWaypointIndex];
            Vector3 direction = (targetWaypoint - transform.position).normalized;
            Vector2 newPosition = rb.position + new Vector2(direction.x, direction.y) * (moveSpeed * Time.deltaTime);

            rb.MovePosition(newPosition);

            if (Vector3.Distance(transform.position, targetWaypoint) < 0.1f)
            {
                currentPatrolWaypointIndex = (currentPatrolWaypointIndex + 1) % patrolWaypoints.Length;
            }
        }

        IEnumerator timer()
        {
            action = false;
            attack = random.Next(attackTypes.Count);

            if (attack == 0 && attack == 1)
            {
                difficultyTime = 4.5f;
            }
            else if (attack == 2)
            {
                 difficultyTime = 5.5f;
            } else
            {
                difficultyTime = 5;
            }

            for (int i = 0; i < attackTypes.Count - 1; i++)
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
                Instantiate(fistLeft, new Vector3(screenBorderLeft.transform.position.x, player1.transform.position.y) + Vector3.left, Quaternion.Euler(0, 0, -90));
                Instantiate(fistRight, new Vector3(screenBorderRight.transform.position.x, player1.transform.position.y) + Vector3.right, Quaternion.Euler(0, 0, 90));
            }

            if (action && attack == 1)
            {
                Instantiate(fistLeft, new Vector3(player1.transform.position.x, floor.transform.position.y, 0) + Vector3.down, Quaternion.Euler(0, 0, 0));
                Instantiate(fistRight, new Vector3(player1.transform.position.x, skybox.transform.position.y, 0) + Vector3.up, Quaternion.Euler(0, 0, 180));
            }

            if (action && attack == 2)
            {
                gameObject.AddComponent<GroundSpears>();
            }

            if (action && attack == 3)
            {
                Instantiate(beam, new Vector3(player1.transform.position.x, floor.transform.position.y + 2, 0), Quaternion.Euler(0, 0, 0));
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
