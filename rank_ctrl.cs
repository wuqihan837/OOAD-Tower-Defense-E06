using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Threading.Tasks;

public class rank_ctrl : MonoBehaviour
{
    public List<GameObject> levels = new List<GameObject>();
    public GameObject loading;
    public GameObject rank;

    private bool haskey = false;

    Dictionary<int, List<string>> level1;
    Dictionary<int, List<string>> self1;
    Dictionary<int, List<string>> level2;
    Dictionary<int, List<string>> self2;
    Dictionary<int, List<string>> level3;
    Dictionary<int, List<string>> self3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (haskey)
        {
            for (int i = 0; i < 10; i++)
            {
                if (i < level1.Count)
                {
                    levels[0].transform.GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(1).gameObject.GetComponent<TMP_Text>().text = level1[i][0];
                    levels[0].transform.GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(2).gameObject.GetComponent<TMP_Text>().text = level1[i][1];
                }
                else
                {
                    levels[0].transform.GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(1).gameObject.GetComponent<TMP_Text>().text = "---";
                    levels[0].transform.GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(2).gameObject.GetComponent<TMP_Text>().text = "0.00";
                }
            }
            levels[0].transform.GetChild(4).GetChild(0).gameObject.GetComponent<TMP_Text>().text = self1[0][0];
            levels[0].transform.GetChild(4).GetChild(1).gameObject.GetComponent<TMP_Text>().text = userInfo.instance.nickname;
            levels[0].transform.GetChild(4).GetChild(2).gameObject.GetComponent<TMP_Text>().text = self1[0][1];
            for (int i = 0; i < 10; i++)
            {
                if (i < level2.Count)
                {
                    levels[1].transform.GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(1).gameObject.GetComponent<TMP_Text>().text = level2[i][0];
                    levels[1].transform.GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(2).gameObject.GetComponent<TMP_Text>().text = level2[i][1];
                }
                else
                {
                    levels[1].transform.GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(1).gameObject.GetComponent<TMP_Text>().text = "---";
                    levels[1].transform.GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(2).gameObject.GetComponent<TMP_Text>().text = "0.00";
                }
            }
            levels[1].transform.GetChild(4).GetChild(0).gameObject.GetComponent<TMP_Text>().text = self2[0][0];
            levels[1].transform.GetChild(4).GetChild(1).gameObject.GetComponent<TMP_Text>().text = userInfo.instance.nickname;
            levels[1].transform.GetChild(4).GetChild(2).gameObject.GetComponent<TMP_Text>().text = self2[0][1];
            for (int i = 0; i < 10; i++)
            {
                if (i < level3.Count)
                {
                    levels[2].transform.GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(1).gameObject.GetComponent<TMP_Text>().text = level3[i][0];
                    levels[2].transform.GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(2).gameObject.GetComponent<TMP_Text>().text = level3[i][1];
                }
                else
                {
                    levels[2].transform.GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(1).gameObject.GetComponent<TMP_Text>().text = "---";
                    levels[2].transform.GetChild(3).GetChild(0).GetChild(0).GetChild(i).GetChild(2).gameObject.GetComponent<TMP_Text>().text = "0.00";
                }
            }
            levels[2].transform.GetChild(4).GetChild(0).gameObject.GetComponent<TMP_Text>().text = self3[0][0];
            levels[2].transform.GetChild(4).GetChild(1).gameObject.GetComponent<TMP_Text>().text = userInfo.instance.nickname;
            levels[2].transform.GetChild(4).GetChild(2).gameObject.GetComponent<TMP_Text>().text = self3[0][1];

            loading.SetActive(false);
            //rank.GetComponent<Animator>().Play("Panel In");

            haskey = false;
        }
    }

    private void update_rank_async()
    {
        Func<string> funcRank = () =>
        {
            level1 = ServerSql.Rank1();
            self1 = ServerSql.myRank1(userInfo.instance.nickname);
            level2 = ServerSql.Rank2();
            self2 = ServerSql.myRank2(userInfo.instance.nickname);
            level3 = ServerSql.Rank3();
            self3 = ServerSql.myRank3(userInfo.instance.nickname);
            haskey = true;
        
            return "success";
        };

        new Action(async () =>
        {
            Task<string> task = new Func<Task<string>>(async () =>
            {
                return await Task.Run(() =>
                {
                    return funcRank();
                });
            })();
            await task;
        })();
    }

    public void update_rank()
    {
        update_rank_async();
    }
}
