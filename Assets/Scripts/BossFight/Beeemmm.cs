using BossFight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beeemmm : MonoBehaviour
{
    int damage = 0;
    int direction;
    
    public int Damage { get => damage; }
    void Start()
    {
        int randomNumber = Boss.random.Next(2);
        int result = randomNumber == 0 ? -1 : 1;
        Debug.Log(result);
        Invoke("SetDamage" , 1);
    }

    void SetDamage()
    {
        damage = 1;
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(Boss.player1.)
    }
}
