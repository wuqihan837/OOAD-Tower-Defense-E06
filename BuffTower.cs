using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTower : Tower, IBuffTower, IMouseDetection
{
    public float buffRange;
    public BuffType buffType;
    public float buffEffectNumber;

    public GameObject buffEffect;

    public GameObject rangeEffect;
    private GameObject tmpRangeEffect;

    private List<AttackableTower> attackableTowerInRange = new List<AttackableTower>();

    public void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,buffRange);
        foreach (Collider col in colliders)
        {
            AttackableTower tmp = col.GetComponent<AttackableTower>();

            if (tmp != null)
            {
                DoBuff(tmp);
                attackableTowerInRange.Add(tmp);
            }
        }
    }

    public void DoBuff(AttackableTower attackableTower)
    {
        attackableTower.AddBuff(buffEffect,buffType, buffEffectNumber);
    }

    public void Delete(AttackableTower attackableTower)
    {
        attackableTowerInRange.Remove(attackableTower);
    }

    public void Register(AttackableTower attackableTower)
    {
        if(Vector3.Distance(transform.position, attackableTower.transform.position) <= buffRange)
        {
            attackableTowerInRange.Add(attackableTower);
            DoBuff(attackableTower);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, buffRange);
    }

    public void OnMouseEnter()
    {
        if(tmpRangeEffect !=null)
        {
            Destroy(tmpRangeEffect);
        }
        tmpRangeEffect = Instantiate(rangeEffect);
        tmpRangeEffect.transform.position = transform.position;
        tmpRangeEffect.transform.localScale = new Vector3(buffRange, 1.0f, buffRange);
    }

    public void OnMouseExit()
    {
        Destroy(tmpRangeEffect);
    }

    public void OnMouseUpAsButton()
    {
        
    }
}
