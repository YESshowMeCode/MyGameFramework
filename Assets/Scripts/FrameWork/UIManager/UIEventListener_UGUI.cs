// ========================================================
// Author：ChenGaoshuang 
// CreateTime：2020/04/25 10:16:14
// FileName：Assets/UIEventListener_UGUI.cs
// ========================================================



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEventListener_UGUI :MonoBehaviour {

    public delegate void voidDelegate(GameObject obj);

    public voidDelegate onClick;

    public static UIEventListener_UGUI Get(GameObject go)
    {
        UIEventListener_UGUI listener = go.GetComponent<UIEventListener_UGUI>();
        //go.onClick.AddListener(listener.OnClick);
        if (listener == null)
        {
            listener = go.AddComponent<UIEventListener_UGUI>();
        }
        return listener;
    }

	void OnClick()
    {
        if(onClick != null)
        {
            onClick(gameObject);
        }
    }
}
