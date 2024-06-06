using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Inputs inputs;

	private void Start()
	{
		inputs = GetComponent<Inputs>();
	}

	void Update()
    {
        resetTriggers();


    }

    private void resetTriggers()
    {

    }
}
