// ========================================================
// Author：ChenGaoshuang 
// CreateTime：2020/04/25 10:19:40
// FileName：Assets/Scripts/UIExt/GeneratedUIScripts/LoginWindowRef/LoginWindowRef.cs
// ========================================================



//尝试一下！！

using UnityEngine;
using UnityEngine.UI;

namespace LoginWindow
{
    public class LoginWindowRef : UGUIRefNode
    {
        public UnityEngine.UI.Image Img_Backgroud;
        public UnityEngine.UI.Button Button_Start;
        public UIDele.Dele OnStartCallback;



        void Awake()
        {
            UIEventListener_UGUI.Get(Button_Start.gameObject).onClick = OnStartClick;
        }

        void Reset()
        {
            Img_Backgroud = transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
            Button_Start = transform.GetChild(1).GetComponent<UnityEngine.UI.Button>();
        }

        private void OnStartClick(GameObject GameObject)
        {
            if (OnStartCallback != null)
                OnStartCallback();
        }
    }
}
