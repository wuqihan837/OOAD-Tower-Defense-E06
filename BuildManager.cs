using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // 单例
    private static BuildManager instance;
    // 缓存当前所选node
    private Node selectedNode;
    // 控制UI
    private UIController uIController;

    private IModifiable selectedTower;

    public GameObject upgradeEffect;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    public static BuildManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        uIController = UIController.GetUIController();
        //piuiControl = piui_control.getpiuiController();
    }

    // 存当前有多少个buff塔
    private List<BuffTower> buffList = new List<BuffTower>();

    public void SetBuild(Node node, string id)
    {
        // 反选
        if (node == selectedNode)
        {
            selectedNode = null;
            uIController.SetCanvasInactive();
            return;
        }
        uIController.SetCanvasInactive();
        selectedNode = node;
        uIController.SetAliveCanvas(id);
        uIController.SetCanvasActive(Input.mousePosition);
    }



    // 显示升级
    public void SetModify(IModifiable aTower)
    {
        //uIController.SetCanvasInactive();
        if (aTower == selectedTower)
        {
            DeselectModifyTower();
            return;
        }
        uIController.SetAliveCanvas("modify");
        uIController.SetCanvasActive(Input.mousePosition);
        uIController.ShowMoneyForModify(aTower.GetSellAmount().ToString(), aTower.GetUpgradeMoney().ToString());

        selectedTower = aTower;
    }

    private void DeselectModifyTower()
    {
        selectedTower = null;
    }

    // 负责具体建造，由shop（初次建造）和node（升级）调用
    public void BuildOnNode(TowerCollection newTowerCollection, int level)
    {
        TowerCollection.TowerInfo newTower = newTowerCollection.towers[level];
        // 没钱就不build
        if (PlayerStats.GetInstance().money < newTower.cost)
        {
            return;
        }
        // 没选上node
        if (selectedNode == null)
        {
            return;
        }
        if(selectedNode.GetBuilt())
        {
            return;
        }
        // 有钱就build
        PlayerStats.GetInstance().CostEnergy(newTower.cost);

        //Debug.Log(selectedNode);

        GameObject anyTower = Instantiate(newTower.towerPrefab, selectedNode.buildPosition.position, Quaternion.identity);
        // build完也要加上effect
        if(level > 0)
        {
            Vector3 tmp = new Vector3(selectedNode.buildPosition.position.x,0.0f, selectedNode.buildPosition.position.z);
            GameObject effect = Instantiate(upgradeEffect, tmp, Quaternion.identity);
            Destroy(effect, 2.0f);
        }

        Tower tower = anyTower.GetComponent<Tower>();
        tower.SetNode(selectedNode);
        if(tower is AttackableTower)
        {
            AttackableTower aTower = (AttackableTower)tower;
            Check(aTower);
            aTower.SetCollection(newTowerCollection);
            aTower.SetLevel(level);
        }
        // 是attack类的
        // 是buff类，初始化就行了
        if (tower is BuffTower)
        {
            BuffTower bTower = (BuffTower)tower;
            buffList.Add(bTower);
        }
        if(tower is Sunflower)
        {
            Sunflower sunflower = (Sunflower)tower;
            sunflower.SetCollection(newTowerCollection);
            sunflower.SetLevel(level);
        }
        // 关掉建造的UI
        uIController.SetCanvasInactive();

        // 清空选择的node
        selectedNode = null;
    }

   

    // 涉及到buffer list的更新，所以需要这里来做
    // 0加钱，1不加钱
    public void DestroyTower(int mode)
    {
        if (selectedTower == null)
        {
            return;
        }

        if (mode == 0)
        {
            PlayerStats.GetInstance().money += selectedTower.GetSellAmount();
        }
        if (mode == 1)
        { }
        
        if(selectedTower is AttackableTower)
        {
            AttackableTower aTower = (AttackableTower)selectedTower;
            foreach (BuffTower buff in buffList)
            {
                buff.Delete(aTower);
            }
        }
        //Debug.Log("Destroy 2!!" + selectedTower);
        selectedTower.DestroyTower();
        selectedTower = null;
        uIController.SetCanvasInactive();
    }

    // 升级塔
    public void UpgradeTower()
    {
        if (selectedTower == null)
        {
            return;
        }
        TowerCollection towers = selectedTower.GetCollection();
        int level = selectedTower.GetLevel();
        // 超过最高级的话，不做
        if(level + 1 >= towers.towers.Length)
        {
            // 以防万一，置空
            selectedTower = null;
            return;
        }
        // 没钱，不做
        if(towers.towers[level + 1].cost > PlayerStats.GetInstance().money)
        {
            return;
        }

        selectedNode = selectedTower.GetNode();
        // 先拆掉原来的，1不加钱
        DestroyTower(1);
        // 在build新的

        BuildOnNode(towers, level + 1);
        DeselectModifyTower();
        //Debug.Log("Upgrade!!" + selectedNode);
    }

    private void Check(AttackableTower aTower)
    {
        if (buffList.Count == 0)
        {
            return;
        }
        foreach (BuffTower buff in buffList)
        {
            buff.Register(aTower);
        }
    }
}
