using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using TMPro;
using Michsky.UI.Zone;
using System.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;

public class login_confirm : MonoBehaviour
{
    public GameObject menu_manager;
    public GameObject bgm;
    public GameObject background;
    public GameObject prompt_msg;
    public GameObject splash_screen;
    public GameObject login_panel;
    public GameObject main_menu;
    public GameObject play;
    public GameObject play_title;
    public GameObject prompt_info;
    private string name;
    public GameObject name_input;
    private string password;
    public GameObject password_input;
    private string signupName;
    public GameObject signupName_input;
    private string signupPassword;
    public GameObject signupPassword_input;
    private string email;
    public GameObject email_input;
    private bool agree;
    public GameObject agree_switch;

    private bool islogin;
    private bool isprompt;
    private float time = 0;

    private Dictionary<int, List<string>> temp;
    private int achivements;

    private static string regulation = "^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+(\\.[a-zA-Z0-9-]+)*\\.[a-zA-Z0-9]{2,6}$";
    private Regex r;
    // Start is called before the first frame update
    void Start()
    {
        name = "myy";
        password = "123456";
        r = new Regex(regulation);
    }

    // Update is called once per frame
    void Update()
    {
        name = name_input.GetComponent<TMP_InputField>().text;
        password = password_input.GetComponent<TMP_InputField>().text;
        signupName = signupName_input.GetComponent<TMP_InputField>().text;
        signupPassword = signupPassword_input.GetComponent<TMP_InputField>().text;
        email = email_input.GetComponent<TMP_InputField>().text;
        agree = agree_switch.GetComponent<SwitchManager>().isOn;

        time += Time.deltaTime;
        if (islogin)
        {
            if (time > 3)
            {
                //splash_screen.GetComponent<Animator>().Play("Login Screen Out");
                // menu_manager.GetComponent<BlurManager>().BlurOutAnim();
                bgm.GetComponent<music_ctrl>().changeMusicTo(2);
                bgm.GetComponent<AudioSource>().Play();
                //splash_screen.GetComponent<AudioSource>().Play();
                splash_screen.GetComponent<Animator>().Play("Out");
                main_menu.SetActive(true);
                main_menu.GetComponent<Animator>().Play("Start");
                //SceneManager.GetSceneByName("open_scene").GetRootGameObjects()[1].transform.GetChild(2).gameObject.SetActive(false);
                background.GetComponent<remove_filter>().remove_filter_();
                play.GetComponent<Animator>().Play("Wait");
                play.GetComponent<Animator>().Play("Panel In");
                play_title.GetComponent<Animator>().Play("Hover to Pressed");
                msg_ctrl.instance.update_msglist();
                side_window_ctrl.start_display = true;
                islogin = false;
            }

        }


        if (isprompt)
        {
            if (time > 2)
            {
                prompt_msg.GetComponent<Animator>().Play("Panel Out");
                isprompt = false;
            }
        }
    }

    private void update_userinfo()
    {
        Func<string> funcUpdate = () =>
        {
            temp = ServerSql.Calculate(this.name);
            achivements = ServerSql.achievementsnumber(name);
            userInfo.instance.nickname = name;
            userInfo.instance.password = password;
            userInfo.instance.email = "11812106@gmail.com";
            userInfo.instance.totalkill = int.Parse(temp[0][1]);
            userInfo.instance.totalmiss = int.Parse(temp[0][2]);
            userInfo.instance.totalplayhour = float.Parse(temp[0][3]);
            userInfo.instance.totalachivements = 14;
            userInfo.instance.achivements = this.achivements;
            userInfo.instance.totalenergyspend = int.Parse(temp[0][4]);
            userInfo.instance.levelnumber = 3;
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
                    return funcUpdate();
                });
            })();
            await task;
        })();
    }

    private void btnLogin_Click()
    {
        Func<bool> funcLogin = () => { return ServerSql.login(this.name, this.password); };
        Action actionSucces = () => {
            time = 0;
            islogin = true;
            //splash_screen.GetComponent<AudioSource>().Play();
            splash_screen.GetComponent<Animator>().Play("Loading");
            bgm.GetComponent<music_ctrl>().stop();

            update_userinfo();
        };
        Action actionFail = () => {
            //splash_screen.GetComponent<Animator>().Play("Login Screen In");
            //bgm.GetComponent<AudioSource>().Play();
            prompt("NICKNAME OR PASSWORD GOES WRONG!");
        };
        new Action(async () =>
        {
            Task<bool> task = new Func<Task<bool>>(async () =>
            {
                return await Task.Run(() =>
                {
                    return funcLogin();
                });
            })();

            if (await task)
            {
                actionSucces();
            }
            else
            {
                actionFail();
            }
        })();
    }

    public void login()
    {
        if (confirm_input_correct("login"))
        {
            //splash_screen.GetComponent<Animator>().Play("Loading");
            splash_screen.GetComponent<AudioSource>().Play();
            //bgm.GetComponent<music_ctrl>().stop();
            btnLogin_Click();
        }
  
    }

