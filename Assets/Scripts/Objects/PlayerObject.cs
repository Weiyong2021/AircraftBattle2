using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家的脚本
/// </summary>
public class PlayerObject : MonoBehaviour
{
    //当前的血量
    public int nowhp; 
    //最大血量
    public int maxHp;
    //是否死亡
    public bool isDead;
    //移动速度
    public float speed;
    //旋转速度
    public float roundSpeed;
   
    public Quaternion targetQ;
    //上一帧数的位置
    private Vector3 forntPos;
    //当前帧数的位置
    private Vector3 curPos;

    //渲染玩家的相机
    private Camera playerCamera;

    //玩家的位置转为屏幕的位置
    private Vector3 worldToScreePos;

    //
    RaycastHit hitInfo;


    private static PlayerObject instance;
    public static PlayerObject Instance => instance;
    private void Awake()
    {
        instance = this;

        playerCamera = GameObject.Find("PlayerCamera").transform.GetComponent<Camera>();


    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    /// <param name="nowhp"></param>
    /// <param name="maxHp"></param>
    /// <param name="speed"></param>
    /// <param name="roundSpeed"></param>
    public void Init(int nowhp, int maxHp, float speed, float roundSpeed=20)
    {
        this.nowhp = nowhp;
        this.maxHp = maxHp;
        this.speed = speed;
        this.roundSpeed = roundSpeed;
    }



    /// <summary>
    /// 死亡函数
    /// </summary>
    public void Dead()
    {
        isDead = true;
        //显示游戏结束面板
        GameOverPanel.Instance.ShowMe();
        
        
    }
   /// <summary>
   /// 受伤函数
   /// </summary>
    public void would()
    {
        if (isDead)
            return;
        //见血
        this.nowhp -= 1;
        GamePanel.Instance.ChangeHp(this.nowhp);
        if(this.nowhp<1)
        {
            this.Dead();
        }
    }

    private float hValue;
    private float vValue;


    void Update()
    {
        if (isDead)
            return;//如果玩家直接挂了就直接返回

        //旋转移动逻辑

        hValue = Input.GetAxisRaw("Horizontal");//左右键盘AD输入获取得到的值左边为-1  右边为1
        vValue = Input.GetAxisRaw("Vertical");

        if (hValue == 0)//0为没有按下AD输入键盘
        {
            targetQ = Quaternion.identity;//四元数为单位四元数  旋转量为0

        }
        else
            targetQ = hValue < 0 ? Quaternion.AngleAxis(20, Vector3.forward) : Quaternion.AngleAxis(-20, Vector3.forward);

        //旋转逻辑。。。
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetQ, Time.deltaTime*roundSpeed);


        curPos = this.transform.position;
        forntPos = curPos;

        //移动逻辑   移动过后要判断是否超过了屏幕的范围。。。。。。。。。。
        this.transform.Translate(hValue * Vector3.right * speed * Time.deltaTime,Space.World);
        this.transform.Translate(vValue * Vector3.forward * speed * Time.deltaTime);

        worldToScreePos = playerCamera.WorldToScreenPoint(this.transform.position);
        // print(worldToScreePos);
        if(worldToScreePos.x<0||worldToScreePos.x>=Screen.width)
        {
            this.transform.position = new Vector3(forntPos.x, this.transform.position.y, this.transform.position.z);
            curPos = this.transform.position;

        }

        if (worldToScreePos.y< 0 || worldToScreePos.y>= Screen.height)
        {
            //这里要改变的是z抽的坐标。。。。。。。。。。。。
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y,forntPos.z );
            curPos = this.transform.position;


        }


        //射线检测

        //本来是要点击鼠标左键才可以使用射线检测
        //但是太累了，就在这里直接使用鼠标的坐标，而不是去点击才发起检测
        //

       
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo, 1000f, 1 << LayerMask.NameToLayer("Bullet")))
        {
            GameObject bulletObj = hitInfo.collider.gameObject;
            var bullet = bulletObj.GetComponent<BulletObject>();
            if (bullet != null && bullet is BulletObject)
            {
                bullet.Dead();

            }

        }
      
    }

}
