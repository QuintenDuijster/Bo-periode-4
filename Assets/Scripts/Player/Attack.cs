using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
	private Inputs inputs;

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

	private void Start()
	{
		inputs = GetComponent<Inputs>();
	}

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


		if (Input.GetKey(inputs.meleeAttack) && canAttack && throwCooldownTimer <= 0)
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

		if (Input.GetKey(inputs.throwAttack) && canThrow && meleeCooldownTimer <= 0)
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