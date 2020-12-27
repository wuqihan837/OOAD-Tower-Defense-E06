using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class circlebar_ctrl : MonoBehaviour
{
    //声明imgFillAmount
    private Image imgFillAmount;
    private float gpa = 3.22f;
    public GameObject ratio;
    private TMP_Text ratioContent;

    //是否开始读条
    bool isPlay = false;
    //计时用：初始时间
    float timer = 0;
    //计时用：读条所用的全部时间
    float duration = 1;


    void Start()
    {
        //获取到刚刚修改Image Type为Filled的Image
        imgFillAmount = GetComponent<Image>();
        imgFillAmount.fillAmount = Mathf.Lerp(0, 1, 0);
        ratioContent = ratio.GetComponent<TMP_Text>();
        ratioContent.text = "0.00";
        isPlay = true;
        gpa = PlayerStats.GetInstance().GetGPA();
    }

    void Update()
    {
        //判断是否开始读条
        if (isPlay)
        {
            //使timer根据时间增长
            timer += Time.deltaTime/5;
            //修改FillAmount的值
            //（使当前时间占全部时间的比例为FillAmount中0到1之间的值）
            imgFillAmount.fillAmount = Mathf.Lerp(0, gpa/4, timer / duration);
            ratioContent.text = (gpa/4 * 4*timer / duration).ToString("f2");

            //计时器
            if (timer >= duration)
            {
                //停止读条
                isPlay = false;
                //将timer还原为0，为下一次计时做准备
                timer = 0;
            }
        }

        //鼠标点击
        /*if (Input.GetMouseButtonDown(0))
        {
            //开始读条
            isPlay = true;
        }*/
    }
}
