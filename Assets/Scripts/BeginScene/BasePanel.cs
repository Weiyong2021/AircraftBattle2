using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel<T> : MonoBehaviour where T:class
{

    #region 单例模式
    private static T instance;

    public static T Instance => instance;

    protected virtual void Awake()
    {
        instance = this as T;

    }

    #endregion


    //强行的执行初始化函数     让子类去重写该初始化函数
    void Start()
    {
        Init();
    }

    public abstract void Init();


    public  virtual void ShowMe()
    {
        this.gameObject.SetActive(true);

    }

    public  virtual void HideMe()
    {
        this.gameObject.SetActive(false);

    }
}
