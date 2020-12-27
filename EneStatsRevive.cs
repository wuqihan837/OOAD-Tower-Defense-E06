using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneStatsRevive : EnemyStats
{
    public float probability = 0.1f;
    public GameObject reviveEffect;
    public override void TakenDamage(float _damage)
    {
        health -= _damage;
        hpSlider.value = health / initialHealth;
        if(health <= 0.0f)
        {
            if(canSkill && Random.Range(0.0f, 1.0f) < probability)
            {
                health = initialHealth / 2;
                hpSlider.value = health / initialHealth;
                GameObject eff = Instantiate(reviveEffect, transform.position, Quaternion.identity, transform);
                Destroy(eff, 2.0f);
                canSkill = false;
            }
            else
            {
                Death();
            }
        }
    }
}
