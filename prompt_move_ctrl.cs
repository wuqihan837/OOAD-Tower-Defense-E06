using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class prompt_move_ctrl : MonoBehaviour
{
    public GameObject prompt;
    private GameObject prompt_text;
    public float moveTime;
    public float appearTime;
    public bool isActive = false;
    public bool isAppear = false;
    DateTime oldTime;
    DateTime newTime;
    public static prompt_move_ctrl instance;

    public Vector3 targetPosi;
    // Start is called before the first frame update
    private void Awake()
    {
        prompt_text = prompt.transform.GetChild(0).gameObject;
        targetPosi = new Vector3(1, 0, 0) * 1920;
        instance = this;
    }
    void Start()
    {
        prompt_text.transform.localPosition = targetPosi;
    }

    // Update is called once per frame
    void Update()
    {
        newTime = DateTime.Now;
        if (isActive)
        {
            if (!isAppear)
            {
                if ((newTime - oldTime).TotalMilliseconds < appearTime)
                {
                    Vector3 nowPosi = Vector3.Lerp(-1 * targetPosi, -0.7f * targetPosi, (float)(newTime - oldTime).TotalMilliseconds / appearTime);
                    prompt_text.transform.localPosition = nowPosi;
                    prompt.GetComponent<CanvasGroup>().alpha = (float)(newTime - oldTime).TotalMilliseconds / appearTime;
                }
                else
                {
                    isAppear = true;
                    prompt.GetComponent<CanvasGroup>().alpha = 1;
                }
            }
            else
            {
                if ((newTime - oldTime).TotalMilliseconds < moveTime)
                {
                    Vector3 nowPosi = Vector3.Lerp(-0.7f * targetPosi, targetPosi, (float)(newTime - oldTime).TotalMilliseconds / moveTime);
                    prompt_text.transform.localPosition = nowPosi;
                    //Debug.Log(prompt_text.transform.localPosition);
                }
                else
                {
                    isAppear = false;
                    isActive = false;
                    prompt_text.transform.localPosition = -1 * targetPosi;
                    prompt.GetComponent<CanvasGroup>().alpha = 0;
                }
            }

        }

    }

    public void prompt_appear(string text, int fontsize)
    {
        prompt_text.GetComponent<Text>().text = text;
        prompt_text.GetComponent<Text>().fontSize = fontsize;
        oldTime = DateTime.Now;
        isActive = true;
    }
}
