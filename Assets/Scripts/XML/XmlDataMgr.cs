using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
/// <summary>
/// xml数据管理类
/// </summary>
public class XmlDataMgr
{
    #region 单例模式
    private static XmlDataMgr instance = new XmlDataMgr();

    private XmlDataMgr()
    {

    }

    public static XmlDataMgr Instance
    {
        get
        {
            return instance;
        }

    }

    #endregion


    /// <summary>
    /// 保存数据到xml中 
    /// </summary>
    /// <param name="data">数据对象</param>
    /// <param name="fileName">文件名</param>
    public void SavaDate(object data,string fileName)
    {
        //得到存储路径
        string path = Application.persistentDataPath + "\\" + fileName + ".xml";
        //存储文件
        using (StreamWriter writer = new StreamWriter(path))
        {
            //序列化
            XmlSerializer s = new XmlSerializer(data.GetType());

            s.Serialize(writer, data);
            
        }
        
    }

    /// <summary>
    /// 从xml文件中读取数据
    /// </summary>
    /// <param name="type">对象类型</param>
    /// <param name="fileName">文件名</param>
    /// <returns></returns>

    public object LoadData(Type type,string fileName)
    {
        //1.判断文件是否存在
        string path = Application.persistentDataPath + "\\" + fileName + ".xml";

        if(!File.Exists(path))//打包过后一开始进入游戏可能文件不存在，而是存在
        {                     //streamingAssets文件夹下，所以先来这么一个判断
            path =Application.streamingAssetsPath + "\\" + fileName + ".xml";

            //在如果
            if(!File.Exists(path))
            {
                //如果根本不存在文件 两个路径都找了
                //都没有，那么直接new一个对象 
                //返回给外部   无非里面都是默认值
                return Activator.CreateInstance(type);

            }
        }

        //2.存在就读取
        using (StreamReader reader = new StreamReader(path))
        {
            XmlSerializer s = new XmlSerializer(type);
            return s.Deserialize(reader);
        }
        
    }
}
