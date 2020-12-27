using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeBullet : Bullet
{
    private float exploreRange;

    public void SetExplore(float _range)
    {
        exploreRange = _range;
    }

    protected override void HitTarget()
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2.0f);

        // 扣血
        Collider[] colliders = Physics.OverlapSphere(transform.position, exploreRange);
        foreach (Collider col in colliders)
        {
            if (col.tag == "Enemy")
            {
                EnemyStats enemy = col.GetComponent<EnemyStats>();
                enemy.TakenDamage(damage);
            }
        }
        Destroy(gameObject);
        return;
    }
}
