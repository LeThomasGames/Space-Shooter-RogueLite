using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    public int speed = 20;
    public int damage = 10;

    private bool hasDespawned = false;

    protected virtual void OnEnable()
    {
        hasDespawned = false;
    }

    protected virtual void Update()
    {
        Move();
    }

    protected abstract void Move();

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (hasDespawned) 
            return;

        DoDamage(other);
    }

    protected abstract void PlayExplosionEffect();

    protected virtual void DoDamage(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyBase enemy = other.gameObject.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            PlayExplosionEffect();
            Despawn();
        }
    }

    private void OnBecameInvisible()
    {
        if (!hasDespawned)
            Despawn();
    }

    protected void Despawn()
    {
        if (hasDespawned) return;

        hasDespawned = true;
        BulletPooler.Instance.ReturnBullet(gameObject);
    }
}
