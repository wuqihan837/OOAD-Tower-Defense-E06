
using UnityEngine;

public class StandardBullet : Bullet
{
    protected override void HitTarget()
    {
        GameObject effect = Instantiate(hitEffect,transform.position,Quaternion.identity);
        Destroy(effect, 2.0f);
        enemyStats.TakenDamage(damage);
        Destroy(gameObject);
        return;
    }
}
