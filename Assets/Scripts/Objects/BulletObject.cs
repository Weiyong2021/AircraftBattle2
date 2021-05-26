using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 子弹对象
/// </summary>
public class BulletObject : MonoBehaviour
{
    //子弹使用的数据
    private BulletInfo info;


    private float time = 0f;


    //初始化子弹数据的方法
    public void InitInfo(BulletInfo info)
    {
        this.transform.SetParent(GameObject.Find("Main").transform, false);

        this.info = info;
        Invoke("DelayDestroy", this.info.liveTime);//延时移除子弹

    }

    //我搞在这里翻车了一天。。。。。。。。。。。。。。。。。。。。。。。。。。
    //private void Start()
    //{
    //    this.info = GameDataMgr.Instance.bulletData.bulletList[16];

    //}

    private void  DelayDestroy()
    {
        // Destroy(this.gameObject);
        this.Dead();

    }
    
    /// <summary>
    /// 销毁场景上的子弹
    /// </summary>
    public void Dead()
    {
        //创建死亡的特效
        GameObject obj = Instantiate<GameObject>(Resources.Load<GameObject>(this.info.deadEffRes));
       
        obj.transform.position = this.transform.position;
        Destroy(obj, 1f);
        Destroy(this.gameObject);//销毁子弹
    }

    //和玩家碰撞的时候，就触发
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerObject obj = other.gameObject.GetComponent<PlayerObject>();
            //玩家受伤
            obj.would();
            //销毁自己
            this.Dead();
        }
    }

    void Update()
    {

        //所有的共同的特点都是朝自己的面朝向移动
        this.transform.Translate(Vector3.forward * info.forwardSpeed * Time.deltaTime);
        
        switch (this.info.type)
        {
            //1.为面朝自己的前面运动
          
            case 2:
                time += Time.deltaTime;
                //曲线运动
                this.transform.Translate(Vector3.right * Time.deltaTime * Mathf.Sin(time*info.roundSpeed) *info.rightSpeed);
                break;
            case 3:
                //右边抛物线
                this.transform.rotation *= Quaternion.AngleAxis(info.roundSpeed * Time.deltaTime, Vector3.up);

                break;
            case 4:
                //左边抛物线
                this.transform.rotation *= Quaternion.AngleAxis(-info.roundSpeed * Time.deltaTime, Vector3.up);

                break;
            case 5:
                //跟踪弹
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                    Quaternion.LookRotation(PlayerObject.Instance.transform.position-this.transform.position)
                    ,Time.deltaTime*info.roundSpeed);

                break;
        }

    }

}
