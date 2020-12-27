using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class msg_ctrl : MonoBehaviour
{
    public GameObject messageList_parent;
    public GameObject messageList_prefab;
    public static msg_ctrl instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void update_msglist()
    {
        int count = messageList_parent.transform.childCount;
        for(int i = 0; i < count; i++)
        {
            Destroy(messageList_parent.transform.GetChild(i).gameObject);
        }

        List<GameObject> msgList_all = new List<GameObject>();

        for(int i = 0; i < userInfo.instance.msglist.Count; i++)
        {
            GameObject temp = GameObject.Instantiate(messageList_prefab) as GameObject;
            temp.transform.GetChild(1).GetChild(1).gameObject.GetComponent<TMP_Text>().text = userInfo.instance.msglist[i].msg;
            temp.transform.GetChild(1).GetChild(2).gameObject.GetComponent<TMP_Text>().text = userInfo.instance.msglist[i].time;
            temp.transform.SetParent(messageList_parent.transform);
            temp.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
