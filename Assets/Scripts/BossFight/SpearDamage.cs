using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearDamage : MonoBehaviour
{
    int damage = 1;
    bool up = true;
    public int Damage { get => damage; }
    Vector3 startPos;
    void Start()
    {
        startPos = gameObject.transform.position;
        Invoke("ChangeValue", 0.4f);
        transform.position += new Vector3(0 , -20);
    }

    void ChangeValue()
    {
        up = false;
    }

    
    void Update()
    {
        
        if (up)
        {
            transform.position =Vector3.MoveTowards(gameObject.transform.position, startPos + new Vector3(0, 2), 0.4f); 
        }
        else
        {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, startPos + new Vector3(0, -20), 0.4f);
        }
    }
}
