using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    internal int health { get; set; }
    internal bool dead;

    public void addHealth(int index)
    {
        int newHealth = health;
        if (maxHealth < (newHealth + index))
        {
            health = maxHealth;
        }
        else
        {
            health = health + index;
        }
    }

    private void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if (health < 0 && dead)
        {
            //Debug.Log("DEAD");
        }
        //Debug.Log(health);
    }
}