using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform[] path;
    private Transform target;
    private int wavepointIndex = 0;
    public float arriveEndDis = 3.0f;

    private EnemyStats enemyStats;

    private void Start()
    {
        enemyStats = transform.GetComponent<EnemyStats>();
        wavepointIndex = 0;
    }

    private void Update()
    {
        // 以防万一有时间差，没有附上值
        if (target == null) return;

        Move();

        transform.LookAt(target.position);

        if (Vector3.Distance(transform.position, target.position) <= arriveEndDis)
        {
            GetNextWaypoint();
        }

    }

    private void Move()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * Time.deltaTime * enemyStats.GetSpeed(), Space.World);
    }
    private void GetNextWaypoint()
    {
        if (wavepointIndex >= path.Length - 1)
        {
            EndPath();
            return;
        }
        wavepointIndex++;
        target = path[wavepointIndex];
    }

    private void EndPath()
    {
        enemyStats.EndPath();
        return;
    }

    public void SetPath(Transform[] _path)
    {
        // 选择path
        path = _path;
        target = path[0];
    }
}
