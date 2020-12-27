using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public float initialSpeed;
    public float initialHealth;

    [SerializeField]
    private float speed;
    public float health;

    public Slider hpSlider;

    private List<Bullet> bullets = new List<Bullet>();

    private Dictionary<BuffType, bool> debuffStats = new Dictionary<BuffType, bool>();
    private Dictionary<BuffType, DebuffTower> debuffOrigin = new Dictionary<BuffType, DebuffTower>();
    private Dictionary<BuffType, GameObject> debuffEffects = new Dictionary<BuffType, GameObject>();

    public GameObject deathEffect;
    public GameObject endPathEffect;

    public Transform shotPosition;

    protected bool canSkill;

    private void Awake()
    {
        speed = initialSpeed;
        health = initialHealth;
        canSkill = true;
    }

    public virtual void TakenDamage(float _damage)
    {
        health -= _damage;
        hpSlider.value = health / initialHealth;
        if (health <= 0.0f)
        {
            Death();
        }
    }
    
    public void SetDebuffTower(BuffType debuffType,DebuffTower debuffOrigin)
    {
        DebuffTower debuffTower = GetDebuffOrigin(debuffType);
        // 之前没有这个类型，就加上去
        if (debuffTower == null)
        {
            SetDebuffOrigin(debuffType, debuffOrigin);
            debuffOrigin.register(this);
        }
        // 之前有过debuff经历，但和这个新的一样，就不管
        else if(debuffTower == debuffOrigin)
        {
            
        }
        // 有新的来接管了
        else if(debuffTower != debuffOrigin)
        {
            debuffTower.remove(this);
            debuffOrigin.register(this);
            SetDebuffOrigin(debuffType, debuffOrigin);
        }
        //Debug.Log(debuffOrigin.gameObject.GetInstanceID());
    }
    public void update(Vector3 towerPos, float range, float effectNum, GameObject spEffect, BuffType buffType)
    {
        float disToTower = Vector3.Distance(towerPos, this.transform.position);
        // 进入范围且没有debuff，所以需要添加debuff
        if (disToTower <= range && !GetDebuffed(buffType))
        {
            //Debug.Log("within and not debuff " + towerPos+ " "+ disToTower);
            SetDebuff(buffType, true);
            DoDebuff(buffType, effectNum,spEffect);
        }
        // 进入范围有buff
        else if (disToTower <= range)
        {
            //Debug.Log("within and debuff " + towerPos+" " + disToTower);
        }
        // 出去范围有buff，就要去掉buff，恢复原始状态
        else if (disToTower > range && GetDebuffed(buffType))
        {
            SetDebuff(buffType, false);
            UndoDebuff(buffType);
            //Debug.Log("without and debuff " + towerPos + " " + disToTower);
        }
        // 出去范围也无debuff
        else if (disToTower > range)
        {
            //Debug.Log("without and not debuff " + towerPos + " " + disToTower);
        }
    }

    private void DoDebuff(BuffType debuffType, float debuffNumber, GameObject effect)
    {
        switch(debuffType)
        {
            case BuffType.SlowDown:
                ChangeSpeed(debuffNumber);
                break;
            case BuffType.Poison:
                //TakenDamage(Time.deltaTime * debuffNumber);
                StartCoroutine("DoPoison", debuffNumber);
                break;
        }
        SetupSpecialEffect(debuffType, effect);
    }

    private void SetupSpecialEffect(BuffType debuff, GameObject effect)
    {
        GameObject prevEffect = GetDebuffEffect(debuff);
        if (prevEffect == null)
        {
            prevEffect = Instantiate(effect, transform.position, Quaternion.identity, transform);
            SetDebuffEffect(debuff, prevEffect);
        }
    }

    private void UndoDebuff(BuffType debuff)
    {
        switch(debuff)
        {
            case BuffType.SlowDown:
                SpeedRecover();
                break;

            case BuffType.Poison:
                StopCoroutine("DoPoison");
                Debug.Log("Cancel Poison");
                break;
        }
        DebuffTower tower = GetDebuffOrigin(debuff);
        if(tower != null)
        {
            tower.remove(this);
        }
        GameObject effect = GetDebuffEffect(debuff);
        if(effect != null)
        {
            Destroy(effect);
        }
        SetDebuffEffect(debuff, null);
    }

    protected void ChangeSpeed(float fraction)
    {
        speed *= fraction;
    }

    private void SpeedRecover()
    {
        speed = initialSpeed;
    }


    private IEnumerator DoPoison(float damage)
    {
        float deltaTime = 0.5f;
        while(true)
        {
            TakenDamage(deltaTime * damage);
            yield return new WaitForSeconds(deltaTime);
        }
    }
    protected virtual void Death()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            Destroy(bullets[i].gameObject);
        }

        GameObject obj = Instantiate(deathEffect,shotPosition.position, Quaternion.identity);
        Destroy(obj, 2.0f);
        PlayerStats.GetInstance().AddEnemyKill();
        Destroy(gameObject);
    }

    public virtual void EndPath()
    {
        GameObject endEffect = Instantiate(endPathEffect, transform.position, Quaternion.identity);
        Destroy(endEffect, 3.0f);
        PlayerStats.GetInstance().MissAnEnemy(health / initialHealth);
        Destroy(gameObject);
    }

    public float GetSpeed()
    {
        return speed;
    }

    public bool GetDebuffed(BuffType buff)
    {
        bool isDebuffed = false;
        // 若不存在，则out为false；所以说就算还没有遇到过这个buff，也是false
        debuffStats.TryGetValue(buff, out isDebuffed);
        return isDebuffed;
    }

    private void SetDebuff(BuffType debuff, bool status)
    {
        debuffStats[debuff] = status;
    }

    private DebuffTower GetDebuffOrigin(BuffType debuff)
    {
        DebuffTower tower = null;
        debuffOrigin.TryGetValue(debuff, out tower);
        return tower;
    }

    private void SetDebuffOrigin(BuffType type,DebuffTower tower)
    {
        debuffOrigin[type] = tower;
    }

    private GameObject GetDebuffEffect(BuffType debuff)
    {
        GameObject effect = null;
        debuffEffects.TryGetValue(debuff, out effect);
        return effect;
    }

    private void SetDebuffEffect(BuffType debuff, GameObject effect)
    {
        debuffEffects[debuff] = effect;
    }

    private void Update()
    {
        NotifyBullets();
    }

    public void Register(Bullet bullet)
    {
        if(bullet == null)
        {
            return;
        }
        bullets.Add(bullet);
        //Debug.Log("Register:"  + bullet.GetInstanceID());
    }

    public void NotifyBullets()
    {
        for(int i = 0; i < bullets.Count; i ++)
        {
            bullets[i].UpdateBullet(shotPosition.position);
        }
    }

    public void Remove(Bullet bullet)
    {
        if(bullet == null)
        {
            return;
        }
        bullets.Remove(bullet);
        //Debug.Log("Remove:" + bullet.GetInstanceID());
    }

    public Transform GetShotPosition()
    {
        return shotPosition;
    }

    private void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;
    }
}
