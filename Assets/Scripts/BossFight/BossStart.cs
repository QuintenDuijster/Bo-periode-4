using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BossFight
{
    public class BossStart : MonoBehaviour
    {
        bool startBossFight = true;
        private GameObject cam;
        private ScreenShake screenShake;


        void Start()
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera");
            screenShake = cam.GetComponent<ScreenShake>();
            //eventuele bossintro
        }
        private void Update()
        { 
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Vector3.zero, 0.01f);
            gameObject.transform.localScale = Vector3.MoveTowards(gameObject.transform.localScale, new Vector3(7, 7), Time.deltaTime);

            if (startBossFight)
            {
                gameObject.AddComponent<Boss>();
                Destroy(this);
            }

            if (screenShake.shake < 0)
            {
                startBossFight = true;
            }
        }
    }
}