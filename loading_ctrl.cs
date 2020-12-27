using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class loading_ctrl : MonoBehaviour
{
    public List<GameObject> levels = new List<GameObject>();
    public List<GameObject> objects = new List<GameObject>();
    private GameObject nextlevel;
    private GameObject gameObject;
    public AsyncOperation ass;

    DateTime oldtime;
    DateTime newtime;
    public int loadingtime;

    private bool starter = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (userInfo.instance.level == 1)
        {
            nextlevel = levels[0];
            gameObject = objects[0];
        }else if(userInfo.instance.level == 2)
        {
            nextlevel = levels[1];
            gameObject = objects[1];
        }
        else
        {
            nextlevel = levels[2];
            gameObject = objects[2];
        }
    }

    void Start()
    {
        gameObject.SetActive(true);
        nextlevel.SetActive(true);
        if (SceneManager.sceneCount <= 2)
        {
            oldtime = DateTime.Now;
            ass = SceneManager.LoadSceneAsync("Scene" + userInfo.instance.level.ToString(), LoadSceneMode.Additive);
            starter = true;
            ass.allowSceneActivation = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        newtime = DateTime.Now;
        if (!starter)
        {
            if (SceneManager.sceneCount <= 2)
            {
                oldtime = DateTime.Now;
                ass = SceneManager.LoadSceneAsync("Scene" + userInfo.instance.level.ToString(), LoadSceneMode.Additive);
                starter = true;
                ass.allowSceneActivation = false;
            }
        }
        else
        {
            if (ass.progress > 0.899 && (newtime - oldtime).TotalMilliseconds > loadingtime)
            {
                nextlevel.transform.GetChild(3).gameObject.SetActive(false);
                nextlevel.transform.GetChild(2).gameObject.SetActive(true);
            }
        }

    }

    public void enter()
    {
        ass.allowSceneActivation = true;
        SceneManager.UnloadSceneAsync("loading");
        SceneManager.GetSceneByName("open_scene").GetRootGameObjects()[2].GetComponent<music_ctrl>().ingame = true;
        Debug.Log(SceneManager.GetSceneByBuildIndex(0).name);
        Debug.Log(SceneManager.GetSceneByName("open_scene").name);
        SceneManager.GetSceneByName("open_scene").GetRootGameObjects()[2].GetComponent<music_ctrl>().changeMusicTo(userInfo.instance.game_music_index);
    }

}
