using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffTower : Tower, IMouseDetection
{
    private List<EnemyStats> enemies = new List<EnemyStats>();

    public float range;
    public float debuffExtend;

    public GameObject slowdownEffect;

    public BuffType debuffType;

    public GameObject rangeEffect;
    private GameObject tmpRangeEffect;

    private void Start()
    {
        InvokeRepeating("FindEnemies", 0.0f, 0.2f);
        InvokeRepeating("notify", 0.0f, 0.2f);
    }

    private void FindEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        for(int i = 0; i < colliders.Length; i ++)
        {
           
            if (colliders[i].tag == "Enemy")
            {
                EnemyStats enemy;

                if (colliders[i].TryGetComponent<EnemyStats>(out enemy))
                {
                    // 给enemy记录是谁的debuff，已经有debuff的话就不管了
                    if(!enemy.GetDebuffed(debuffType))
                    {
                        enemy.SetDebuffTower(debuffType, this);
                    }
                }
            }
        }
    }
    public void notify()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                remove(enemies[i]);
                continue;
            }
            enemies[i].update(this.transform.position, range, debuffExtend,slowdownEffect,debuffType);
        }
    }

    public void register(EnemyStats e)
    {
        enemies.Add(e);
        //Debug.Log(this.GetInstanceID() + " has registered an enemy " );
    }

    public void remove(EnemyStats enemy)
    {
        enemies.Remove(enemy);
        //Debug.Log(this.GetInstanceID() + " has removed an enemy " + enemies.Remove(enemy));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void OnMouseEnter()
    {
        if (tmpRangeEffect != null)
        {
            Destroy(tmpRangeEffect);
        }
        tmpRangeEffect = Instantiate(rangeEffect);
        tmpRangeEffect.transform.position = transform.position;
        tmpRangeEffect.transform.localScale = new Vector3(range, 1.0f, range);
    }

    public void OnMouseExit()
    {
        Destroy(tmpRangeEffect);
    }

    public void OnMouseUpAsButton()
    {

    }
}
