using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSound : SoundProperty
{
    public AudioClip BGM;
    public AudioClip menuClick;
    public AudioClip menuHovering;
    public Button btn1, btn2, btn3;
    public void Start()
    {
        Sound.Ins.PlayBGM(BGM);


        btn1.onClick.AddListener(MenuClick);
        btn2.onClick.AddListener(MenuClick);
        btn3.onClick.AddListener(MenuClick);
     






    }
    public void MenuClick()
    {
        MySpeaker.PlayOneShot(menuClick);
    }
    public void MenuHovering()
    {
        MySpeaker.PlayOneShot(menuHovering);
    }
}
