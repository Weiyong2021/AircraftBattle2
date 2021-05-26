using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
/// <summary>
/// 角色数据集合
/// </summary>
public class RoleInfo
{

    [XmlAttribute]
    public int hp;//血量
    [XmlAttribute]
    public int speed;//速度
    [XmlAttribute]
    public int volume;//体积
    [XmlAttribute]
    public int scale;//缩放大小
    [XmlAttribute]
    public string resName;//资源路径


}
public class RoleData
{
    public List<RoleInfo> roleList = new List<RoleInfo>();
}
