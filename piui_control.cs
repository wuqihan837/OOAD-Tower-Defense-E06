using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piui_control : MonoBehaviour
{
    public static piui_control instance;

    [SerializeField]
    PiUIManager piUi;
    private bool menuOpened;
    private PiUI normalMenu;
    // Start is called before the first frame update


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        normalMenu = piUi.GetPiUIOf("general");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static piui_control getpiuiController()
    {
        return instance;
    }

    //public void SetpiuiActive(string id, Vector2 pos = default(Vector2))
    //{
    //    Debug.Log(id);
    //    Debug.Log(pos);
    //    piUi.ChangeMenuState(id, pos);
    //}


    //public void click_button()
    //{
    //    piUi.ChangeMenuState("Pi Menu", new Vector2(Screen.width / 2f, Screen.height / 2f));
    //    Debug.Log("click !!!!");
    //}

    //public void click_button2()
    //{
    //    piUi.ChangeMenuState("Pi Menu 2", new Vector2(Screen.width / 2f, Screen.height / 2f));
    //    Debug.Log("click 2!!!!");
    //}

    //public void click_exit()
    //{
    //    piUi.ChangeMenuState("Pi Menu", new Vector2(Screen.width * 2f, Screen.height * 2f));
    //}
}
