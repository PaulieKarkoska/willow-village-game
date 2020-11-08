using Invector;
using UnityEngine;
using UnityEngine.UI;

public class TreeOfLife : MonoBehaviour
{
    public delegate void TreeKilled();
    public static event TreeKilled OnTreeKilled;

    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private vHealthController healthController;

    private void Start()
    {
        healthSlider.maxValue = healthController.MaxHealth;
        healthSlider.value = healthController.maxHealth;
    }

    public float maxHealth
    {
        get { return healthController.maxHealth; }
    }

    public void UpdateHealthSlider(float value)
    {
        healthSlider.value = value;
    }

    public void RestoreAllHealth()
    {
        healthSlider.value = healthSlider.maxValue;
        healthController.ResetHealth();
    }

    public void UpgradeHealth(float value)
    {
        healthSlider.maxValue = value;
        healthSlider.value = value;

        healthController.maxHealth = (int)value;
        healthController.ResetHealth();
    }

    public void OnNodeReceiveDamage(vDamage damage)
    {
        healthController.TakeDamage(damage);
        if (healthController.currentHealth <= 0)
            OnTreeKilled?.Invoke();
    }
}