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
            Destroy(spearI, 1);
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

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.tag == "Player")
			{
				Health health = collision.gameObject.GetComponent<Health>();
				health.addHealth(-damage);
			}
		}
	}
}
