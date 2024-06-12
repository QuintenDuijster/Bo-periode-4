using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BossFight
{
    public class BossStart : MonoBehaviour
    {
        bool startBossFight = true;
        void Start()
        {
            //eventuele bossintro
        }
        private void Update()
        {
            if (startBossFight)
            {
                gameObject.AddComponent<Boss>();
                Destroy(this);
            }
        }

    }
}