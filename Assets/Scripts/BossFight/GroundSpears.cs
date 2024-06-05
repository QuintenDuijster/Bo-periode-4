using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


namespace BossFight
{
    public class GroundSpears : MonoBehaviour
    {
        [SerializeField]
        GameObject spearHoles, spear;
        private float timeForSpear = 1;
        int i = 0;
        int damage = 1;
        private bool help = true, started = false;
        

        public int Damage { get => damage; }
        void Start()
        {
            spearHoles = GameObject.Find("GroundSpears");
            spear = Resources.Load("Prefabs/BossFight/Spear") as GameObject;

            StartCoroutine(spearTimer());
        }

        IEnumerator spearTimer()
        {
            for (i = i; i < spearHoles.transform.childCount || i > -1; i += help ? 1 : -1)
            {
                if (i >= spearHoles.transform.childCount - 1)
                {
                    help = false;
                    if (!started)
                    {
                        started = true;
                        StartCoroutine(spearTimer());
                        
                    }
                }
                else if (i <= -1)
                {
                    Destroy(this);
                }

                GameObject spearI = Instantiate(spear, spearHoles.transform.GetChild(i));
                Destroy(spearI, 1);
                yield return new WaitForSeconds(timeForSpear);
                
            }
        }

    }
}
