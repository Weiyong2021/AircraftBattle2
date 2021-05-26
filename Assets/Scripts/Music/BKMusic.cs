using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusic : MonoBehaviour
{
    private static BKMusic instance;
    public static BKMusic Instance => instance;
    private AudioSource audioSource;


    private void Awake()
    {
        instance = this;
        audioSource = this.GetComponent<AudioSource>();


        SetBKMusicIsOpen(GameDataMgr.Instance.musicData.musicIsOpen);

        //一开始进来就设置背景音乐
        SetBKMuiscValue(GameDataMgr.Instance.musicData.musicValue);


    }

    public void SetBKMuiscValue(float value)
    {
        audioSource.volume = value;

    }

    public void SetBKMusicIsOpen(bool isOpen)
    {
        audioSource.mute = !isOpen;

    }

}
