using System.Collections;
using TMPro;
using UnityEngine;

public class SettingsHandler : MonoBehaviour
{
	[SerializeField] private Inputs inputs;
	[SerializeField] private TMP_Text[] labels;

	private void Start()
	{
		labels[0].text = inputs.left.ToString();
		labels[1].text = inputs.right.ToString();
		labels[2].text = inputs.up.ToString();
		labels[3].text = inputs.down.ToString();
		labels[4].text = inputs.dash.ToString();
		labels[5].text = inputs.jump.ToString();
		labels[6].text = inputs.meleeAttack.ToString();
		labels[7].text = inputs.throwAttack.ToString();
	}

	public void ChangeKey(TMP_Text label)
	{
		StartCoroutine(ChangeKeyCoroutine(label));
	}

	private IEnumerator ChangeKeyCoroutine(TMP_Text label)
	{
		bool getKey = true;
		KeyCode key;

		while (getKey)
		{
			if (label.text.Length < 3)
			{
				label.text += ".";
			}
			else
			{
				label.text = "";
			}

			key = GetCurrentKeyDown();
			if (key != KeyCode.None)
			{
				getKey = false;
				label.text = key.ToString();
				ChangeKeyCode(label.name, key);
			}

			yield return null; 
		}
	}

	private void ChangeKeyCode(string keyName, KeyCode keyCode)
	{
		switch (keyName)
		{
			case "LeftKey": 
				inputs.left = keyCode;
				break;
			case "RightKey":
				inputs.right = keyCode;
				break;
			case "UpKey":
				inputs.up = keyCode;
				break;
			case "DownKey":
				inputs.down = keyCode;
				break;
			case "JumpKey":
				inputs.jump = keyCode;
				break;
			case "DashKey":
				inputs.down = keyCode;
				break;
			case "MeleeKey":
				inputs.meleeAttack = keyCode;
				break;
			case "ThrowKey":
				inputs.throwAttack = keyCode;
				break;
		}
	}

	private KeyCode GetCurrentKeyDown()
	{
		foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
		{
			if (Input.GetKeyDown(keyCode))
			{
				return keyCode;
			}
		}
		return KeyCode.None;
	}
}
