using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class scip : MonoBehaviour
{
    int i = 0;
    void Start()
    {
        Destroy(gameObject, 1);
        //transform.position = Vector3.SmoothDamp(gameObject.transform.position, new Vector3(Boss.player1.transform.position.x, 0));
    }

}



