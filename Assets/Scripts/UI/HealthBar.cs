using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Gradient gradient;
    public Image fill;

    public void SetHealth(int health)
    {
        healthBar.value = health;

        fill.color = gradient.Evaluate(healthBar.normalizedValue);
    }

    public void SetMaxHealth(int health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;

       fill.color = gradient.Evaluate(1f);
    }

}
