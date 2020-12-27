using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public abstract class Shop : MonoBehaviour
{
    // 子类统一通过父类方法建塔
    protected void BuildTower(TowerCollection tower)
    {
        //Debug.Log(tower);
        //此时必定是第一次build，所以从0级开始
        BuildManager.GetInstance().BuildOnNode(tower, 0);
    }
}
