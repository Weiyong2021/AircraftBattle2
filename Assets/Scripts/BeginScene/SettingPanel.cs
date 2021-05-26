using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : BasePanel<SettingPanel>
{
    public UIButton btnClose;

    public UIToggle togMusic;
    public UIToggle togSound;

    public UISlider sliderMusic;
    public UISlider sliderSound;



    public override void Init()
    {
        btnClose.onClick.Add(new EventDelegate(() => {
            //关闭设置按钮
            HideMe();

        
        }));


        togMusic.onChange.Add(new EventDelegate(() => {
            //音乐开关
            GameDataMgr.Instance.SetMusicIsOpen(togMusic.value);

        
        
        }));

        togSound.onChange.Add(new EventDelegate(() => {

            //音效开关
            GameDataMgr.Instance.SetSoundIsOpen(togSound.value);


        }));

        sliderMusic.onChange.Add(new EventDelegate(() => {
            //音乐大小
            GameDataMgr.Instance.SetMusicValue(sliderMusic.value);

        }));

        sliderSound.onChange.Add(new EventDelegate(() => {
            //音效大小
            GameDataMgr.Instance.SetSoundValue(sliderSound.value);



        }));



        //隐藏自己
        HideMe();


    }

    public  override void HideMe()
    {
        base.HideMe();

        //保存数据
        GameDataMgr.Instance.SaveMusicData();

    }

    public  override void ShowMe()
    {
        base.ShowMe();
        //更新数据
        MusicData m = GameDataMgr.Instance.musicData;
        sliderMusic.value = m.musicValue;
        sliderSound.value = m.soundValue;

        togMusic.value = m.musicIsOpen;
        togSound.value = m.soundIsOpen;


    }
}
