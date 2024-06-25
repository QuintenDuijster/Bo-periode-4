using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BossFight
{
    public class BossStart : MonoBehaviour
    {
        bool startBossFight = true;
        private GameObject camera;
        private ScreenShake screenShake;

        void Start()
        {
            camera = GameObject.FindGameObjectWithTag("MainCamera");
            screenShake = camera.GetComponent<ScreenShake>();
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