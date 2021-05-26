using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginPanel : BasePanel<BeginPanel>
{
    public UIButton btnSetting;
    public UIButton btnRank;
    public UIButton btnQuit;
    public UIButton btnStart;

   // public Transform showAriPlanePos;


    public override void Init()
    {
        //监听按钮事件
        btnStart.onClick.Add(new EventDelegate(()=> {
            //显示选角面板
            // print("//显示选角面板");
            ChooseHeroPanel.Instance.ShowMe();


            //隐藏自己
            HideMe();
        }));

        btnRank.onClick.Add(new EventDelegate(() => {
            //显示排行榜信息
            //print("//显示排行榜信息");
            RankPanel.Instance.ShowMe();


        }));

        btnSetting.onClick.Add(new EventDelegate(() => {
            //显示设置面板
            // print("//显示设置面板");
            SettingPanel.Instance.ShowMe();
        }));

        btnQuit.onClick.Add(new EventDelegate(() => {
            //退出游戏
            Application.Quit();
        }));

    }

    public  override void HideMe()
    {
        base.HideMe();

      

    }

    public  override void ShowMe()
    {
        base.ShowMe();
      
    }
}
