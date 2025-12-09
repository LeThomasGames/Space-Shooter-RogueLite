using Unity.Android.Gradle;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Paramètres de vie")]
    public int maxHealth = 100;
    public int currentHealth;
    public bool isInvincible = false;

    [HideInInspector]
    [Header("Événements")]
    public UnityEvent<int, int> OnHealthChanged;
    public UnityEvent OnDeath;

    [Header("Invincibilité")]
    public SpriteRenderer ship;
    public SpriteRenderer lines;
    // vitesse clignotement
    public float invincibilityFlashDelay = 0.2f;
    // temps invincibility
    public float invincibilityTime = 3f;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            TakeDamage(100);
    }

    public void TakeDamage(int amount)
    {
        if (!isInvincible)
        {
            isInvincible = true;
            currentHealth -= amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
            if (currentHealth > 0)
            {
                StartCoroutine(InvincibilityFlash());
                StartCoroutine(HandleInvincibilityDeley());
            }
            else { Die(); }
        }
    }
    public IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            ship.color = new Color(1f, 1f, 1f, 0f);
            lines.color = new Color(1f, 0f, 0f, 0f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
            ship.color = new Color(1f, 1f, 1f, 1f);
            lines.color = new Color(1f, 0f, 0f, 0.5f);
            yield return new WaitForSeconds(invincibilityFlashDelay);
        }
    }

    public IEnumerator HandleInvincibilityDeley()
    {
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    private void Die()
    {
        Debug.Log("Le joueur est mort !");
        OnDeath?.Invoke();
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        gameObject.GetComponent<PlayerShooting>().enabled = false;
        DestroyPart();
        anim.SetBool("Die", true);
    }

    public void DestroyPart()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void Destroy()
    {
        ship.color = new Color(1f, 1f, 1f, 0f);
        lines.color = new Color(1f, 0f, 0f, 0f);
    }
}

