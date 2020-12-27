using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Michsky.UI.Zone;
using System;
using System.Threading.Tasks;

public class jump_back : MonoBehaviour
{
    // Start is called before the first frame update\
    private Dictionary<int, List<string>> temp;
    private int achivements;

    private void add_game_info(int level)
    {
        Func<string> funcGame = () =>
        {
            userInfo.instance.new_achivement_list.Add(ServerSql.addeverygame(userInfo.instance.nickname, level, PlayerStats.GetInstance().GetEnemyKill(), PlayerStats.GetInstance().GetEnemyMiss(), PlayerStats.GetInstance().GetPlayTime(), PlayerStats.GetInstance().GetEnergySpent(), PlayerStats.GetInstance().GetGPA())[0]);
            Debug.Log("adding successful!!!");
            temp = ServerSql.Calculate(userInfo.instance.nickname);
            achivements = ServerSql.achievementsnumber(userInfo.instance.nickname);
            userInfo.instance.totalkill = int.Parse(temp[0][1]);
            userInfo.instance.totalmiss = int.Parse(temp[0][2]);
            userInfo.instance.totalplayhour = float.Parse(temp[0][3]);
            userInfo.instance.achivements = this.achivements;
            userInfo.instance.totalenergyspend = int.Parse(temp[0][4]);
            userInfo.instance.gpa = float.Parse(temp[0][5]);
            userInfo.instance.update_msg();
            return "success";
        };

        new Action(async () =>
        {
            Task<string> task = new Func<Task<string>>(async () =>
            {
                return await Task.Run(() =>
                {
                    return funcGame();
                });
            })();
            await task;
        })();
    }


    public void back_starter()
    {
        SceneManager.UnloadSceneAsync("Scene" + userInfo.instance.level.ToString());
        SceneManager.GetSceneByName("open_scene").GetRootGameObjects()[0].SetActive(true);
        SceneManager.GetSceneByName("open_scene").GetRootGameObjects()[1].SetActive(true);
        SceneManager.GetSceneByName("open_scene").GetRootGameObjects()[1].transform.GetChild(1).gameObject.GetComponent<BlurManager>().BlurInAnim();
        SceneManager.GetSceneByName("open_scene").GetRootGameObjects()[1].transform.GetChild(3).gameObject.SetActive(false);
        SceneManager.GetSceneByName("open_scene").GetRootGameObjects()[1].transform.GetChild(4).GetChild(0).gameObject.GetComponent<Animator>().Play("Panel In");
        SceneManager.GetSceneByName("open_scene").GetRootGameObjects()[1].transform.GetChild(4).GetChild(3).GetChild(0).gameObject.GetComponent<Animator>().Play("Hover to Pressed");
        SceneManager.GetSceneByName("open_scene").GetRootGameObjects()[2].GetComponent<music_ctrl>().ingame = false;
        SceneManager.GetSceneByName("open_scene").GetRootGameObjects()[2].GetComponent<music_ctrl>().changeMusicTo(userInfo.instance.msc_index);
        //userInfo.instance.new_achivement_list.Add(ServerSql.addeverygame("wqh", userInfo.instance.level, 20, 10, 0.1, 323, 3.23f)[0]);
        add_game_info(userInfo.instance.level);
    }

    public void back_starter_from_options()
    {
        SceneManager.UnloadSceneAsync("Scene" + userInfo.instance.level.ToString());
        SceneManager.GetSceneByName("open_scene").GetRootGameObjects()[0].SetActive(true);
        SceneManager.GetSceneByName("open_scene").GetRootGameObjects()[1].SetActive(true);
        SceneManager.GetSceneByName("open_scene").GetRootGameObjects()[1].transform.GetChild(3).gameObject.SetActive(false);
        SceneManager.GetSceneByName("open_scene").GetRootGameObjects()[1].transform.GetChild(4).GetChild(0).gameObject.GetComponent<Animator>().Play("Panel In");
        SceneManager.GetSceneByName("open_scene").GetRootGameObjects()[1].transform.GetChild(4).GetChild(3).GetChild(0).gameObject.GetComponent<Animator>().Play("Hover to Pressed");
        SceneManager.GetSceneByName("open_scene").GetRootGameObjects()[2].GetComponent<music_ctrl>().changeMusicTo(userInfo.instance.msc_index);
        //SceneManager.GetSceneByName("open_scene")
    }

    public void jump_to_scene(int i)
    {
        AsyncOperation ass = SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive);
        //ass.progress
        SceneManager.GetSceneByName("open_scene").GetRootGameObjects()[1].SetActive(false);
        Debug.Log(SceneManager.GetActiveScene());

    }

    public void loading_from_game(int i)
    {
        SceneManager.LoadScene("loading", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Scene" + userInfo.instance.level.ToString());
        GameObject[] objectarray = SceneManager.GetSceneByName("open_scene").GetRootGameObjects();
        for (int j = 3; j < objectarray.Length; j++)
        {
            Destroy(objectarray[j]);
        }
        //userInfo.instance.new_achivement_list.Add(ServerSql.addeverygame("wqh", userInfo.instance.level, 20, 10, 0.1, 323, 3.23f)[0]);
        add_game_info(userInfo.instance.level);
        userInfo.instance.level = i;
    }

    public void loading(int i)
    {
        userInfo.instance.level = i;
        SceneManager.LoadScene("loading", LoadSceneMode.Additive);
        SceneManager.GetSceneByName("open_scene").GetRootGameObjects()[0].SetActive(false);
        SceneManager.GetSceneByName("open_scene").GetRootGameObjects()[1].SetActive(false);

    }

    public void exit_game()
    {
        Application.Quit();
    }
}
