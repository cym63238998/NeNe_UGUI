using NeNe.LGL.Data;
using NeNe.LGL.Net;
using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;
namespace NeNe.LGL.Panel
{
    public class Page_Main : BasePanel
    {
        CanvasGroup canvasGroup;
        private Button exitButton;//退出按钮 
        private Button headButton;//头像icon
        private Button setButton;//设置按钮
        private Button face;//捏脸页面按钮
        private Button clothes;//衣服
        private Button figure;//身体

        /// <summary>
        /// 朋友圈
        /// </summary>
        private void Moments()
        {
            UIManager.Instance.PushPanel("Page_Moments", UIFramework.Animation.rightToleft, 0.3f);
        }
        /// <summary>
        /// 返回
        /// </summary>
        public override void Return()
        {
            UIManager.Instance.PopAllPanel();
            UIManager.Instance.PushPanel("Login", UIFramework.Animation.noAnimation, 0.2f);
        }



        public override void OnEnter()
        {
            // UserInformation.Instance.Init(PlayerPrefs.GetString("information"));//初始化数据  
            headButton = transform.Find("3").GetChild(0).GetComponent<Button>();  //点击头像进入朋友圈
            canvasGroup = GetComponent<CanvasGroup>();
            setButton = transform.Find("3").Find("settingButton").GetComponent<Button>();
            face = transform.Find("3").Find("faceButton").GetComponent<Button>();//脸部按钮
            clothes = transform.Find("3").Find("clothesButton").GetComponent<Button>();//衣服按钮
            figure = transform.Find("3").Find("figureButton").GetComponent<Button>();//身材按钮
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
            headButton.onClick.RemoveListener(Moments);
            setButton.onClick.RemoveAllListeners();
            face.onClick.RemoveAllListeners();
            clothes.onClick.RemoveAllListeners();
            figure.onClick.RemoveAllListeners();
        }

        /// <summary>
        /// 展示设置页面
        /// </summary>
        public void ShowSetting()
        {
            UIManager.Instance.PushPanel("Page_Setting", UIFramework.Animation.rightToleft, 0.3f);
        }
        public override void AddListener()
        {
            headButton.onClick.AddListener(Moments);
            setButton.onClick.AddListener(ShowSetting);
            face.onClick.AddListener(() => PushNextPage("Pinch_face"));
            clothes.onClick.AddListener(() => UIManager.Instance.SystemDialog("服装页面开发中", 800));
            figure.onClick.AddListener(() => UIManager.Instance.SystemDialog("身材页面开发中", 800));
            canvasGroup.blocksRaycasts = true;
            HttpManager.Instance.GetPersonInformation(UserInformation.Instance.id.ToString());
        }

        public void PushNextPage(string name)
        {
            UIManager.Instance.PushPanel(name, UIFramework.Animation.rightToleft, 0.3f);
        }

        public override void Test()
        {
            base.Test();
        }

    }
}