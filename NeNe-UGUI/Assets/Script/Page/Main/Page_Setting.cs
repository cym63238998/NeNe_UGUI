using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;
namespace NeNe.LGL.Panel
{
    public class Page_Setting : BasePanel
    {
        CanvasGroup canvasGroup;
        private Button returnLogin;//返回登录界面按钮
        private Button returnButton;//返回按钮
        public override void AddListener()
        {
            canvasGroup.blocksRaycasts = true;
            returnButton.onClick.AddListener(Return);
            returnLogin.onClick.AddListener(ReturnLogin);
        }

        public override void OnEnter()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            returnButton = transform.Find("Return").GetComponent<Button>();
            returnLogin = transform.Find("returnLogin").GetComponent<Button>();
            AddListener();
        }

        public override void OnExit()
        {
            RemoveListener();
        }

        public override void OnPause()
        {
            RemoveListener();
        }

        public override void OnResume()
        {
            AddListener();
        }

        public override void RemoveListener()
        {
            canvasGroup.blocksRaycasts = false;
            returnButton.onClick.RemoveAllListeners();
            returnLogin.onClick.RemoveAllListeners();
        }

        public override void Return()
        {
            UIManager.Instance.PopPanel(UIFramework.Animation.leftToright, 0.4f);
        }
        /// <summary>
        /// 回到登录页面
        /// </summary>
        public void ReturnLogin()
        {
            Debug.Log("执行");
            PlayerPrefs.DeleteKey("information");
            UIManager.Instance.PopAllPanel();
            UIManager.Instance.PushPanel("Login_Main", UIFramework.Animation.noAnimation, 0.2f);
        }
    }
}
    
