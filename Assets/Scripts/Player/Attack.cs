using System;
using UnityEngine;

public class Attack : MonoBehaviour
{
<<<<<<< Updated upstream
	[SerializeField] private GameObject hitArea;
	[SerializeField] private GameObject Throwable;
=======
    private Controller controller;

    [Header("Keys")]
    [SerializeField] private KeyCode attack;
    [SerializeField] private KeyCode throwWeapon;
>>>>>>> Stashed changes

	private GameObject test;

<<<<<<< Updated upstream
	private bool canAttack = true;
	private bool isAttacking = false;
	private float AttackCooldownTimer = 0;
	private float MeleeCooldown = 2;
	private float ThrowCooldown = 2;

	private void Update()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


		HandlePosition(mousePosition);
		HandleRotation(mousePosition);
		HandleMelee();
		HandleThrow(mousePosition);
	}

	private void HandlePosition(Vector3 mousePosition)
	{
		double yDist = mousePosition.y - hitArea.transform.position.y;
		double xDist = mousePosition.x - hitArea.transform.position.x;

		double rad = Math.Atan2(yDist, xDist);

		Vector3 position = new Vector3(Mathf.Cos((float)rad), Mathf.Sin((float)rad), 0);

		if (Vector3.Distance(mousePosition, transform.position) > Vector3.Distance(hitArea.transform.position, transform.position))
		{
			hitArea.transform.position = transform.position + position;
		}
	}

	private void HandleRotation(Vector3 mousePosition)
	{
		float AngleRad = Mathf.Atan2(mousePosition.y - hitArea.transform.position.y, mousePosition.x - hitArea.transform.position.x);
		float angle = (180 / Mathf.PI) * AngleRad;

		hitArea.transform.rotation = Quaternion.Euler(0, 0, angle);
	}

	private void HandleMelee()
	{
		if (Input.GetMouseButtonDown(0) && canAttack)
		{
			hitArea.SetActive(true);
			canAttack = false;
			isAttacking = true;
			AttackCooldownTimer = 0;
		}

		if (isAttacking)
		{
			AttackCooldownTimer += Time.deltaTime;
			if (AttackCooldownTimer >= MeleeCooldown)
			{
				hitArea.SetActive(false);
				isAttacking = false;
				canAttack = true;
			}
		}
	}

	private void HandleThrow(Vector3 mousePosition)
	{
		if (Input.GetMouseButtonDown(1))
		{
			test = Instantiate(Throwable);
			test.transform.LookAt(mousePosition);
			test.transform.position = transform.position + new Vector3 (1f, 1f, 0);
			Rigidbody2D rb = test.GetComponent<Rigidbody2D>();
			rb.AddForce(new Vector3(700, 0, 0));
		}
	}
}
=======
    void Start()
    {
        controller = GetComponent<Controller>();
    }

    void Update()
    {
        if (Input.GetKey(attack))
        {
            isAttacking = true;
        }

    }
}
>>>>>>> Stashed changes
