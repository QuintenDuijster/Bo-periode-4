using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearDamage : MonoBehaviour
{
    int damage = 1;
    Vector3 velo = Vector3.zero;
    bool up = true;
    public int Damage { get => damage; }
    void Start()
    {
        Invoke("ChangeValue", 0.5f);
    }

    void ChangeValue()
    {
        up = false;
    }

    
    void Update()
    {
        if (up)
        {
            Vector3.SmoothDamp(transform.position + new Vector3(0, -6, 0), transform.position + new Vector3(0, 6, 0), ref velo, 0.5f);
        } else
        {
            Vector3.SmoothDamp(transform.position + new Vector3(0, 6, 0), transform.position + new Vector3(0, -6, 0), ref velo, 0.5f);
        } 
    }
}
