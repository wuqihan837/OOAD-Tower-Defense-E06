using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class settlement_scrollbar_ctrl : MonoBehaviour
{
    //声明imgFillAmount
    private Image imgFillAmount;
    public string value_name;
    private int number;
    private int number_limit;
    public GameObject ratio;
    private TMP_Text ratioContent;
    //是否开始读条
    bool isPlay = false;
    //计时用：初始时间
    float timer = 0;
    //计时用：读条所用的全部时间
    float duration = 1;

    public int index = 0;


    void Start()
    {
        //获取到刚刚修改Image Type为Filled的Image
        imgFillAmount = GetComponent<Image>();
        imgFillAmount.fillAmount = Mathf.Lerp(0, 1, 0);
        ratioContent = ratio.GetComponent<TMP_Text>();
        ratioContent.text = "0/100";
        number_limit = 100;
        if (index == 0)
        {
            number = PlayerStats.GetInstance().GetEnemyKill();
            number_limit = PlayerStats.GetInstance().GetEnemyKill()+ PlayerStats.GetInstance().GetEnemyMiss();
        }
        else if(index == 1)
        {
            number = PlayerStats.GetInstance().GetEnemyMiss();
            number_limit = PlayerStats.GetInstance().GetEnemyKill() + PlayerStats.GetInstance().GetEnemyMiss();
        }
        else if (index == 2)
        {
            number = PlayerStats.GetInstance().GetEnergySpent();
            number_limit = 2000;
        }
        else
        {
            number = (int)(PlayerStats.GetInstance().GetPlayTime()*300);
            number_limit = 500;
        }
        

        isPlay = true;
    }

    void Update()
    {
        //判断是否开始读条
        if (isPlay)
        {
            //使timer根据时间增长
            timer += Time.deltaTime/3;
            //修改FillAmount的值
            //（使当前时间占全部时间的比例为FillAmount中0到1之间的值）
            imgFillAmount.fillAmount = Mathf.Lerp(0, ((float)number/(float)number_limit), timer / duration);
            Debug.Log(imgFillAmount.fillAmount);
            ratioContent.text = ((int)(number* timer / duration)).ToString() + "/" + number_limit.ToString();

            //计时器
            if (timer >= duration)
            {
                //停止读条
                isPlay = false;
                //将timer还原为0，为下一次计时做准备
                timer = 0;
            }
        }
    }
    }
