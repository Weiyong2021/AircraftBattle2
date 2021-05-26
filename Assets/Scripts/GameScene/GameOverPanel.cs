using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverPanel : BasePanel<GameOverPanel>
{
    public UIButton btnSure;

    public UILabel labTime;
    public UIInput inputName;

    private float endTime = 0f;


    public override void Init()
    {
        btnSure.onClick.Add(new EventDelegate(()=>{

            //记录数据
            GameDataMgr.Instance.AddRankData(inputName.value, (int)endTime);

            SceneManager.LoadScene("BeginScene");

        
        }));

        //Invoke("Test", 6);

        HideMe();
    }

    //测试
    private void Test()
    {
        GameOverPanel.Instance.ShowMe();
    }

    public override void ShowMe()
    {
        base.ShowMe();

        endTime = GamePanel.Instance.curTime;

        labTime.text = GamePanel.Instance.labTime.text;

        //设置时间缩放为0，停止游戏的运行
        //Time.timeScale = 0;


    }


    public override void HideMe()
    {
        base.HideMe();

       // Time.timeScale = 1;
    }
}
