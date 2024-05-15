using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("Keys")]
    [SerializeField] private KeyCode attack;
    [SerializeField] private KeyCode throwWeapon;

    private bool isAttacking = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(attack))
        {
            isAttacking = true;
        }
    }
}
