using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private GameObject hitArea;
    [SerializeField] private GameObject throwable;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hitArea.SetActive(true);
        }
		else
		{
			hitArea.SetActive(false);
		}

		if (Input.GetMouseButtonDown(1))
		{
            GameObject newThrowable = Instantiate(throwable);
            Rigidbody2D newThrowable_Rb = newThrowable.GetComponent<Rigidbody2D>();
            float verticalVelocity = 0f;

            if(transform.rotation.y == 0){
                verticalVelocity = -10;
				newThrowable.transform.position = transform.position - new Vector3(1, 0, 0);
			}
            else
            {
                verticalVelocity = 10;
				newThrowable.transform.position = transform.position - new Vector3(-1, 0, 0);
			}

            newThrowable_Rb.velocity = new Vector2 (verticalVelocity, 0.0f);
		}
	}
}
