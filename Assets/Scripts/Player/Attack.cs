using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{

	[Header("MeleeAttack")]
	[SerializeField] private GameObject hitArea;
	[SerializeField] private int meleeCooldown;
	private float meleeCooldownTimer;
	private bool canMelee;
	private bool isAttacking;

	[Header("ThrowAttack")]
	[SerializeField] private GameObject throwable;
	[SerializeField] private int throwableSpeed;
	[SerializeField] private int throwCooldown;
	private float throwCooldownTimer;
	private bool canThrow;

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			HandleMeleeAttack();
		}

		if (Input.GetMouseButtonDown(1))
		{
			HandleThrowAttack();
		}

		if (meleeCooldownTimer < meleeCooldown)
		{
			meleeCooldownTimer += Time.deltaTime;
		}
		else
		{
			canMelee = true;
		}

		if (throwCooldownTimer < throwCooldown)
		{
			throwCooldownTimer += Time.deltaTime;
		}
		else
		{
			canThrow = true;
		}
	}

	private void HandleMeleeAttack()
	{
		if (canMelee)
		{
			canMelee = false;

		}
	}

	private void HandleThrowAttack()
	{
		GameObject newThrowable = Instantiate(throwable);
		Rigidbody2D newThrowable_Rb = newThrowable.GetComponent<Rigidbody2D>();
		float verticalVelocity;
		Vector3 direction;

		if (transform.rotation.y == 0)
		{
			direction = new Vector3(1, 0, 0);

			verticalVelocity = -throwableSpeed;
		}
		else
		{
			direction = new Vector3(-1, 0, 0);

			verticalVelocity = throwableSpeed;
		}

		newThrowable.transform.position = transform.position - direction;


		newThrowable_Rb.velocity = new Vector2(verticalVelocity, 0.0f);
	}
}
