using UnityEngine;

public class StandardTower : AttackableTower
{
    public GameObject bulletPrefab;

    public GameObject hitEffect;
    protected override void Shot()
    {
        timer = 0.0f;
        // 创建子弹，获取子弹的脚本组件，指定目标敌人

        float attackCoe = PlayerStats.GetInstance().TowerAttackAmpl();

        GameObject bulletObj = Instantiate(bulletPrefab, shotPosition.position, shotPosition.rotation);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.SetTarget(currentEnemyStats);
        bullet.SetDamage(attackPower * attackCoe);
        bullet.SetHitEffect(hitEffect);

        // 在enemy register上这个bullet
        currentEnemyStats.Register(bullet);

        GameObject effect = Instantiate(shotEffect,shotPosition.position,shotPosition.rotation);
        Destroy(effect, 1.0f);
    }
}
