using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
	[Header("Melee")]
	[SerializeField] private GameObject hitArea;
	[SerializeField] private float meleeCooldown;
	[SerializeField] private int meleeDamage;
	private float meleeCooldownTimer;
	private bool canAttack = true;

	[Header("Throw")]
	[SerializeField] private GameObject Throwable;
	[SerializeField] private float throwCooldown;
	[SerializeField] private float throwForce;
	[SerializeField] private int ThrowDamage;
	private float throwCooldownTimer;
	private bool canThrow = true;

	private void Update()
	{
		HandleMelee();
		HandleThrow();
	}

	private void HandleMelee()
	{
		if (meleeCooldownTimer > 0)
		{
			meleeCooldownTimer -= Time.deltaTime;
		}
		else
		{
			hitArea.SetActive(false);
			canAttack = true;
		}


		if (Input.GetKey(KeyCode.K) && canAttack && throwCooldownTimer <= 0)
		{
			hitArea.SetActive(true);
			meleeCooldownTimer = meleeCooldown;
		}
	}

	private void HandleThrow()
	{
		if (throwCooldownTimer > 0)
		{
			throwCooldownTimer -= Time.deltaTime;
		}
		else
		{
			canThrow = true;
		}

		if (Input.GetKey(KeyCode.L) && canThrow && meleeCooldownTimer <= 0)
		{
			throwCooldownTimer = throwCooldown;
			canThrow = false;
			GameObject newThrowable;
			newThrowable = Instantiate(Throwable, transform.position, transform.rotation);
			Rigidbody2D rb = newThrowable.GetComponent<Rigidbody2D>();

			Vector3 force = new Vector3(throwForce, 0, 0);

			if (newThrowable.transform.rotation.y == 0)
			{
				force = -force;
			}

			rb.AddForce(force);
		}
	}
}