using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EneStatsBlockOneDam : EnemyStats
{
    public GameObject[] cups;
    private int index = 0;
    public float propability = 0.3f;

    public GameObject blockEffect;
    public override void TakenDamage(float _damage)
    {
        if(canSkill && _damage > 20.0f)
        {
            if(Random.Range(0.0f, 1.0f) < propability)
            {
                GameObject eff = Instantiate(blockEffect, shotPosition.position,Quaternion.identity);
                Destroy(eff, 2.0f);
                Destroy(cups[index]);
                index++;
                if(index >= cups.Length)
                {
                    canSkill = false;
                }
                return;
            }
        }

        base.TakenDamage(_damage);
    }
}
