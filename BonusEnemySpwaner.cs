using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusEnemySpwaner : MonoBehaviour
{
    public static int CountEnemyAlive = 0;
    public Wave[] waves;
    public float waveRate = 0.2f;

    public GameObject waypointEffect;
    public GameObject startSpawnerEffect;

    public bool canGenerate = true;

    private void Awake()
    {
        canGenerate = true;
    }

    public void GenerateBonus()
    {
        if (canGenerate)
        {
            canGenerate = false;
            StartCoroutine(SpawnEnemy());
        }
        
    }

    private IEnumerator SpawnEnemy()
    {
        foreach (Wave wave in waves)
        {
            prompt_move_ctrl.instance.prompt_appear(wave.hint, 80);
            yield return new WaitForSeconds(4.0f);

            // 先获取并显示路径
            List<int> pathIndeces = new List<int>();
            foreach (Wave.OnePathManage onePath in wave.oneWave)
            {
                Paths.Path p = Paths.GetInstance().allPaths[onePath.pathIndex];
                StartCoroutine(ShowPath(p));
            }
            yield return new WaitForSeconds(5.0f);

            // 往所有路上生成
            for (int i = 0; i < wave.oneWave.Length; i++)
            {
                int index = wave.oneWave[i].pathIndex;
                Paths.Path onePath = Paths.GetInstance().allPaths[index];
                StartCoroutine(InstanEnemies(wave.oneWave[i].enemiesOnOnePath, onePath));
            }

            // 等到死完了，到下一波
            while (CountEnemyAlive > 0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(wave.waveInterval);
        }
        yield return new WaitForSeconds(5.0f);
        canGenerate = true;
    }

    private IEnumerator InstanEnemies(Wave.EnemyCount[] enemies, Paths.Path onePath)
    {
        Vector3 start = onePath.start.position;
        Transform[] path = onePath.onePath;
        foreach (Wave.EnemyCount enemyCount in enemies)
        {
            GameObject obj = enemyCount.enemyObj;
            int num = enemyCount.enemyNum;
            for (int i = 0; i < num; i++)
            {
                GameObject spwanerStart = Instantiate(startSpawnerEffect, start, Quaternion.identity);
                Destroy(spwanerStart, 5.0f);
                GameObject enemyObj = Instantiate(obj, start, Quaternion.identity);
                EnemyMovement movement = enemyObj.GetComponent<EnemyMovement>();
                movement.SetPath(path);

                CountEnemyAlive++;

                yield return new WaitForSeconds(enemyCount.interval);
            }
        }
    }

    private IEnumerator ShowPath(Paths.Path path)
    {
        float interval = 0.5f;
        foreach (Transform item in path.onePath)
        {
            GameObject obj = Instantiate(waypointEffect, item.position, Quaternion.identity);
            Destroy(obj, 5.0f);
            yield return new WaitForSeconds(interval);
        }
    }
}
