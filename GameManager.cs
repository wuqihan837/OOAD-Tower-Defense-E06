using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int currentLevel = 0;
    public string[] scenes;
    private static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    public static GameManager GetInstance()
    {
        return instance;
    }


    public void NextLevel()
    {
        currentLevel++;
        SceneManager.LoadScene(scenes[currentLevel]);
    }
}
