using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class EnemyMoveAI : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform[] path;

    private EnemyStats enemyStats;

    public float arriveEndDis = 3.0f;

    private int waypointIndex;

    private void Start()
    {
        waypointIndex = 0;
    }

    private void SetEnd(Vector3 end)
    {
        agent.SetDestination(end);
        Run();
    }

    public void SetPath(Transform[] p)
    {
        path = p;
        // 开始跑
        SetEnd(p[0].position);
    }

    private void Run()
    {
        enemyStats = transform.GetComponent<EnemyStats>();
        agent.speed = enemyStats.GetSpeed();
    }

    public void SetSpeed(float s)
    {
        agent.speed = s;
    }

    public void GetSpeed()
    {
        Debug.Log(GetInstanceID().ToString() + " " + agent.speed);
    }

    private void Update()
    {
        float disToTarget = Vector3.Distance(transform.position, agent.destination);
        // 到达当前目的地
        if (waypointIndex <= path.Length - 2 && disToTarget < arriveEndDis)
        {
            waypointIndex++;
            SetEnd(path[waypointIndex].position);
        }
        else if(waypointIndex >= path.Length - 1 && disToTarget < arriveEndDis)
        {
            FinishPath();
        }
    }

    private void FinishPath()
    {
        // 到达终点，算miss
        //PlayerStats.GetInstance().MissAnEnemy();
        Destroy(gameObject);
        return;
    }

    private void OnDestroy()
    {
        EnemySpawner_AI.CountEnemyAlive--;
    }
}
