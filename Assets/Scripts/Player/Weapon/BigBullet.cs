using UnityEngine;

public class BigBullet : BulletBase
{   
    protected override void Move()
    {
        Vector2 vec = new Vector2(speed, 0);
        transform.Translate(vec * speed * Time.deltaTime);
    }
    protected override void PlayExplosionEffect()
    {
        
        return;
    }
}
