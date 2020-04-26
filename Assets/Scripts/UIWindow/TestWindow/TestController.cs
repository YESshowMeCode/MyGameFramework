// ========================================================
// Author：ChenGaoshuang 
// CreateTime：2020/04/25 18:13:50
// FileName：Assets/Scripts/UIWindow/TestWindow/TestController.cs
// ========================================================



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Test;

public class TestController :UIBaseController<TestWindow> {


    public int iter = 0;
    public override void PanelOpened(CtrlParams pars)
    {
        base.PanelOpened(pars);
    }

    public override void PanelClose()
    {
        base.PanelClose();
    }
}
