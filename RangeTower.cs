using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeTower : AttackableTower
{
    public float explosionRange;
    public GameObject bulletPrefab;

    public GameObject hitEffect;

    public Animator ani;
    protected override void Shot()
    {
        if(ani.GetBool("Shoot"))
        {
            ani.SetBool("Shoot",false);
        }
        float attackCoe = PlayerStats.GetInstance().TowerAttackAmpl();

        ani.SetBool("Shoot", true);
        timer = 0.0f;
        // 创建子弹，获取子弹的脚本组件，指定目标敌人

        GameObject bulletObj = Instantiate(bulletPrefab, shotPosition.position, shotPosition.rotation);
        RangeBullet bullet = bulletObj.GetComponent<RangeBullet>();
        bullet.SetTarget(currentEnemyStats);
        bullet.SetDamage(attackPower * attackCoe);
        bullet.SetExplore(explosionRange);
        bullet.SetHitEffect(hitEffect);

        currentEnemyStats.Register(bullet);

        GameObject effect = Instantiate(shotEffect, shotPosition.position, shotPosition.rotation);
        Destroy(effect, 1.0f);
        StartCoroutine(CloseShootAni());
    }
    IEnumerator CloseShootAni()
    {
        yield return new WaitForSeconds(1.5f);
        ani.SetBool("Shoot", false);
    }
}
