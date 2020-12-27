using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class music_ctrl : MonoBehaviour
{
    public AudioClip[] audios;
    public GameObject control;
    public GameObject gameVoiceControl;
    public bool ingame=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!ingame)
        {
            if (control != null)
            {
                this.GetComponent<AudioSource>().volume = control.transform.GetChild(2).GetComponent<Slider>().value/100f;
                userInfo.instance.game_voice = gameVoiceControl.transform.GetChild(2).GetComponent<Slider>().value / 100f;
            }
        }
        else
        {
            this.GetComponent<AudioSource>().volume = userInfo.instance.game_voice;
        }
    }

    public void changeMusicTo(int index)
    {
        this.GetComponent<AudioSource>().clip = audios[index];
        this.GetComponent<AudioSource>().Play();
    }

    public void changeMainMusicTo(int index)
    {
        userInfo.instance.msc_index = index;
    }

    public void changeGameMusicTo(int index)
    {
        userInfo.instance.game_music_index = index;
    }

    public void stop()
    {
        this.GetComponent<AudioSource>().Stop();
    }

    public void pause()
    {
        this.GetComponent<AudioSource>().Pause();
    }
}
