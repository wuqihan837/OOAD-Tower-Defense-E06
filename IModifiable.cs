using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IModifiable : IMouseDetection
{
    int GetSellAmount();

    void DestroyTower();

    Vector3 GetPosition();

    string GetUpgradeMoney();
    void SetCollection(TowerCollection towerCollection);
    void SetLevel(int level);
    TowerCollection GetCollection();
    int GetLevel();
    Node GetNode();

    float GetRange();
}
