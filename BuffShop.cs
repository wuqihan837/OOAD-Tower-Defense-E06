using System.Collections.Generic;
using UnityEngine;

public class BuffShop : Shop
{
    public TowerCollection addAttack;
    public TowerCollection addRange;

    public TowerCollection reduceSpeed;
    public TowerCollection poison;


    public int addAttackCount = 1;
    public int addRangeCount = 1;
    public int reduceSpeedCount = 1;
    public int poisonCount = 1;

    private static BuffShop instance;

    private float timer;
    private float triggerInterval = 1.0f;
    private float timeElapse;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        timer = Time.time;
        timeElapse = 0.0f;

       poisonCount = 1;
        reduceSpeedCount = 1;
        addRangeCount = 1;
        addAttackCount = 1;
    }

    private void Update()
    {
        timeElapse = Time.time - timer;
    }
    public static BuffShop GetInstance()
    {
        return instance;
    }

    public Dictionary<UIType, string> GetBuffPrice()
    {
        Dictionary<UIType, string> dic = new Dictionary<UIType, string>();
        dic.Add(UIType.addAttack, addAttack.towers[0].cost.ToString());
        dic.Add(UIType.slowSpeed, reduceSpeed.towers[0].cost.ToString());
        dic.Add(UIType.poison, poison.towers[0].cost.ToString());
        dic.Add(UIType.addRange, addRange.towers[0].cost.ToString());
        return dic;
    }
    public void BuildAddAttackTower()
    {
        if(timeElapse < triggerInterval)
        {
            return;
        }
        timer = Time.time;
        if(addAttackCount > 0)
        {
            base.BuildTower(addAttack);
            addAttackCount--;
        }
            
    }    
    
    public void BuildAddRangeTower()
    {
        if (timeElapse < triggerInterval)
        {
            return;
        }
        timer = Time.time;
        if (addRangeCount > 0)
        {
            base.BuildTower(addRange);
            addRangeCount--;
        }   
    }

    public void BuildReduceSpeed()
    {
        if (timeElapse < triggerInterval)
        {
            return;
        }
        timer = Time.time;
        Debug.Log("Triggered speed" + reduceSpeedCount);
        if (reduceSpeedCount > 0)
        {
            base.BuildTower(reduceSpeed);
            reduceSpeedCount--;
        }
    }

    public void BuildPoison()
    {
        if (timeElapse < triggerInterval)
        {
            return;
        }
        timer = Time.time;
        Debug.Log("Triggered poison" + poisonCount);
        if (poisonCount > 0)
        {
            base.BuildTower(poison);
            poisonCount--;
        }
    }
}
