using Damage;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplaySubscriber : MonoBehaviour
{
    [SerializeField] private HealthContainer healthContainer;
    [SerializeField] private PlayerSpawner playerSpawner;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private Gradient healthColorGradient;

    private void OnEnable()
    {
        playerSpawner.onPlayerSpawned += OnPlayerSpawned;
    }

    private void OnDisable()
    {
        playerSpawner.onPlayerSpawned -= OnPlayerSpawned;
        if(healthContainer != null)
        {
            healthContainer.OnHealthChanged -= OnHealthChanged;
        }
    }

    private void OnHealthChanged(float currentHealth, float maxHealth)
    {
        healthBarImage.fillAmount = currentHealth / maxHealth;
        healthBarImage.color = healthColorGradient.Evaluate(currentHealth / maxHealth);
    }

    private void OnPlayerSpawned(GameObject player)
    {
        healthContainer = player.GetComponent<HealthContainer>();
        healthContainer.OnHealthChanged += OnHealthChanged;
    }
}
