using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class update_info : MonoBehaviour
{
    public GameObject name_top;
    public GameObject kill;
    public GameObject miss;
    public GameObject playhour;
    public GameObject levelnumber;
    public GameObject energyspend;
    public GameObject achievements;
    public GameObject gpa_text;
    public GameObject gpa_bar;

    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        name_top.GetComponent<TMP_Text>().text = userInfo.instance.nickname;
        kill.GetComponent<TMP_Text>().text = userInfo.instance.totalkill.ToString();
        miss.GetComponent<TMP_Text>().text = userInfo.instance.totalmiss.ToString();
        playhour.GetComponent<TMP_Text>().text = userInfo.instance.totalplayhour.ToString();
        levelnumber.GetComponent<TMP_Text>().text = userInfo.instance.levelnumber.ToString();
        energyspend.GetComponent<TMP_Text>().text = userInfo.instance.totalenergyspend.ToString();
        achievements.GetComponent<TMP_Text>().text = userInfo.instance.achivements.ToString()+"/"+userInfo.instance.totalachivements.ToString();
        gpa_text.GetComponent<TMP_Text>().text = userInfo.instance.gpa.ToString("f2");
        gpa_bar.GetComponent<Image>().fillAmount = userInfo.instance.gpa / 4.0f;

        /*if (count < 4)
        {
            side_window_ctrl.enqueue(Time.time.ToString(), "dd");
            count++;
        }*/

        if (userInfo.instance.new_achivement_list.Count > 0)
        {
            for(int i = 0; i < userInfo.instance.new_achivement_list.Count; i++)
            {
                for(int j=0;j < userInfo.instance.new_achivement_list[i].Count; j++)
                {
                    side_window_ctrl.enqueue(userInfo.instance.new_achivement_list[i][j], "dd");
                    Debug.Log(userInfo.instance.new_achivement_list[i][j]);
                }
            }
            userInfo.instance.new_achivement_list.Clear();
            msg_ctrl.instance.update_msglist();
        }
        
    }
}
