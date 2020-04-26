// ========================================================
// Author：ChenGaoshuang 
// CreateTime：2020/04/25 18:13:35
// FileName：Assets/Scripts/UIWindow/TestWindow/TestWindow.cs
// ========================================================



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Test;

public class TestWindow : UIWindowBase<TestController,TestWindowRef> {

    protected override void Awake()
    {
        base.Awake();
    }


    protected override bool Open()
    {
        Ref.OnStartCallback = () =>
        {

        };
        Ctrler.iter = 1;
        return base.Open();

    }

    protected override bool Close()
    {
        return base.Close();
    }



}
