using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankItem : MonoBehaviour
{
    public UILabel labRank;

    public UILabel labName;
    public UILabel labTime;


    public void InitInfo(int rank,string name,int time)
    {
        this.labRank.text = rank.ToString();
        this.labName.text = name;
        string str = "";

        if(time/3600>0)
        {
            str += time / 3600 + "h";
        }

        if(time%3600/60>0||str!="")
        {
            str += time % 3600 / 60 + "m";
        }

        str += time%60 + "'";

        labTime.text = str;


    }

}
