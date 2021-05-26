using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel:BasePanel<GamePanel>
{
    public UIButton btnBack;

    public UILabel labTime;

    public GameObject hpFather;


    List<GameObject> hpObjs = new List<GameObject>();

    public float curTime = 0f;


    public override void Init()
    {
        //
        this.FindHpObjs();

        btnBack.onClick.Add(new EventDelegate(() => {
            //淡出决定退出面板
            QuitPanel.Instance.ShowMe();
        
        }));

        //测试
       // ChangeHp(5);


       
       
    }


    private void FindHpObjs()
    {
       
        for (int i = 0; i < 10; ++i)
        {
            GameObject obj = hpFather.transform.GetChild(i).transform.GetChild(0).gameObject;
            if(obj!=null)
            {
               
                hpObjs.Add(obj);
            }
            
        }
    }


    public void ChangeHp(int hp)
    {
        for(int i=0;i<hpObjs.Count;++i)
        {
            hpObjs[i].SetActive(i < hp);

        }
    }

    
    void Update()
    {
        curTime += Time.deltaTime;
       
        labTime.text = "";
        //时
        if ((int)curTime / 3600 > 0)
        {
            labTime.text += (int)curTime / 3600 + "h";
        }
        //分
        if ((int)curTime % 3600 / 60 > 0 || labTime.text != "")
        {
            labTime.text += (int)curTime % 3600 / 60 + "m";
        }
        //秒
        labTime.text += (int)curTime % 60 + "s";


    }
}
