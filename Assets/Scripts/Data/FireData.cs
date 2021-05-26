using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class FireInfo
{
    [XmlAttribute]
    public int id;
    [XmlAttribute]
    public int type;//发火点类型
    [XmlAttribute]
    public int num;//数量   该组子弹右多少个子弹
    [XmlAttribute]
    public float cd;//每一科子弹发射的间隔
    [XmlAttribute]
    public string ids;//关联的子弹ID  1,10代表
    [XmlAttribute]
    public float delay;//一组子弹发射之后的  时间的间隔


}

public class FireData 
{
    public List<FireInfo> fireInfoList = new List<FireInfo>();


}
