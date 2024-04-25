using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float health 
    {
        get 
        { 
            return health; 
        }
        
        set
        {
            float newHp = health;
            if (newHp + value <= maxHealth)
            {
                health = newHp;
            }
        }
    }

    private void Start()
    {
        health = maxHealth;   
    }

    void Update()
    {
        if (health <= 0)
        {
            //death screen or somthing
        }
    }
}
