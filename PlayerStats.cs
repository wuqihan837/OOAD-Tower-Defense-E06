using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats player;

    public int money;
    public float health;

    public int startMoney;
    public float startHealth;

    public Image energyRatio;
    public TMP_Text energyText;

    public Image healthRatio;
    public TMP_Text gpaText;

    // kill去enemy stats里找。死了算kill一个。
    [SerializeField]
    private int enemyKilled = 0;
    // miss去enemy move ai里找。到达终点算miss
    [SerializeField]
    private int enemyMiss = 0;
    // 在game ctrl里
    [SerializeField]
    private float playTime = 0.0f;
    // energy在这里
    [SerializeField]
    private int energySpent = 0;

    public static PlayerStats GetInstance()
    {
        return player;
    }

    private void Awake()
    {
        if (player == null)
        {
            player = this;
        }
        money = startMoney;
        health = startHealth;
    }


    private void Start()
    {
        enemyKilled = 0;
        enemyMiss = 0;
        playTime = 0.0f;
        energySpent = 0;
    }

    public void AddMoney(int amount)
    {
        money = Mathf.Min(money + amount, startMoney);
    }

    private void Update()
    {
        //moneyText.text = money.ToString();
        energyText.text = money.ToString();
        energyRatio.fillAmount = money * 1.0f / startMoney;

        gpaText.text = "GPA:" + (health * 1.0f / startHealth * 4.0f).ToString("f2");
        healthRatio.fillAmount = health * 1.0f / startHealth;
    }

    public float GetGPA()
    {
        return health * 1.0f / startHealth * 4.0f;
    }

    public void MissAnEnemy(float healthRemain)
    {
        health -= healthRemain * Mathf.Pow(GetGPA() / 4,2);
        AddEnemyMiss();
    }

    public void AddEnemyKill()
    {
        enemyKilled++;
    }
    public int GetEnemyKill()
    {
        return enemyKilled;
    }
    public void AddEnemyMiss()
    {
        enemyMiss++;
    }

    public int GetEnemyMiss()
    {
        return enemyMiss;
    }

    public void SetPlayTime(float time)
    {
        playTime = time;
    }
    public float GetPlayTime()
    {
        return playTime / 1440;
    }

    public void CostEnergy(int spent)
    {
        money -= spent;
        energySpent += spent;
    }

    public int GetEnergySpent()
    {
        return energySpent;
    }

    public float TowerAttackAmpl()
    {
        return 1 + (money / startMoney) * 0.5f;
    }
}
