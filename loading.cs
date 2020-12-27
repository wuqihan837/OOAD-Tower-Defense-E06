using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class loading : MonoBehaviour
{
    DateTime oldtime;
    DateTime newtime;
    public int loadingtime;
    public GameObject loading_;
    public GameObject prompt;
    // Start is called before the first frame update
    void Start()
    {
        oldtime = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        newtime = DateTime.Now;
        if ((newtime - oldtime).TotalMilliseconds > loadingtime)
        {
            loading_.SetActive(false);
            prompt.SetActive(true);
        }
       
    }
}
