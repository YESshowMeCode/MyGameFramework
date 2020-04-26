// ========================================================
// Author：ChenGaoshuang 
// CreateTime：2020/04/25 10:16:14
// FileName：Assets/UIEventListener_UGUI.cs
// ========================================================



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIEventListener_UGUI :BaseClassScript,IPointerDownHandler,IPointerUpHandler,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler {

    public enum SoundAudio
    {
        None = 0,
        UI_Click = 1,
    }
    public delegate void Action();

    public SoundAudio AudioEnum = SoundAudio.None;

    private bool isMouseEntered = false;
    private bool isMousePressing = false;
    private bool isLongPressed = false;
    private bool isPressDown = false;

    float longPressMaxTime;
    private float longPressDTime = 0.0f;

    //Click事件
    public Action NGClick;
    public Action UnlockClick;
    public Action LockedClick;
    public UIDele.Dele<GameObject> onClick;

    //长按事件
    public UIDele.Dele<GameObject> OnLongPress;//多次触发
    public Action OnLongPressOnceEnter;//长按只触发一次
    public Action OnLongPressOnceEnd;
    public UIDele.Dele<GameObject> onHover;//按住间隔0.1s后调用，不接受out之类


    //基础事件
    public UIDele.Dele<GameObject> onDown;
    public UIDele.Dele<GameObject> onUp;
    public UIDele.Dele<GameObject> onEnter;
    public UIDele.Dele<GameObject> onExit;

    public object[] parameter;
    private float Delay = 0.3f;
    private readonly float LONG_PRESS_TIME_ONCE = 0.35f;
    private readonly float LONG_PRESS_PARKOUR_TIME = 0.1f;
    private readonly float SPEED = 0.95f;
    private readonly float MAXTIME = 0.05f;

    private float iniTime = 0.15f;
    float dTime;

    private void Awake()
    {
        Toggle _curTog = gameObject.GetComponent<Toggle>();
        if (_curTog == null)
        {
            return;
        }
        
    }

    void OnDisable()
    {
        isMouseEntered = false;
        isMousePressing = false;
        isLongPressed = false;
        isPressDown = false;
        longPressMaxTime = 0;
        longPressDTime = 0;
    }

    bool IsLongPressEmpty()
    {
        return (OnLongPress == null) && (onHover == null);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        isPressDown = true;

        if (onDown != null)
        {
            onDown(CacheGameObject);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        if (onUp != null)
        {
            onUp(CacheGameObject);
        }
        isPressDown = false;
    }

    public void FixedUpdate()
    {
        if (isMouseEntered)
        {
            FixedUpdatePointer();
        }
        FixedUpdateLongPress();
    }

    #region 长按处理

    //长按开始
    void PrepareLongPress()
    {
        isLongPressed = false;
        longPressDTime = 0.0f;
        iniTime = 0.15f;
        longPressMaxTime = LONG_PRESS_TIME_ONCE;
    }

    void EndLongPress()
    {
        if (isLongPressed)
        {
            if(OnLongPressOnceEnd != null)
            {
                OnLongPressOnceEnd();
            }

            if (OnLongPress != null)
            {
                OnLongPress(CacheGameObject);
            }
        }
    }

    private void FixedUpdateLongPress()
    {
        if (!isMousePressing)
            return;

        //触发长按
        longPressDTime += Time.fixedDeltaTime;
        if(longPressDTime>=LONG_PRESS_TIME_ONCE && !isLongPressed)
        {
            isLongPressed = true;
            if (OnLongPressOnceEnter != null)
            {
                OnLongPressOnceEnter();
            }
        }

        //多次长按触发
        if (longPressDTime > longPressMaxTime)
        {
            longPressMaxTime += iniTime;
            if (iniTime > MAXTIME)
            {
                iniTime = iniTime * SPEED;
            }

            if (OnLongPress != null)
            {
                OnLongPress(CacheGameObject);
            }
        }

        //隔0.1秒后一直触发
        if(longPressDTime > LONG_PRESS_PARKOUR_TIME)
        {
            if(onHover != null)
            {
                onHover(CacheGameObject);
            }
        }
    }

    #endregion

    //鼠标进入
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0) && !isMousePressing)
        {
            isMousePressing = true;
            PrepareLongPress();
        }
        isMouseEntered = true;
        if(onEnter != null)
        {
            onEnter(CacheGameObject);
        }
    }

    void FixedUpdatePointer()
    {
        if(Input.GetMouseButton(0) && !isMousePressing)
        {
            isMousePressing = true;
            PrepareLongPress();
        }

        if (onExit != null)
        {
            onExit(CacheGameObject);
        }


    }

    //鼠标移除
    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseEntered = false;
        if (isMousePressing)
        {
            EndLongPress();
            isMousePressing = false;
        }
        if (onExit != null)
        {
            onExit(CacheGameObject);
        }
    }

    //点击事件
    public void OnPointerClick(PointerEventData eventData)
    {
        if(!IsLongPressEmpty() && isLongPressed)
        {
            return;
        }

        var btn = this.GetComponent<Selectable>();
        if(btn!=null && !btn.interactable)
        {
            return;
        }

        switch (AudioEnum)
        {
            case SoundAudio.None:
                break;
            case SoundAudio.UI_Click:
                break;
            default:
                Debug.LogError("没有对应点击音效");
                break;
        }

        if(LockedClick != null)
        {
            LockedClick();
            return;
        }

        if (UnlockClick != null)
        {
            UnlockClick();
        }

        if(onClick != null)
        {
            onClick(CacheGameObject);
        }

        
    }

    

    public void OnPointer(PointerEventData eventData)
    {
        if(onEnter != null)
        {
            onEnter(CacheGameObject);
        }
    }


    public static UIEventListener_UGUI Get(GameObject go,params object[] param)
    {
        UIEventListener_UGUI lister = go.GetComponent<UIEventListener_UGUI>();
        if(lister == null)
        {
            lister = go.AddComponent<UIEventListener_UGUI>();
            lister.parameter = param;
        }
        return lister;
    }


}
