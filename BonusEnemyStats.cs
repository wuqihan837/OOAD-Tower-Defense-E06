using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusEnemyStats : EnemyStats
{
    public int bonusEnergy = 10;

    //多加钱
    protected override void Death()
    {
        PlayerStats.GetInstance().AddMoney(bonusEnergy);
        base.Death();
    }
    private void OnDestroy()
    {
        BonusEnemySpwaner.CountEnemyAlive--;
    }
}
