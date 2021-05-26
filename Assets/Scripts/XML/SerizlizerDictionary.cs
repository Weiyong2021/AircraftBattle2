using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;
/// <summary>
/// 经过自己序列化的字典类，本来字典是不支持xml的序列化和反序列化的
/// 但是自己可以手动实现字典的xml序列化和反序列化
/// 以后使用字典存储数据的话，就是使用经过自己序列化和反序列化后的字典
/// </summary>
public class SerizlizerDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
{
    //这个是固定规则，可以不用理他，就这样写就可以了
    public XmlSchema GetSchema()
    {
        return null;
    }


    //自定义字典的 反序列化 规则
    public void ReadXml(XmlReader reader)
    {
        XmlSerializer keySer = new XmlSerializer(typeof(TKey));
        XmlSerializer valueSer = new XmlSerializer(typeof(TValue));

        //要跳过根节点
        reader.Read();
        //判断 当前不是元素节点 结束 就进行 反序列化
        while (reader.NodeType != XmlNodeType.EndElement)
        {
            //反序列化键
            TKey key = (TKey)keySer.Deserialize(reader);
            //反序列化值
            TValue value = (TValue)valueSer.Deserialize(reader);
            //存储到字典中
            this.Add(key, value);
        }


    }

    //自定义 字典的 序列化 规则
    public void WriteXml(XmlWriter writer)
    {
        XmlSerializer keySer = new XmlSerializer(typeof(TKey));
        XmlSerializer valueSer = new XmlSerializer(typeof(TValue));

        foreach (KeyValuePair<TKey, TValue> kv in this)
        {
            //键值对 的序列化
            keySer.Serialize(writer, kv.Key);
            valueSer.Serialize(writer, kv.Value);
        }
    }

}
