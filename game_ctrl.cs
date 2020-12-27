using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_ctrl : MonoBehaviour
{
    public GameObject start_panel;
    public GameObject finish_panel;
    public GameObject settlement_panel;
    public GameObject map;
    private Animator modelwindow;
    //计时用：初始时间
    float timer = 0;
    //计时用：读条所用的全部时间
    float duration = 1;

    int state = 0;
    private static bool fin=false;

    private float startTime;

    void Start()
    {
        modelwindow = start_panel.GetComponent<Animator>();
        modelwindow.Play("Panel In");

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0)
        {
            timer += Time.deltaTime/2;
            if (timer > duration)
            {
                modelwindow.Play("Panel Out");
                state = 1;
                //EnemySpawner_AI.GetInstance().StartSpawning();
                EnemySpawner.GetInstance().StartSpawning();
            }
        }else if(state == 1)
        {
            if (fin)
            {
                PlayerStats.GetInstance().SetPlayTime(Time.time - startTime);
                state = 2;
                fin = false;
                timer = 0;
                modelwindow = finish_panel.GetComponent<Animator>();
                modelwindow.Play("Panel In");
            }
        }
        else if (state == 2)
        {
            timer += Time.deltaTime / 2;
            if (timer > duration)
            {
                map.SetActive(false);
                settlement_panel.SetActive(true);
                settlement_panel.GetComponent<Animator>().Play("Panel In");
                state = 3;
            }
        }
        

    }

    public static void setfin()
    {
        fin = true;
    }
}
