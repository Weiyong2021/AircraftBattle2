using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

//单个子弹的数据信息
public class BulletInfo
{
    [XmlAttribute]
    public int id;
    [XmlAttribute]
    public int type;//子弹的移动规则  1-5代表
    [XmlAttribute]
    public float forwardSpeed;//正朝向移动移速
    [XmlAttribute]
    public float rightSpeed;//横向的移动速度
    [XmlAttribute]
    public float roundSpeed;//旋转的速度
    [XmlAttribute]
    public string resName;//子弹预设资源的路径
    [XmlAttribute]
    public string deadEffRes;//子弹消亡的特效资源路径
    [XmlAttribute]
    public float liveTime;//子弹的生命周期

}

/// <summary>
/// 子弹数据集合
/// </summary>
public class BulletData 
{
    public List<BulletInfo> bulletList = new List<BulletInfo>();

}
