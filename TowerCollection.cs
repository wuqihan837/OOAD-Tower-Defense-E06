using UnityEngine;

[System.Serializable]
public class TowerCollection
{
    public TowerInfo[] towers;

    [System.Serializable]
    public class TowerInfo
    {
        public GameObject towerPrefab;
        public int cost;
    }
}
