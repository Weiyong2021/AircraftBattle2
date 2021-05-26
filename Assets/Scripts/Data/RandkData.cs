using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;


/// <summary>
/// 每一条排行榜的信息
/// </summary>
public class RankInfo
{
    [XmlAttribute]
    public string name;
    [XmlAttribute]
    public int time;

}


public class RandkData
{
    public List<RankInfo> rankList = new List<RankInfo>();

}
