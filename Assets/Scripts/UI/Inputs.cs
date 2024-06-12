using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Inputs")]
public class Inputs : ScriptableObject
{
	public KeyCode up;
	public KeyCode down;
	public KeyCode left;
	public KeyCode right;
	public KeyCode jump;
	public KeyCode dash;
	public KeyCode meleeAttack;
	public KeyCode throwAttack;
}