/*    public void login()
    {
        splash_screen.GetComponent<Animator>().Play("Loading");
        splash_screen.GetComponent<AudioSource>().Play();
        bgm.GetComponent<music_ctrl>().stop();

        if (confirm_input_correct("login"))
        {
       
            if (!ServerSql.login(name,password))
            {  
                splash_screen.GetComponent<Animator>().Play("Login Screen In");
                bgm.GetComponent<AudioSource>().Play();
                prompt("NICKNAME OR PASSWORD GOES WRONG!");
            }
            else
            {
                time = 0;
                islogin = true;
                //splash_screen.GetComponent<AudioSource>().Play();
                //splash_screen.GetComponent<Animator>().Play("Loading");
                //bgm.GetComponent<music_ctrl>().stop();
                Dictionary<int, List<string>> temp = ServerSql.Calculate(name);
                userInfo.instance.nickname = name;
                userInfo.instance.password = password;
                userInfo.instance.email = "11812106@gmail.com";
                userInfo.instance.totalkill = int.Parse(temp[0][1]);
                userInfo.instance.totalmiss = int.Parse(temp[0][2]);
                userInfo.instance.totalplayhour = float.Parse(temp[0][3]);
                userInfo.instance.totalachivements = 4;
                userInfo.instance.achivements = ServerSql.achievementsnumber(name);
                userInfo.instance.totalenergyspend = int.Parse(temp[0][4]);
                userInfo.instance.levelnumber = 3;
                userInfo.instance.gpa = float.Parse(temp[0][5]);
                userInfo.instance.update_msg();
                msg_ctrl.instance.update_msglist();
            }
        }
    }*/

    public void signup()
    {
        if (confirm_input_correct("sign up"))
        {
            if (!ServerSql.register(signupName, signupPassword, email, Time.time.ToString()))
            {
                prompt("NICKNAME ALREADY EXISTS!");
            }
            else
            {
                splash_screen.GetComponent<AudioSource>().Play();
                login_panel.GetComponent<MainPanelManager>().PanelAnim(0);
            }
        }
    }

    public void prompt(string info)
    {
        prompt_info.GetComponent<TMP_Text>().text = info;
        isprompt = true;
        prompt_msg.GetComponent<Animator>().Play("Panel In");
        time = 0;
    }

    private bool confirm_input_correct(string type)
    {
        if (type == "login")
        {
            //检测NICKNAME, PASSWORD
            if (name == "" || password == "")
            {
                prompt("PLEASE INPUT ALL NEEDED INFORMATION!");
                return false;
            }

        }
        else
        {
            if (!r.Match(email).Success)
            {
                prompt("PLEASE INPUT CORRECT EMAIL!");
                return false;
            }
            if (!agree)
            {
                prompt("PLEASE AGREE THE REGULATION!");
                return false;
            }
            if (signupName == "" || signupPassword == "" || email == "")
            {
                
                prompt("PLEASE INPUT ALL NEEDED INFORMATION!");
                return false;
            }
        }
        return true;
    }
}
