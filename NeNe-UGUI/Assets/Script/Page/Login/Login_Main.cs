using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace NeNe.LGL.Panel
{
    /// <summary>
    /// Login预制体
    /// </summary>
    public class Login_Main : BasePanel
    {
        CanvasGroup canvasGroup;
        private Button weiboButton;//微博按钮
        private Button qqButton;//QQ登录按钮
        private Button phoneButton;//手机登录按钮
        private Button weChatButton;//微信登录按钮
                                    /// <summary>
                                    /// 手机登录
                                    /// </summary>
        void PhoneLogin()
        {
            //入栈
            UIManager.Instance.PushPanel("Login_Phone", UIFramework.Animation.rightToleft, 0.2f);
        }

        /// <summary>
        /// 微博登录
        /// </summary>
        void WeiboLogin()
        {
            UIManager.Instance.SystemDialog("微博登录", 0.2f, () =>
            {
                Debug.Log("微博登录");
            });
            //入栈  
        }
        /// <summary>
        /// 手机登录
        /// </summary>
        void QQLogin()
        {
            UIManager.Instance.SystemDialog("QQ登录", 0.2f, () =>
              {
                  Debug.Log("QQ登录");
              });
            //入栈
            //19072122504389001
        }
        /// <summary>
        /// 微信登录
        /// </summary>
        void WeChatLogin()
        {
            UIManager.Instance.SystemDialog("微信登录", 0.2f, () =>
             {
                 Debug.Log("微信登录");
             });
            Debug.Log("微信登录");
            //入栈
        }

        public override void OnEnter()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            weiboButton = transform.Find("weiboLogin").GetComponent<Button>();
            weChatButton = transform.Find("WeChatLogin").GetComponent<Button>();
            qqButton = transform.Find("QQLogin").GetComponent<Button>();
            phoneButton = transform.Find("phoneLogin").GetComponent<Button>();
            AddListener();
        }
        public override void OnPause()
        {
            RemoveListener();
            Debug.Log("Main我暂停鼠标与面板的交互");

        }
        public override void OnResume()
        {
            AddListener();
            Debug.Log("Main我恢复鼠标与面板的交互");

        }

        public override void OnExit()
        {
            //失去焦点
            RemoveListener();
            //收入对象池
            Debug.Log("我退出了");
        }






        public override void AddListener()
        {
            weiboButton.onClick.AddListener(WeiboLogin);
            weChatButton.onClick.AddListener(WeChatLogin);
            qqButton.onClick.AddListener(QQLogin);
            phoneButton.onClick.AddListener(PhoneLogin);
            canvasGroup.blocksRaycasts = true;
        }

        public override void RemoveListener()
        {
            weiboButton.onClick.RemoveListener(WeiboLogin);
            weChatButton.onClick.RemoveListener(WeChatLogin);
            qqButton.onClick.RemoveListener(QQLogin);
            phoneButton.onClick.RemoveListener(PhoneLogin);
            canvasGroup.blocksRaycasts = false;
        }

        public override void Return()
        {
            Debug.Log("栈底元素不出栈直接退出APP");
        }
    }
}