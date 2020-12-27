using UnityEngine;
using UnityEngine.EventSystems;

public class Sunflower : Tower, IModifiable
{
    public float rate;
    public int addAmount;

    private float timer = 0.0f;

    private BuildManager buildManager;

    public GameObject sunEffect;

    public void DestroyTower()
    {
        nodeBuiltOn.SetBuilt(false);
        if (this == null || this.gameObject == null)
            return;
        Destroy(this.gameObject);
        return;
    }

    public TowerCollection GetCollection()
    {
        return this.towerCollection;
    }

    public int GetLevel()
    {
        return this.level;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public float GetRange()
    {
        return 0.0f;
    }

    public int GetSellAmount()
    {
        int value = 0;
        for (int i = 0; i <= level; i++)
        {
            value += towerCollection.towers[i].cost;
        }
        return (int)(value * 0.6);
    }

    public string GetUpgradeMoney()
    {
        if (level + 1 >= towerCollection.towers.Length)
        {
            return "MAX LEVEL";
        }
        return towerCollection.towers[level + 1].cost.ToString();
    }

    public void OnMouseEnter()
    {
        
    }

    public void OnMouseExit()
    {
        
    }

    public void OnMouseUpAsButton()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        buildManager.SetModify(this);
    }

    public void SetCollection(TowerCollection towerCollection)
    {
        this.towerCollection = towerCollection;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    private void Start()
    {
        buildManager = BuildManager.GetInstance();
        timer = 0.0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1 / rate)
        {
            PlayerStats.GetInstance().AddMoney(addAmount);
            timer = 0;
        }
    }
}
