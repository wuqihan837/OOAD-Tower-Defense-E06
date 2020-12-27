using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userInfo : MonoBehaviour
{
    public string nickname;
    public string password;
    public string email;
    public int totalkill;
    public int totalmiss;
    public float totalplayhour;
    public int totalachivements;
    public int achivements;
    public int totalenergyspend;
    public int levelnumber;
    public float gpa;
    public List<msg_info> msglist = new List<msg_info>();
    public int level = 0;
    public List<List<string>> new_achivement_list = new List<List<string>>();
    public int msc_index = 2;

    public int game_music_index = 6;
    public float game_voice;

    public static userInfo instance;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        //DontDestroyOnLoad(this);
    }
    void Start()
    {

    }

    public static userInfo getInstance()
    {
        return instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public class msg_info
    {
        public string msg;
        public string time;
        public msg_info(string t, string m)
        {
            time = t;
            msg = m;
        }
    }

    public void update_msg()
    {
        //Debug
        Dictionary<int, List<string>> msg_dic = ServerSql.showallemails(nickname);
        msglist.Clear();
        foreach(int key in msg_dic.Keys)
        {
            msglist.Add(new msg_info(msg_dic[key][0], msg_dic[key][1]));
        }
    }
}
