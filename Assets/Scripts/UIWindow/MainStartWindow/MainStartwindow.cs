﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoginWindow;

public class MainStartwindow : UIWindowBase<MainStartController, LoginWindowRef>
{

    protected override void Awake()
    {
        base.Awake();
    }

    protected override bool Open()
    {
        Ref.OnStartCallback = () =>
        {
            Debug.LogError("123");
        };
        //Ctrler = 0;
        return base.Open();

    }
}
