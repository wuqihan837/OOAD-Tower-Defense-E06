using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_Conctrl : MonoBehaviour
{
    [Header("PANEL LIST")]
    public List<GameObject> panels = new List<GameObject>();
    // Start is called before the first frame update
    private GameObject currentPanel;
    private Animator currentPanelAnimator;
    public int currentPanelIndex = 0;
    string panelFadeIn = "Panel In";
    //string panelFadeOut = "Panel Out";
    void Start()
    {
        currentPanel = panels[currentPanelIndex];
        currentPanelAnimator = currentPanel.GetComponent<Animator>();
        currentPanelAnimator.Play(panelFadeIn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
