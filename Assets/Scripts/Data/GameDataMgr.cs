using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏数据管理类
/// </summary>
public class GameDataMgr
{

    private static GameDataMgr instance = new GameDataMgr();
    
    public static GameDataMgr Instance => instance;

    //音乐数据
    public MusicData musicData;

    //排行榜数据
    public RandkData rankData;

    //角色数据
    public  RoleData roleData;

    //子弹数据
    public BulletData bulletData;

    //开火点数据
    public FireData fireData;



    public int nowSelHeroIndex = 0;



    private GameDataMgr() {

        //音乐数据
        musicData = XmlDataMgr.Instance.LoadData(typeof(MusicData), "MusicData") as MusicData;
        //排行榜数据
        rankData = XmlDataMgr.Instance.LoadData(typeof(RandkData), "RandkData") as RandkData;
        //一开始就加载角色数据
        roleData = XmlDataMgr.Instance.LoadData(typeof(RoleData), "RoleData") as RoleData;
        //一开始就要记在子弹的数据
        bulletData = XmlDataMgr.Instance.LoadData(typeof(BulletData), "BulletData") as BulletData;

        //开火点的数据
        fireData = XmlDataMgr.Instance.LoadData(typeof(FireData), "FireData") as FireData;

    }


    #region 音乐音效相关
    //保存音乐相关的数据的方法
    public void SaveMusicData()
    {
        XmlDataMgr.Instance.SavaDate(musicData, "MusicData");
    }

    //开关背景音乐的方法
    public void SetMusicIsOpen(bool isOpen)
    {
        //
        musicData.musicIsOpen = isOpen;
        //开启音乐
        BKMusic.Instance.SetBKMusicIsOpen(isOpen);

    }

    public void SetSoundIsOpen(bool isOpen)
    {
        musicData.soundIsOpen = isOpen;

    }

    public void SetMusicValue(float value)
    {
        musicData.musicValue = value;
        //设置音乐大小
        BKMusic.Instance.SetBKMuiscValue(value);


    }

    public void SetSoundValue(float value)
    {
        musicData.soundValue = value;

    }

    #endregion


    #region 排行榜相关
    /// <summary>
    /// 添加排行榜数据
    /// </summary>
    /// <param name="name">玩家名</param>
    /// <param name="time">通关时间</param>
    public void AddRankData(string name, int time)
    {
        //单条数据
        RankInfo rankInfo = new RankInfo();
        rankInfo.name = name;
        rankInfo.time = time;
        rankData.rankList.Add(rankInfo);

        //排序
        rankData.rankList.Sort((a, b) =>
        {
            if (a.time > b.time)
                return -1;
            return 1;
        });

        //移除大于20条的内容
        if (rankData.rankList.Count > 20)
            rankData.rankList.RemoveAt(20);
        //rankData.rankList.RemoveRange(20, rankData.rankList.Count - 20);

        //保存数据    RandkData
        XmlDataMgr.Instance.SavaDate(rankData, "RandkData");
    }


    #endregion


    #region 角色数据相关

    public RoleInfo GetCurHeroInfo()
    {
        return roleData.roleList[this.nowSelHeroIndex];

    }


    #endregion



}
