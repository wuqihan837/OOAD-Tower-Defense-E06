using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float speed;
    public float getShotDis;

    [SerializeField]
    protected float damage;

    public EnemyStats enemyStats;

    protected GameObject hitEffect;

    private void Awake()
    {
        // 3秒还没有打到敌人，销毁自己
        Destroy(gameObject, 3.0f);
    }

    public void SetTarget(EnemyStats _target)
    {
        enemyStats = _target;
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }    


    public void SetHitEffect(GameObject hit)
    {
        this.hitEffect = hit;
    }
    private void MoveTorwardsTarget(Vector3 pos)
    {   
        Vector3 dir = pos - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    }

    // 作为击中目标的判断，击中了就销毁自己
    protected abstract void HitTarget();

    public void UpdateBullet(Vector3 targetPos)
    {
        MoveTorwardsTarget(targetPos);
        if (Vector3.Distance(transform.position, targetPos) < getShotDis)
        {
            HitTarget();
        }
    }

    private void OnDestroy()
    {
        enemyStats.Remove(this);
    }
}
