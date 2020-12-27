using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settlement_scrollbar_mainctrl : MonoBehaviour
{
    [Header("SETTLEMENT LIST")]
    public List<GameObject> penalbar = new List<GameObject>();
    private int index = 0;

    private Animator anim;
    //计时用：初始时间
    float timer = 0;
    //计时用：读条所用的全部时间
    float duration = 1;
    // Start is called before the first frame update
    void Start()
    {
        penalbar[index].SetActive(true);
        anim = penalbar[index].GetComponent<Animator>();
        anim.Play("Panel In");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (index < penalbar.Count - 1)
        {
            if (timer > duration)
            {
                index++;
                penalbar[index].SetActive(true);
                anim = penalbar[index].GetComponent<Animator>();
                anim.Play("Panel In");
                timer = 0f;
            }
        }
    }
}
