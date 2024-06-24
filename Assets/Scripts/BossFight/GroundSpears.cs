using System;
using System.Collections;
using UnityEngine;


namespace BossFight
{
    public class GroundSpears : MonoBehaviour
    {
        [SerializeField]
        GameObject spearHoles, spear;
        private float timeForSpear = 0.5f;
        int i = 0;
        private bool othaWay;


        
        void Start()
        {
            spearHoles = GameObject.Find("GroundSpears");
            spear = Resources.Load("Prefabs/BossFight/Spear") as GameObject;

            StartCoroutine(spearTimer());
        }

        IEnumerator spearTimer()
        {

            GameObject spearI = Instantiate(spear, spearHoles.transform.GetChild(i));
            Destroy(spearI, 0.5f);
            Debug.Log(i);
            if (i < 4 && othaWay == false)
            {
                i++;

                if (i == 4)
                {
                    Debug.Log(othaWay);
                    othaWay = true;
                }
            }
            else
            {
                i--;
                if (i == -1)
                {
                    Destroy(this);
                }
            }
            yield return new WaitForSeconds(timeForSpear);

            StartCoroutine(spearTimer());
        }

    }
}
