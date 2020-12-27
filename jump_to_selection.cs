using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UIElements;

public class jump_to_selection : MonoBehaviour
{
    [Header("PANEL LIST")]
    public List<GameObject> panels = new List<GameObject>(); 

    [Header("BUTTON LIST")]
    public List<GameObject> buttons = new List<GameObject>();

    private GameObject currentPanel;
    private GameObject nextPanel;
    private GameObject currentButton;
    private GameObject nextButton;
    private Animator currentPanelAnimator;
    private Animator nextPanelAnimator;
    private Animator currentButtonAnimator;
    private Animator nextButtonAnimator; 
    string buttonFadeIn = "Hover to Pressed";
    string buttonFadeOut = "Pressed to Normal";
    string panelFadeIn = "Panel In";
    string panelFadeOut = "Panel Out";
    [Header("SETTINGS")]
    public int currentPanelIndex = 0;
    private int currentButtonlIndex = 0;

    public bool enableButton = true;
    //public int sceneIndex = 1;
    //public bool jumpScene = false;
    //public DateTime oldtime;
    //public GameObject loading;

    void Start(){
        if (enableButton)
        {
            currentButton = buttons[currentPanelIndex];
            currentButtonAnimator = currentButton.GetComponent<Animator>();
            currentButtonAnimator.Play(buttonFadeIn);
        }
        

        currentPanel = panels[currentPanelIndex];
        currentPanelAnimator = currentPanel.GetComponent<Animator>();
        currentPanelAnimator.Play(panelFadeIn);
    }

    public void PanelAnim(int newPanel)
        {
            if (newPanel != currentPanelIndex)
            {
                currentPanel = panels[currentPanelIndex];

                currentPanelIndex = newPanel;
                nextPanel = panels[currentPanelIndex];

                currentPanelAnimator = currentPanel.GetComponent<Animator>();
                nextPanelAnimator = nextPanel.GetComponent<Animator>();

                currentPanelAnimator.Play(panelFadeOut);
                nextPanelAnimator.Play(panelFadeIn);

                if (enableButton)
                {
                    currentButton = buttons[currentButtonlIndex];

                    currentButtonlIndex = newPanel;
                    nextButton = buttons[currentButtonlIndex];

                    currentButtonAnimator = currentButton.GetComponent<Animator>();
                    nextButtonAnimator = nextButton.GetComponent<Animator>();

                    currentButtonAnimator.Play(buttonFadeOut);
                    nextButtonAnimator.Play(buttonFadeIn);
                }

            }
        }

    public void next_panel()
    {
        currentPanel = panels[currentPanelIndex];
        int newPanel = currentPanelIndex;
        if (newPanel == panels.Count-1)
        {
            newPanel = 0;
        }
        else
        {
            newPanel++;
        }


        currentPanelIndex = newPanel;
        nextPanel = panels[currentPanelIndex];

        currentPanelAnimator = currentPanel.GetComponent<Animator>();
        nextPanelAnimator = nextPanel.GetComponent<Animator>();

        currentPanelAnimator.Play(panelFadeOut);
        nextPanelAnimator.Play(panelFadeIn);

        if (enableButton)
        {
            currentButton = buttons[currentButtonlIndex];

            currentButtonlIndex = newPanel;
            nextButton = buttons[currentButtonlIndex];

            currentButtonAnimator = currentButton.GetComponent<Animator>();
            nextButtonAnimator = nextButton.GetComponent<Animator>();

            currentButtonAnimator.Play(buttonFadeOut);
            nextButtonAnimator.Play(buttonFadeIn);
        }

    }

    public void pre_panel()
    {
        currentPanel = panels[currentPanelIndex];
        int newPanel = currentPanelIndex;
        if (newPanel == 0)
        {
            newPanel = panels.Count - 1;
        }
        else
        {
            newPanel--;
        }


        currentPanelIndex = newPanel;
        nextPanel = panels[currentPanelIndex];

        currentPanelAnimator = currentPanel.GetComponent<Animator>();
        nextPanelAnimator = nextPanel.GetComponent<Animator>();

        currentPanelAnimator.Play(panelFadeOut);
        nextPanelAnimator.Play(panelFadeIn);

        if (enableButton)
        {
            currentButton = buttons[currentButtonlIndex];

            currentButtonlIndex = newPanel;
            nextButton = buttons[currentButtonlIndex];

            currentButtonAnimator = currentButton.GetComponent<Animator>();
            nextButtonAnimator = nextButton.GetComponent<Animator>();

            currentButtonAnimator.Play(buttonFadeOut);
            nextButtonAnimator.Play(buttonFadeIn);
        }

    }

    /* void Update(){
        if(!jumpScene){
            oldtime = DateTime.Now;
            currentbuttonanimator = currentbutton.GetComponent<Animator>();
            currentbuttonanimator.Play(buttonFadeIn);
        } else {
            if((DateTime.Now-oldtime).TotalMilliseconds>1000){
                jumpScene = false;
                SceneManager.LoadScene(sceneIndex); 
            } else {
                nextbuttonanimator = nextbutton.GetComponent<Animator>();
                nextbuttonanimator.Play(buttonFadeIn);
            }
        }
    }
    public void LoadsGame(){
        sceneIndex = 0;
        jumpScene = true;
        loading.SetActive(true);
    }

    public void LoadsSetting(GameObject next){
        sceneIndex = 4;
        jumpScene = true;
        loading.SetActive(true);
        currentbuttonanimator = currentbutton.GetComponent<Animator>();
        currentbuttonanimator.Play(buttonFadeOut);
        nextbutton = next;
        nextbuttonanimator = nextbutton.GetComponent<Animator>();
        nextbuttonanimator.Play(buttonFadeIn);
    }

    public void LoadsAchievements(GameObject next){
        sceneIndex = 3;
        jumpScene = true;
        loading.SetActive(true);
        currentbuttonanimator = currentbutton.GetComponent<Animator>();
        currentbuttonanimator.Play(buttonFadeOut);
        nextbutton = next;
        nextbuttonanimator = nextbutton.GetComponent<Animator>();
        nextbuttonanimator.Play(buttonFadeIn);
    }

    public void LoadsStarter(GameObject next){
        sceneIndex = 1;
        jumpScene = true;
        loading.SetActive(true);
        currentbuttonanimator = currentbutton.GetComponent<Animator>();
        currentbuttonanimator.Play(buttonFadeOut);
        nextbutton = next;
        nextbuttonanimator = nextbutton.GetComponent<Animator>();
        nextbuttonanimator.Play(buttonFadeIn);
    }*/

    public void quit(){
        Application.Quit();
    } 
}
