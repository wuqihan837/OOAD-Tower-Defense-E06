using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    // 不需要拖进来
    public piui_control piuiControl;

    [SerializeField]
    PiUIManager piUIManager;
    private PiUI aliveUI;

    private Dictionary<UIType, string> buildGeneralPrice = new Dictionary<UIType, string>();
    private Dictionary<UIType, string> buildBuffPrice = new Dictionary<UIType, string>();

    private float menu_change_interval = 0.5f;
    private float menu_change_last_time;


    [System.Serializable]
    public struct TextCollection
    {
        public UIType id;
        public PiUI.PiData text;
    }

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Start()
    {
        piuiControl = piui_control.getpiuiController();

        buildGeneralPrice = GeneralShop.GetInstance().GetGeneralPrice();
        buildBuffPrice = BuffShop.GetInstance().GetBuffPrice();

        menu_change_last_time = Time.time;
    }

    public static UIController GetUIController()
    {
        return instance;
    }

    private void Update()
    {
        if (aliveUI == null)
            return;

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            SetCanvasInactive();
        }
    }
    
    public void SetCanvasActive(Vector2 pos)
    {
        //Debug.Log(menu_change_last_time);
        //if(Time.time - menu_change_last_time < menu_change_interval)
        //{
        //    return;
        //}
        if (aliveUI == null)
            return;
        aliveUI.OpenMenu(pos);

        menu_change_last_time = Time.time;
    }

    public void SetCanvasInactive()
    {
        //Debug.Log(menu_change_last_time);
        //if (Time.time - menu_change_last_time < menu_change_interval)
        //{
        //    return;
        //}

        if (aliveUI == null)
            return;
        aliveUI.CloseMenu();

        menu_change_last_time = Time.time;
    }

    public void SetAliveCanvas(string id)
    {
        if(id != null || id != "")
        {
            aliveUI = piUIManager.GetPiUIOf(id);

            switch(id)
            {
                case "general":
                    ShowMoneyForGeneral();
                    break;
                case "buff":
                    ShowMoenyForBTower();
                    break;
            }
        }
    }

    public void ShowMoneyForModify(string destroyMoney, string upgradeMoney)
    {
        UIType id_des = UIType.destroy;
        UIType id_up = UIType.upgrade;
        SetMoneyText(id_des,destroyMoney);
        SetMoneyText(id_up, upgradeMoney);
        piUIManager.UpdatePiMenu("modify");
    }

    public void ShowMoneyForGeneral()
    {
        UIType id_st = UIType.standard;
        UIType id_ra = UIType.range;
        UIType id_la = UIType.laser;
        UIType id_sf = UIType.sleep;

        SetTextInDic(id_st, buildGeneralPrice);
        SetTextInDic(id_ra, buildGeneralPrice);
        SetTextInDic(id_sf, buildGeneralPrice);
        SetTextInDic(id_la, buildGeneralPrice);

        piUIManager.UpdatePiMenu("general");
    }

    public void ShowMoenyForBTower()
    {
        UIType id_at = UIType.addAttack;
        UIType id_ar = UIType.addRange;
        UIType id_re = UIType.slowSpeed;
        UIType id_po = UIType.poison;

        SetTextInDic(id_at, buildBuffPrice);
        SetTextInDic(id_ar, buildBuffPrice);
        SetTextInDic(id_re, buildBuffPrice);
        SetTextInDic(id_po, buildBuffPrice);


        piUIManager.UpdatePiMenu("buff");
    }

    private void SetTextInDic(UIType id,Dictionary<UIType, string> dic)
    {
        string text;
        if (dic.TryGetValue(id, out text))
        {
            SetMoneyText(id, text);
        }
    }

    private void SetMoneyText(UIType id, string money)
    {
        if(aliveUI == null)
        {
            return;
        }
        if (id == UIType.upgrade)
        {
            money += "UPGRADE " + money;
            aliveUI.SetSliceLabel(money, id);
        }
        else if (id == UIType.destroy)
        {
            money = "DESTROPY " + money;
            aliveUI.SetSliceLabel(money, id);
        }
        else { }

    }
}
