using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public Slider slider;

    private Health playerHealth;

    void Awake()
    {
        if (slider == null)
            slider = GetComponent<Slider>();
    }

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("HealthBarUI: no se encontró el jugador con tag 'Player'");
            return;
        }

        playerHealth = player.GetComponent<Health>();
        if (playerHealth == null)
        {
            Debug.LogError("HealthBarUI: el jugador no tiene componente Health");
            return;
        }

        playerHealth.OnHealthChanged += UpdateBar;
        UpdateBar(playerHealth.CurrentHealth, playerHealth.MaxHealth);
    }

    void UpdateBar(int current, int max)
    {
        if (slider != null)
        {
            slider.maxValue = max;
            slider.value = current;
        }
    }

    void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnHealthChanged -= UpdateBar;
    }
}
