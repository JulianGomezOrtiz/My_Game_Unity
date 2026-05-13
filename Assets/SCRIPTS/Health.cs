using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    public System.Action<int, int> OnHealthChanged;
    public System.Action OnDied;

    public float deathDestroyDelay = 3f;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        isDead = true;
        currentHealth = 0;

        var controller = GetComponent<StarterAssets.ThirdPersonController>();
        if (controller != null) controller.enabled = false;

        OnDied?.Invoke();
        Destroy(gameObject, deathDestroyDelay);
    }
}
