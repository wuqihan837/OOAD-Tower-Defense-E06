using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
[AddComponentMenu("UI/Extensions/Horizontal Scroll Snap")]
public class scrollsnap_ctrl : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    private Transform _screensContainer;

    private int _screens = 1;
    private int _startingScreen = 1;

    private bool _fastSwipeTimer = false;
    private int _fastSwipeCounter = 0;
    private int _fastSwipeTarget = 30;

    private System.Collections.Generic.List<Vector3> _positions;
    private ScrollRect _scroll_rect;
    private Vector3 _lerp_target;
    private bool _lerp;

    private int _containerSize;

    [Tooltip("The gameobject that contains toggles which suggest pagination. (optional)")]
    public GameObject Pagination;

    [Tooltip("Button to go to the next page. (optional)")]
    public GameObject NextButton;
    [Tooltip("Button to go to the previous page. (optional)")]
    public GameObject PrevButton;

    public Boolean UseFastSwipe = true;
    public int FastSwipeThreshold = 100;

    private bool _startDrag = true;
    private Vector3 _startPosition = new Vector3();
    private int _currentScreen;
    public float _speed = 10;

    private DateTime clock_time;
    public double changeTime = 5000;

    // Use this for initialization
    void Start()
    {
        clock_time = DateTime.Now;
        _scroll_rect = gameObject.GetComponent<ScrollRect>();
        _screensContainer = _scroll_rect.content;
        DistributePages();
        ChangeBulletsInfo(0);

        _screens = _screensContainer.childCount;
        Debug.Log(_scroll_rect.horizontalNormalizedPosition);

        _lerp = false;

        _positions = new System.Collections.Generic.List<Vector3>();

        if (_screens > 0)
        {
            for (int i = 0; i < _screens; ++i)
            {
                _scroll_rect.horizontalNormalizedPosition = (float)i / (float)(_screens-1 );
                Debug.Log(_scroll_rect.horizontalNormalizedPosition);

                _positions.Add(_screensContainer.localPosition);
            }
        }


        _scroll_rect.horizontalNormalizedPosition = (float)(_startingScreen-1) / (float)(_screens-1);

        _containerSize = (int)_screensContainer.gameObject.GetComponent<RectTransform>().offsetMax.x;

        Debug.Log(_scroll_rect.horizontalNormalizedPosition);

        if (NextButton)
            NextButton.GetComponent<Button>().onClick.AddListener(() => { NextScreen(); });

        if (PrevButton)
            PrevButton.GetComponent<Button>().onClick.AddListener(() => { PreviousScreen(); });
    }

    void Update()
    {
        if (_lerp)
        {
            _screensContainer.localPosition = Vector3.Lerp(_screensContainer.localPosition, _lerp_target, _speed * Time.deltaTime);
            if (Vector3.Distance(_screensContainer.localPosition, _lerp_target) < 0.001f)
            {
                _lerp = false;
            }

            //change the info bullets at the bottom of the screen. Just for visual effect
            if (Vector3.Distance(_screensContainer.localPosition, _lerp_target) < 10f)
            {
                ChangeBulletsInfo(CurrentScreen());
            }
        }

        if (_fastSwipeTimer)
        {
            _fastSwipeCounter++;
        }

        if ((DateTime.Now - clock_time).TotalMilliseconds > changeTime)
        {
            NextScreen();
            clock_time = DateTime.Now;
        }

    }

    private bool fastSwipe = false;

    //下一页
    public void NextScreen()
    {
        if (CurrentScreen() < _screens - 1)
        {

            _lerp = true;
            _lerp_target = _positions[CurrentScreen() + 1];

            ChangeBulletsInfo(CurrentScreen() + 1);
        }
        else if (CurrentScreen() == _screens - 1)
        {
            _lerp = true;
            _lerp_target = _positions[0];

            ChangeBulletsInfo(0);
        }
    }

    //上一页
    public void PreviousScreen()
    {
        if (CurrentScreen() > 0)
        {
            Debug.Log(CurrentScreen());
            _lerp = true;
            _lerp_target = _positions[CurrentScreen() - 1];

            ChangeBulletsInfo(CurrentScreen() - 1);
        }
    }

    private void NextScreenCommand()
    {
        if (_currentScreen < _screens - 1)
        {
            _lerp = true;
            _lerp_target = _positions[_currentScreen + 1];

            ChangeBulletsInfo(_currentScreen + 1);
        }else if(_currentScreen == _screens - 1)
        {
            _lerp = true;
            _lerp_target = _positions[0];

            ChangeBulletsInfo(0);
        }
    }
    private void PrevScreenCommand()
    {
        if (_currentScreen > 0)
        {
            _lerp = true;
            _lerp_target = _positions[_currentScreen - 1];

            ChangeBulletsInfo(_currentScreen - 1);
        }
    }

    //获取回到指定位置的坐标
    private Vector3 FindClosestFrom(Vector3 start, System.Collections.Generic.List<Vector3> positions)
    {
        Vector3 closest = Vector3.zero;
        float distance = Mathf.Infinity;

        foreach (Vector3 position in _positions)
        {
            if (Vector3.Distance(start, position) < distance)
            {
                distance = Vector3.Distance(start, position);
                closest = position;
            }
        }

        return closest;
    }

    //返回当前屏幕的标识索引
    public int CurrentScreen()
    {
        //此处需要将_screenContainer的Anchor设置Min(0,0)、Max(1,1);
        float absPoz = Math.Abs(_screensContainer.gameObject.GetComponent<RectTransform>().offsetMin.x);

        absPoz = Mathf.Clamp(absPoz, 1, _containerSize - 1);

        float calc = ((gameObject.GetComponent<RectTransform>().rect.width/2 - _screensContainer.gameObject.GetComponent<RectTransform>().localPosition.x) / _screensContainer.gameObject.GetComponent<RectTransform>().rect.width) * _screens;

        return (int)calc;
    }
    //改变底部标识
    private void ChangeBulletsInfo(int currentScreen)
    {
        Debug.Log(currentScreen);
        if (Pagination)
            for (int i = 0; i < Pagination.transform.childCount; i++)
            {
                Pagination.transform.GetChild(i).GetComponent<Toggle>().isOn = (currentScreen == i) ?
                    true :
                    false;
                if (Pagination.transform.GetChild(i).GetComponent<Toggle>().isOn)
                {
                    Pagination.transform.GetChild(i).localScale = new Vector3(1.5f, 1.5f, 1.5f);
                }
                else
                {
                    Pagination.transform.GetChild(i).localScale = new Vector3(1f, 1f, 1f);
                }
            }
    }

    //基于品目分辨率改变——screensContainer子物体的坐标和位置
    private void DistributePages()
    {
        int _offset = 0;
        int _step = (int)gameObject.GetComponent<RectTransform>().rect.width;
        _offset = _step / 2;
        int _dimension = 0;

        int currentXPosition = 0;

        for (int i = 0; i < _screensContainer.transform.childCount; i++)
        {
            RectTransform child = _screensContainer.transform.GetChild(i).gameObject.GetComponent<RectTransform>();
            currentXPosition = _offset + i * _step;
            child.anchoredPosition = new Vector2(currentXPosition, 0f);
            child.sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x-50, gameObject.GetComponent<RectTransform>().sizeDelta.y-50);
        }

        _dimension = currentXPosition + _offset * 1;

        _screensContainer.GetComponent<RectTransform>().offsetMax = new Vector2(_dimension, 0f);
    }

    #region Interfaces
    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = _screensContainer.localPosition;
        _fastSwipeCounter = 0;
        _fastSwipeTimer = true;
        _currentScreen = CurrentScreen();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _startDrag = true;
        if (_scroll_rect.horizontal)
        {
            if (UseFastSwipe)
            {
                fastSwipe = false;
                _fastSwipeTimer = false;
                if (_fastSwipeCounter <= _fastSwipeTarget)
                {
                    if (Math.Abs(_startPosition.x - _screensContainer.localPosition.x) > FastSwipeThreshold)
                    {
                        fastSwipe = true;
                    }
                }
                if (fastSwipe)
                {
                    if (_startPosition.x - _screensContainer.localPosition.x > 0)
                    {
                        NextScreenCommand();
                    }
                    else
                    {
                        PrevScreenCommand();
                    }
                }
                else
                {
                    _lerp = true;
                    _lerp_target = FindClosestFrom(_screensContainer.localPosition, _positions);
                }
            }
            else
            {
                _lerp = true;
                _lerp_target = FindClosestFrom(_screensContainer.localPosition, _positions);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        _lerp = false;
        if (_startDrag)
        {
            OnBeginDrag(eventData);
            _startDrag = false;
        }
    }
    #endregion
}

