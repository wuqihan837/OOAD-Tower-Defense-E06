
using UnityEngine;

public class EneStatsSpeedUp : EnemyStats
{
    public GameObject speedupEffect;
    public override void TakenDamage(float _damage)
    {
        base.TakenDamage(_damage);
        if(canSkill && health / initialHealth < 0.2f)
        {
            GameObject eff = Instantiate(speedupEffect, shotPosition.position, Quaternion.identity);
            Destroy(eff, 2.0f);
            ChangeSpeed(2.0f);
            canSkill = false;
        }
    }
}
