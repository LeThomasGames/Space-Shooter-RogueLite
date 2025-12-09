using Unity.VisualScripting;
using UnityEngine;

public class StraightBullet : BulletBase
{
    protected override void Move()
    {
        Vector2 vec = new Vector2(1, 0);
        transform.Translate(vec * speed * Time.deltaTime);
    }
    protected override void PlayExplosionEffect()
    {

    }
}