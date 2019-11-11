using NeNe.LGL.Data;
using NeNe.LGL.Net;
using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;
namespace NeNe.LGL.Panel
{
    public class Page_Moments : BasePanel
    {
        private Button returnButton;//返回按钮
        private Button editorButton;//编辑按钮
        private Button followerButton;//粉丝按钮
        private Button followingButton;//关注按钮
        private Text nameText;
        private CanvasGroup canvasGroup;
        public override void Return()
        {
            UIManager.Instance.PopPanel(UIFramework.Animation.leftToright, 0.25f);
        }

        public override void AddListener()
        {
            returnButton.onClick.AddListener(Return);
            editorButton.onClick.AddListener(ShowSelfInformation);
            followerButton.onClick.AddListener(ShowFollowerList);
            followingButton.onClick.AddListener(ShowFollowingList);
            followingButton.GetComponent<Text>().text = "关注:" + UserInformation.Instance.followingCount.ToString();
            followerButton.GetComponent<Text>().text = "粉丝:" + UserInformation.Instance.followerCount.ToString();
            nameText.text = UserInformation.Instance.name;
            canvasGroup.blocksRaycasts = true;
        }

        public override void OnEnter()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            returnButton = transform.Find("Return").GetComponent<Button>();
            editorButton = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>();
            followerButton = transform.GetChild(0).GetChild(0).GetChild(0).Find("follower").GetComponent<Button>();
            followingButton = transform.GetChild(0).GetChild(0).GetChild(0).Find("following").GetComponent<Button>();
            nameText = transform.GetChild(0).GetChild(0).GetChild(0).Find("userNameText").GetComponent<Text>();
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
            editorButton.onClick.RemoveListener(ShowSelfInformation);
            returnButton.onClick.RemoveListener(Return);
            followerButton.onClick.RemoveAllListeners();
            followingButton.onClick.RemoveAllListeners();
        }

        /// <summary>
        /// 弹出个人信息栏
        /// </summary>
        public void ShowSelfInformation()
        {
            if (HttpManager.Instance.IsConnectNet())
            {
                UIManager.Instance.PushPanel("Page_Self", UIFramework.Animation.rightToleft, 0.2f);
            }
            else
            {
                UIManager.Instance.SystemDialog("您没有联网", 200);
            }
        }

        /// <summary>
        /// 粉丝列表
        /// </summary>
        public void ShowFollowerList()
        {
            UIManager.Instance.PushPanel("Page_Follower", UIFramework.Animation.rightToleft, 0.2f);
        }
        /// <summary>
        /// 关注列表
        /// </summary>
        public void ShowFollowingList()
        {
            UIManager.Instance.PushPanel("Page_Following", UIFramework.Animation.rightToleft, 0.2f);
        }
    }
}