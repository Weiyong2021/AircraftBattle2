using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        print(Application.persistentDataPath);
        //得到玩家的信息数据
        RoleInfo info = GameDataMgr.Instance.GetCurHeroInfo();
        GameObject playerObj = Instantiate<GameObject>(Resources.Load<GameObject>(info.resName));
        PlayerObject player = playerObj.AddComponent<PlayerObject>();
        //初始化数据
        player.Init(info.hp, 10, info.speed*50);

        //更新面板撒上玩家的数据
        GamePanel.Instance.ChangeHp(info.hp);

    }

}
