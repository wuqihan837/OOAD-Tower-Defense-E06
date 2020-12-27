using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SelectSuperTower : MonoBehaviour
{ 
    [SerializeField]
    private UIType[] types = new UIType[4];
    [SerializeField]
    private UnityEvent[] events = new UnityEvent[4];
    [SerializeField]
    private Sprite[] sprites = new Sprite[4];
    [SerializeField]
    private int[] sizes = new int[4];

    public PiUI buffshopUI;

    public void Submit(List<bool> isSelected)
    {
            int tmp = 0;
            for(int i = 0; i < 4; i ++)
            {
                // 如果被选中，就去set对应的ui块的type
                if(isSelected[i])
                {
                Debug.Log(i);
                    buffshopUI.SetSliceType(tmp, types[i], events[i],sprites[i], sizes[i]);
                    tmp++;
                }
            }
    }
}
