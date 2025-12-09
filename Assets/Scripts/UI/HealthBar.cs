using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    private PlayerHealth playerHealth;
    void Start()
    {
        playerHealth = PlayerData.Instance.Health;
        if (playerHealth != null)
        {
            healthSlider.maxValue = playerHealth.maxHealth;
            healthSlider.value = playerHealth.currentHealth;
            playerHealth.OnHealthChanged.AddListener(UpdateHealthBar);
        }
    }

    void UpdateHealthBar(int current, int max)
    {
        healthSlider.maxValue = max;
        healthSlider.value = current;
    }
}