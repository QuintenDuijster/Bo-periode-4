using UnityEngine;

public class SettingsHandler : MonoBehaviour
{
	[SerializeField] private Inputs inputs;
	[SerializeField] private  TMPro.TMP_Text[] labels;

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

	public void changeKey(TMPro.TMP_Text label)
	{
		bool getKey = true;
		KeyCode key;

		while (getKey)
		{
			label.text = "...";
			if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2) && !Input.anyKeyDown)
			{
			    key = GetCurrentKeyDown();
				if (key != KeyCode.None)
				{
					getKey = false;

					label.text = key.ToString();
				}
			}
		}
	}

	private KeyCode GetCurrentKeyDown()
	{
		foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
		{
			if (Input.GetKeyDown(kcode))
			{
				return kcode;
			}
		}
		return KeyCode.None;
	}
}
