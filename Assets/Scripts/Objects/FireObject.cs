using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_Pos_Type
{
    TopLeft,
    Top,
    TopRight,
    Left,
    Right,
    DownLeft,
    Down,
    DownRight

}

public class FireObject : MonoBehaviour
{
    public E_Pos_Type type;

    public Camera playerCamera;

    private Vector3 screenPos;


    private Vector3 initDir;


    private FireInfo nowFireInfo;

    private int nowNum;
    private float nowCd;
    private float nowDelay;

    //子弹的数据信息
    private BulletInfo nowBulletInfo;


    //
    private float changeAngle;


    private Vector3 nowDir;

    void Start()
    {
        //先看看玩家所在的横截面是多少
        // print(camera.WorldToScreenPoint(PlayerObject.Instance.transform.position));
        //结果是194；
    }

    void Update()
    {
        this.UpdatePos();

        //每一次都检测是否都需要重设置开火点数据
        this.ResetFireInfo();

        //发射子弹
        this.UpdateFire();

    }

    //根据屏幕位置的点把八个屏幕的点转化为
    //世界坐标系的点
    
    private void UpdatePos()
    {
        screenPos.z = 194f;

        switch (type)
        {
            case E_Pos_Type.TopLeft:
                screenPos.x = 0f;
                screenPos.y = Screen.height;
                initDir = Vector3.right;
                break;
            case E_Pos_Type.Top:
                screenPos.x = Screen.width / 2;
                screenPos.y = Screen.height;
                initDir = Vector3.right;
                break;
            case E_Pos_Type.TopRight:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height;
                initDir = Vector3.left;
                break;
            case E_Pos_Type.Left:
                screenPos.x = 0;
                screenPos.y = Screen.height/2;
                initDir = Vector3.right;
                break;
            case E_Pos_Type.Right:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height / 2;
                initDir = Vector3.left;
                break;
            case E_Pos_Type.DownLeft:
                screenPos.x = 0;
                screenPos.y = 0;
                initDir = Vector3.right;
                break;
            case E_Pos_Type.Down:
                screenPos.x = Screen.width/2;
                screenPos.y = 0;
                initDir = Vector3.right;
                break;
            case E_Pos_Type.DownRight:
                screenPos.x = Screen.width;
                screenPos.y = 0;
                initDir = Vector3.left;

                break;
        }

        //在把
        this.transform.position = playerCamera.ScreenToWorldPoint(screenPos);

    }



    /// <summary>
    /// 重设置当前发射的炮台数据
    /// </summary>
    private void ResetFireInfo()
    {
        //只有当cd和数量为零的时候

        if(nowNum!=0&&nowCd!=0)
        {
            return;
        }
        //组间
        if(nowFireInfo!=null)
        {
            this.nowDelay -= Time.deltaTime;
            if (this.nowDelay > 0)
                return;
        }
        //拿到配置表的数据
        List<FireInfo> list = GameDataMgr.Instance.fireData.fireInfoList;
       
        nowFireInfo= list[Random.Range(0, list.Count)];
        //注意这里必须使用保留存储，不能直接修改list里面的数据
        nowNum = nowFireInfo.num;//开火的子弹的数据
        nowCd = nowFireInfo.cd;//每一个子弹冷却的时间
        nowDelay = nowFireInfo.delay;//一波子弹发射完后第二次开火的时间间隔

        string[] strs = nowFireInfo.ids.Split(',');
        //得到开始id和结束id  1到10
        int beginID = int.Parse(strs[0]);
        int endId = int.Parse(strs[1]);

        int randomBulletID = Random.Range(beginID, endId + 1);//1到10
        //0到9
        nowBulletInfo = GameDataMgr.Instance.bulletData.bulletList[randomBulletID-1];
       

        if( nowFireInfo.type==2 )//表示是发射的是散弹
        {
            switch (type)
            {
                case E_Pos_Type.TopLeft:
                case E_Pos_Type.TopRight:
                case E_Pos_Type.DownLeft:
                case E_Pos_Type.DownRight:
                    changeAngle = 180f / (nowNum + 1);
                    break;
                case E_Pos_Type.Top:
                case E_Pos_Type.Left:
                case E_Pos_Type.Right:
                case E_Pos_Type.Down:
                    changeAngle = 90f / (nowNum + 1);

                    break;
              
            }
        }
    }



    //从配置文件中读到的数据然后根据数据来发射子弹的函数
    private void UpdateFire()
    {
        if (nowCd == 0 && nowNum == 0)
            return;

        nowCd -= Time.deltaTime;
        if(nowCd>0)
        {
            return;
        }
        GameObject bullet;
        BulletObject obj;
       
        switch (nowFireInfo.type)
        {
           
            case 1://表示一个一个的发。。。
                bullet =Instantiate(Resources.Load<GameObject>(nowBulletInfo.resName));
                obj = bullet.AddComponent<BulletObject>();
               
                obj.InitInfo(nowBulletInfo);

                //设置子弹的位置
                obj.transform.position = this.transform.position;

                //设置方向                                                                                 //在这里错误了
                bullet.transform.rotation = Quaternion.LookRotation(PlayerObject.Instance.transform.position -this.transform.position /*obj.transform.position*/);

                //
                --nowNum;
                nowCd = nowNum == 0 ? 0 : nowFireInfo.cd;

                break;
            case 2://散弹。。。。。
                if(this.nowFireInfo.cd==0)//注意这里不能使用nowC来判断不然会进不来
                {//
                    for(int i=0;i<this.nowNum;++i)
                    {
                        bullet =Instantiate(Resources.Load<GameObject>(nowBulletInfo.resName));
                        obj = bullet.AddComponent<BulletObject>();
                        
                        obj.InitInfo(nowBulletInfo);

                        //设置子弹的位置
                        obj.transform.position = this.transform.position;
                       
                        //设置朝向
                       
                        nowDir = Quaternion.AngleAxis(changeAngle * i, Vector3.up)*initDir;
                       
                        bullet.transform.rotation = Quaternion.LookRotation(nowDir);

                    }
                    nowCd = 0;
                    nowNum = 0;
                }
                else
                {
                    bullet = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resName));
                    obj = bullet.AddComponent<BulletObject>();

                    obj.InitInfo(nowBulletInfo);

                    //设置子弹的位置
                    obj.transform.position = this.transform.position;
                 

                    //设置朝向
                    nowDir = Quaternion.AngleAxis(changeAngle *(nowFireInfo.num-nowNum), Vector3.up) * initDir;
                    bullet.transform.rotation = Quaternion.LookRotation(nowDir);

                    --this.nowNum;
                    nowCd = nowNum == 0 ? 0 : nowFireInfo.cd;

                }
                break;
        }
    }
}
