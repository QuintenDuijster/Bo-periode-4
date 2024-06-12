using BossFight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beeemmm : MonoBehaviour
{
    int damage = 0;
    int direction;
    Vector3 velocity = Vector3.zero; 

    public int Damage { get => damage; }
    void Start()
    {
        Invoke("SetDamage" , 1);
        Destroy(gameObject, 4);
    }

    void SetDamage()
    {
        damage = 1;
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(gameObject.transform.position, new Vector3(Boss.player1.transform.position.x, 0), ref velocity, 0.3f, 7, Time.deltaTime);
    }
}
