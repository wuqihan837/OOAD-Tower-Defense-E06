using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class AttackableTower :Tower, IModifiable
{
    [SerializeField]
    protected float attackRange;
    [SerializeField]
    protected float attackPower;
    [SerializeField]
    private float shotRate;
    [SerializeField]
    private float turnSpeed;
    protected Transform currentEnemy;
    protected EnemyStats currentEnemyStats;

    public Transform shotPosition;

    public GameObject rangeEffect;
    private GameObject tmpRangeEffect;

    public GameObject shotEffect;

    // 计时用
    protected float timer;

    //public Text text;

    protected BuildManager buildManager;

    private void Start()
    {
        // 定时运行检测
        InvokeRepeating("FindNearestEnemy", 0.0f, 0.2f);

        timer = 0.0f;

        buildManager = BuildManager.GetInstance();
    }

    protected virtual void Update()
    {
        //text.text = attackPower.ToString() ;

        if (currentEnemy == null)
        {
            return;
        }

        RotateToEnemy();

        timer += Time.deltaTime;
        if (timer >= 1.0 / shotRate)
        {
            Shot();
        }
    }

    abstract protected void Shot();

    protected void RotateToEnemy()
    {
        // 确定朝向
        Vector3 dir = currentEnemy.position - transform.position;
        // 转
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Quaternion lerp = Quaternion.Lerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
        Vector3 rotateAngle = lerp.eulerAngles;
        transform.rotation = Quaternion.Euler(0, rotateAngle.y, 0);
        //transform.rotation = Quaternion.Euler(rotateAngle);
    }

    protected virtual void FindNearestEnemy()
    {
        // range范围内的所有物体
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
        // 遍历，找tag为enemy且最近的
        Transform nearestEnemy = null;
        float nearestDis = Mathf.Infinity;
        foreach (Collider col in colliders)
        {
            if (col.tag == "Enemy")
            {
                float dis = Vector3.Distance(col.transform.position, transform.position);
                if (dis < nearestDis)
                {
                    nearestDis = dis;
                    nearestEnemy = col.gameObject.transform;
                    
                }
            }
        }
        currentEnemy = nearestEnemy;
        if (currentEnemy != null)
        {
            currentEnemyStats = currentEnemy.GetComponent<EnemyStats>();
        }
    }

    public void AddBuff(GameObject effect, BuffType buffType, float buffNumber)
    {
        switch(buffType)
        {
            case BuffType.AddAttackPower:
                attackPower *= buffNumber;
                break;
            case BuffType.AddRange:
                attackRange *= buffNumber;
                break;
        }
        Instantiate(effect, transform.position, Quaternion.identity, transform);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public void OnMouseUpAsButton()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        buildManager.SetModify(this);
    }

    public void OnMouseEnter()
    {
        if (tmpRangeEffect != null)
        {
            Destroy(tmpRangeEffect);
        }
        //rangeEffect.SetActive(true);
        //rangeEffect.transform.sc = new Vector3(attackRange, 1.0f, attackRange);
        tmpRangeEffect = Instantiate(rangeEffect,transform);
        tmpRangeEffect.transform.position = transform.position;
        tmpRangeEffect.transform.localScale = new Vector3(attackRange, 1.0f, attackRange);
    }

    public void OnMouseExit()
    {
        Destroy(tmpRangeEffect);
    }

    public int GetSellAmount()
    {
        int value = 0;
        for (int i = 0; i <= level; i++)
        {
            value += towerCollection.towers[i].cost;
        }
        return (int)(value * 0.6);
    }

    public void DestroyTower()
    {
        nodeBuiltOn.SetBuilt(false);
        if (this == null || this.gameObject == null)
            return;
        Destroy(this.gameObject);
        return;
    }

    public string GetUpgradeMoney()
    {
        if (level + 1 >= towerCollection.towers.Length)
        {
            return "MAX LEVEL";
        }
        return towerCollection.towers[level + 1].cost.ToString() ;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public TowerCollection GetCollection()
    {
        return this.towerCollection;
    }

    public int GetLevel()
    {
        return this.level;
    }

    public void SetCollection(TowerCollection towerCollection)
    {
        this.towerCollection = towerCollection;
    }

    public void SetLevel(int level)
    {
        this.level = level;
    }

    public float GetRange()
    {
        return this.attackRange;
    }
}
