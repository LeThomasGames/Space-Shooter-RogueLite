using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 10;
    protected float currentHealth;
    protected int contactDamage;

    [Header("Optional FX")]
    public GameObject deathEffect;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        PlayDeathEffect();
        Destroy(gameObject);
    }

    protected abstract void PlayDeathEffect();

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player") && col.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            if (!playerHealth.isInvincible)
            {
                playerHealth.TakeDamage(contactDamage);
                Die();
            }
        }
    }
}



