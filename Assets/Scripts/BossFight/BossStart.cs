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