using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.UI.Zone;

public class side_window_ctrl : MonoBehaviour
{
    public List<GameObject> side_list = new List<GameObject>();
    public List<bool> useornot = new List<bool>();
    public List<float> time = new List<float>();
    private float duration = 7;
    private float wholetime = 0;
    private bool start_next = false;
    private int next_index = 0;
    private static Queue<side_anno> side_queue = new Queue<side_anno>();
    public static bool start_display = false;
    // Start is called before the first frame update
    private void Awake()
    {
        side_queue.Clear();
    }
    void Start()
    {
        for(int i = 0; i < side_list.Count; i++)
        {
            useornot[i] = false;
            time[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (start_display)
        {
            float t = Time.deltaTime;
            wholetime += t;
            if (wholetime > 1)
            {
                start_next = true;
                wholetime = 0;
            }
            for(int i = 0; i < side_list.Count; i++)
            {
                if (useornot[i])
                {
                    time[i] += t;
                }

            }
            if (!useornot[next_index]&&side_queue.Count!=0&&start_next)
            {
                side_anno temp = side_queue.Peek();
                side_queue.Dequeue();
                useornot[next_index] = true;
                side_list[next_index].transform.GetChild(4).GetComponent<TMP_Text>().text = temp.announcement;
                Debug.Log(next_index);
                side_list[next_index].GetComponent<Animator>().Play("Panel In");
                side_list[next_index].GetComponent<BlurManager>().BlurInAnim();
                msg_ctrl.instance.update_msglist();
                time[next_index] = 0;
                if (next_index == side_list.Count - 1)
                {
                    next_index = 0;
                }
                else
                {
                    next_index += 1;
                }
                start_next = false;
                wholetime = 0;
            }
            for(int i = 0; i < side_list.Count; i++)
            {
                if (time[i] > duration)
                {
                    side_list[i].GetComponent<Animator>().Play("Panel Out");
                    side_list[next_index].GetComponent<BlurManager>().BlurOutAnim();
                    useornot[i] = false;
                }
            }
        }

    }
    public static void enqueue(string ann, string pic_add)
    {
        side_queue.Enqueue(new side_anno(ann,pic_add));
    }

    public class side_anno
    {
        public string announcement;
        public string picture;
        public side_anno(string a, string p)
        {
            announcement = a;
            picture = p;
        }
    }
}
