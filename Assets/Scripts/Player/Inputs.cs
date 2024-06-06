using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
	[Header("Movement")]
	public KeyCode up = KeyCode.W;
	public KeyCode down = KeyCode.S;
	public KeyCode left = KeyCode.A;
	public KeyCode right = KeyCode.D;
	public KeyCode jump = KeyCode.Space;
	public KeyCode dash = KeyCode.L;

	[Header("Attack")]
	public KeyCode meleeAttack = KeyCode.J;
	public KeyCode throwAttack = KeyCode.K;

}
