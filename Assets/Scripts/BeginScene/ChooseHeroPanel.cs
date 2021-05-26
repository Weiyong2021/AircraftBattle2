using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseHeroPanel :BasePanel<ChooseHeroPanel>
{
    public UIButton btnClose;
    public UIButton btnRight;
    public UIButton btnLeft;
    public UIButton btnStart;


    public Camera uiCamera;




    public List<GameObject> hpObjs = new List<GameObject>();
    public List<GameObject> speedObjs = new List<GameObject>();
    public List<GameObject> volumeObjs = new List<GameObject>();


    private GameObject curAirPanle;


    private float time = 0f;


    


    public override void Init()
    {
        btnClose.onClick.Add(new EventDelegate(() => {
            //隐藏自己
            HideMe();
            //显示开始面板
            BeginPanel.Instance.ShowMe();

        }));

        btnLeft.onClick.Add(new EventDelegate(() => {
            //选择角色
            --GameDataMgr.Instance.nowSelHeroIndex;
            if(GameDataMgr.Instance.nowSelHeroIndex<0)
            {
                GameDataMgr.Instance.nowSelHeroIndex = GameDataMgr.Instance.roleData.roleList.Count - 1;
            }
            this.ChangeNowHero();
        
        
        }));
       
        btnRight.onClick.Add(new EventDelegate(() => {
            //选择角色
            ++GameDataMgr.Instance.nowSelHeroIndex;
            if (GameDataMgr.Instance.nowSelHeroIndex > GameDataMgr.Instance.roleData.roleList.Count - 1)
            {
                GameDataMgr.Instance.nowSelHeroIndex = 0;
            }
            this.ChangeNowHero();


        }));

        btnStart.onClick.Add(new EventDelegate(() => {

            //加载游戏场景
            SceneManager.LoadScene("GameScene");
        }));


        HideMe();

    }

    /// <summary>
    /// 切换角色的选择
    /// </summary>
    private void ChangeNowHero()
    {
        //
        RoleInfo info = GameDataMgr.Instance.GetCurHeroInfo();
        //更新模型

        //先删除上一次的飞机模型
        this.DestroyObj();


        //在创建新的飞机模型
        curAirPanle = Instantiate<GameObject>(Resources.Load<GameObject>(info.resName));
        curAirPanle.transform.SetParent(GameObject.Find("HeroPos").transform, false);

        curAirPanle.transform.localPosition = Vector3.zero;
        curAirPanle.transform.localRotation= Quaternion.identity;
       
        curAirPanle.transform.localScale = Vector3.one * info.scale;

        //设置层级
        curAirPanle.layer = LayerMask.NameToLayer("UI");



        //更新属性
        for(int i=0;i<10;++i)
        {
            hpObjs[i].SetActive(i < info.hp);
            speedObjs[i].SetActive(i < info.speed);
            volumeObjs[i].SetActive(i < info.volume);

        }

    }

    private void DestroyObj()
    {
        if(curAirPanle!=null)
        {
            Destroy(curAirPanle);
            curAirPanle = null;

        }
    }


    public override void ShowMe()
    {
        base.ShowMe();

        GameDataMgr.Instance.nowSelHeroIndex = 0;

        ChangeNowHero();

        
    }

    public override void HideMe()
    {
        base.HideMe();

        this.DestroyObj();


    }


    private bool isSel;

    void Update()
    {
        time += Time.deltaTime;
        //让角色在面板上自己上下移动
        curAirPanle.transform.Translate(Vector3.up * Mathf.Sin(time)*0.00035f,Space.World);

        //检测是否击中
        if(Input.GetMouseButton(0))
        {
            if(Physics.Raycast(uiCamera.ScreenPointToRay(Input.mousePosition),1000,1<<LayerMask.NameToLayer("UI")))
            {
                isSel = true;
            }
            
        }
        //鼠标抬起就没有击中
        if(Input.GetMouseButtonUp(0))
        {
            isSel = false;
        }
        //鼠标按下而且击中飞机才可以旋转
        if(Input.GetMouseButton(0)&&isSel)
        {                                              //Mouse X
            GameObject.Find("HeroPos").transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 20, Vector3.up);

        }
    }
}
