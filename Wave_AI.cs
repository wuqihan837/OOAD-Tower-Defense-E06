using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Wave_AI
{
    public EnemyPerWave[] enemyPerWave;
    public float spawnRate;

    public Transform[] path;
    public Transform START;

    [System.Serializable]
    public class EnemyPerWave
    {
        public GameObject enemyPrefab;
        public int count;
    }
}
