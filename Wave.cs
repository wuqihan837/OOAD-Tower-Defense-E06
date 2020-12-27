using System.Collections;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public OnePathManage[] oneWave;
    public float waveInterval;

    public string hint;

    [System.Serializable]
    public class OnePathManage
    {
        public int pathIndex;
        public EnemyCount[] enemiesOnOnePath;
    }

    [System.Serializable]
    public class EnemyCount
    {
        public GameObject enemyObj;
        public int enemyNum;
        public float interval;
    }
}


