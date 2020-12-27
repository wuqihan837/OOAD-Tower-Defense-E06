using System;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : AttackableTower
{
    public LineRenderer lineRenderer;

    private List<Tuple<float, EnemyStats>> potentialEnemies = new List<Tuple<float, EnemyStats>>();

    protected override void Update()
    {
        //text.text = attackPower.ToString();

        if (currentEnemy == null)
        {
            shotEffect.SetActive(false);
            return;
        }

        RotateToEnemy();
        Shot();
        if(!shotEffect.activeInHierarchy)
        {
            shotEffect.SetActive(true);
        }
            
    }

    protected override void FindNearestEnemy()
    {
        List<Tuple<float, EnemyStats>> enemies = new List<Tuple<float, EnemyStats>>();
        // range范围内的所有物体
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
        // 遍历，找tag为enemy且最近的
        foreach (Collider col in colliders)
        {
            if (col.tag == "Enemy")
            {
                float dis = Vector3.Distance(col.transform.position, transform.position);
                EnemyStats tmp;
                if(col.TryGetComponent<EnemyStats>(out tmp))
                    enemies.Add(new Tuple<float, EnemyStats>(dis, tmp));
            }
        }
        if (enemies.Count == 0)
        {
            lineRenderer.enabled = false;
            return;
        }
        
        enemies.Sort((x, y) => x.Item1.CompareTo(y.Item1));
        currentEnemy = enemies[0].Item2.transform;
        potentialEnemies = enemies.GetRange(0, Math.Min(level + 1, enemies.Count));
    }

    protected override void Shot()
    {
        if (potentialEnemies.Count == 0)
        {
            return;
        }

        float attackCoe = PlayerStats.GetInstance().TowerAttackAmpl();

        List<Vector3> pos = new List<Vector3>();
        pos.Add(shotPosition.position);
        for(int i = 0; i < potentialEnemies.Count; i ++)
        {
            EnemyStats e = potentialEnemies[i].Item2;

            if (e != null)
            {
                e.TakenDamage(Time.deltaTime * attackPower * attackCoe);
                pos.Add(e.GetShotPosition().position);
            }
        }
        lineRenderer.enabled = true;
        lineRenderer.positionCount = (pos.Count);
        lineRenderer.SetPositions(pos.ToArray());
    }
}
