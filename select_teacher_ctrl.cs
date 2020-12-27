using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class select_teacher_ctrl : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public Transform constant;
    public Transform center;
    Transform _select;
    public List<Transform> objs;
    Coroutine moveToCenter;

    public List<GameObject> confirm_list = new List<GameObject>();
    private List<bool> selected_or_Not = new List<bool>();
    public List<int> credits = new List<int>();

    public GameObject game_start;

    public GameObject submit;

    private int total_credits = 25;
    private int total = 0;
    private int next_confirm_index = 0;

    public GameObject remain_credits_text;

    public GameObject gameStatusObj;

    public GameObject map;
    public GameObject name;

    public Transform select { get{ return _select; } }
    public int objCount { get { return objs.Count; } }
    public int selcetIndex { get { return objs.IndexOf(_select); } }
    Transform leftTrans;
    Transform rightTrans;
    void Awake()
    {
        for(int i = 0; i < objs.Count; i++)
        {
            selected_or_Not.Add(false);
        }
        SetRightLeft();
        FindSelectObj();
        ScaleTheSize();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void select_center()
    {
        //Debug.Log("wtf");
        int center_index = 0;
        for(int i = 0; i < objs.Count; i++)
        {
            if (objs[i] == _select)
            {
                center_index = i;
                break;
            }
        }
        //Debug.Log("----------------------------------------------------");
        //Debug.Log(center_index);
        if (!selected_or_Not[center_index] && total + credits[center_index] < total_credits)
        {
            selected_or_Not[center_index] = true;
            //Debug.Log(total);
            //Debug.Log(credits[center_index]);
            confirm_list[next_confirm_index].GetComponent<Image>().sprite = _select.GetChild(1).transform.GetChild(0).GetComponent<Image>().sprite;
            next_confirm_index++;
            total += credits[center_index];
            _select.parent.transform.GetChild(center_index).GetComponent<Button>().interactable = false;
            remain_credits_text.GetComponent<TMP_Text>().text = (total_credits - total).ToString();
        }
        //Debug.Log("------------------------------------------------------end");
        for(int i = 0; i < selected_or_Not.Count; i++)
        {
            //Debug.Log(selected_or_Not[i]);
        }
    }

    public void remove_all()
    {
        for(int i = 0; i < objs.Count; i++)
        {
            selected_or_Not[i] = false;
            confirm_list[i].GetComponent<Image>().sprite = null;
            objs[i].parent.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
        total = 0;
        next_confirm_index = 0;
        remain_credits_text.GetComponent<TMP_Text>().text = 25.ToString();
    }

    public void play_game()
    {
        if (next_confirm_index < 2)
        {

        }
        else
        {
            //this.transform.parent.transform.parent.GetComponent<Animator>().Play("Panel Out");
            submit.GetComponent<SelectSuperTower>().Submit(selected_or_Not);
            game_start.SetActive(true);
            gameStatusObj.SetActive(false);
            map.SetActive(true);
            name.GetComponent<TMP_Text>().text = userInfo.instance.nickname;
        }

    }

    public void move_constant(int dis)
    {
        constant.Translate(dis * 300 * Vector3.right);
        FindSelectObj();
        ScaleTheSize();
        StartCoroutine(MoveToSelect());
    }

    void SetRightLeft()
    {
        float right = -9999;
        float left = 9999;
        Transform rightObj = null;
        Transform leftObj = null;
        for(int i = 0; i < objs.Count; i++)
        {
            if (objs[i].position.x > right)
            {
                rightObj = objs[i];
                right = objs[i].transform.position.x;
            }
            if (objs[i].position.x < left)
            {
                leftObj = objs[i];
                left = objs[i].transform.position.x;
            }
        }
        leftTrans = leftObj;
        rightTrans = rightObj;
    }

    Transform FindSelectObj()
    {
        Transform nearly = null;
        float minDis = 9999;
        for(int i = 0; i < objs.Count; i++)
        {
            float dis = Vector2.Distance(objs[i].position, center.position);
            if(dis < minDis)
            {
                nearly = objs[i];
                minDis = dis;
            }
        }
        _select = nearly;
        return nearly;
    }

    IEnumerator MoveToSelect()
    {
        Vector3 centerPos = center.position;
        Vector3 targetPos = _select.position;
        float distance = Vector3.Distance(centerPos, targetPos);
        while(distance > 0.03f)
        {
            centerPos = center.position;
            targetPos = _select.position;
            Vector3 pos = Vector3.Lerp(targetPos, centerPos, 0.1f);
            Vector3 contantMove = pos - _select.position;
            constant.Translate(contantMove);
            distance = Vector3.Distance(centerPos, _select.position);
            ScaleTheSize();
            yield return null;
        }
        moveToCenter = null;
    }

    void ScaleTheSize()
    {
        foreach(Transform trans in objs)
        {
            float distance = Vector2.Distance(trans.position, center.position);
            float tlerp = distance / (Screen.width / 2);
            float size = Mathf.Lerp(1f, 0.65f, tlerp);
            trans.localScale = new Vector3(1, 1, 1) * size;
            //Debug.Log(trans.localScale);
            //Debug.Log(trans.position);
            //Debug.Log(trans.localPosition);
        }
    }

    public void OnDrag(PointerEventData data)
    {
        Vector3 delta = data.delta;
        float xMove = delta.x;

        if (leftTrans.position.x + xMove >= (center.position.x + Screen.width / 4))
            xMove = (center.position.x + Screen.width / 4) - leftTrans.position.x;
        if (rightTrans.position.x + xMove <= (center.position.x - Screen.width / 4))
            xMove = (center.position.x - Screen.width / 4) - rightTrans.position.x;

        constant.Translate(xMove * Vector3.right);
        FindSelectObj();
        ScaleTheSize();
        if (moveToCenter != null) StopCoroutine(moveToCenter);
    }

    public void OnEndDrag(PointerEventData data)
    {
        moveToCenter = StartCoroutine(MoveToSelect());
    }


}
