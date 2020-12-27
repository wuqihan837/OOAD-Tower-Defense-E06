using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner_AI : MonoBehaviour
{
    public CanvasGroup nextLevelCancas;
    public static int CountEnemyAlive = 0;
    public Wave_AI[] waves;
    public float waveRate = 0.2f;

    private static EnemySpawner_AI instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //StartSpawning();
    }

    public static EnemySpawner_AI GetInstance()
    {
        return instance;
    }
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemy());
    }
    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(4.0f);
        foreach (Wave_AI wave in waves)
        {
            int l = wave.enemyPerWave.Length;
            // 生成这一波中的每一种怪
            for (int i = 0; i < l; i++)
            {
                int enemyCount = wave.enemyPerWave[i].count;
                for (int j = 0; j < enemyCount; j++)
                {
                    GameObject enemyObj = Instantiate(wave.enemyPerWave[i].enemyPrefab, wave.START.position, Quaternion.identity);
                    EnemyMoveAI movement = enemyObj.GetComponent<EnemyMoveAI>();
                    movement.SetPath(wave.path);
                    CountEnemyAlive++;

                    // 一波中小怪的间隔
                    yield return new WaitForSeconds(wave.spawnRate);
                }
            }
            while (CountEnemyAlive > 0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
        }
        Debug.Log("Here");
        Reset();
        game_ctrl.setfin();
        //ShowCanvas();
    }

    private void Reset()
    {
        UIController.GetUIController().SetCanvasInactive();
    }

    private void ShowCanvas()
    {
        nextLevelCancas.alpha = 1;
        nextLevelCancas.blocksRaycasts = true;
        nextLevelCancas.interactable = true;
    }

}
