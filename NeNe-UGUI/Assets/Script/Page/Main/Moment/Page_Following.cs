using NeNe.LGL.Data;
using NeNe.LGL.Net;
using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NeNe.LGL.Panel
{
    public class Page_Following : BasePanel
    {
        private UICircularScrollView scroller;
        private CanvasGroup canvasGroup;
        private Button returnButton;

        public override void AddListener()
        {
            canvasGroup.blocksRaycasts = true;
            returnButton.onClick.AddListener(Return);
            LoadFollowingList();
        }
        private void Update()
        {

        }

        public async void LoadFollowingList()
        {
            if (await HttpManager.Instance.GetFollowingList(UserInformation.Instance.id.ToString(), "0", "2"))
            {
                scroller.OnEnter();
                //加载数据
                for (int i = 0; i < DataManager.Instance.followingList.Count; i++)
                {

                    //  GameFacade.Instance.text.text = "following" + DataManager.Instance.followingList.Count;
                    scroller.AddNewRow(i);
                }
            }
            else
            {
                UIManager.Instance.SystemDialog("数据拉取失败，请重试或检查网络", 200);
            }
        }
        public override void OnEnter()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            scroller = GetComponent<UICircularScrollView>();
            returnButton = transform.Find("Return").GetComponent<Button>();
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
        }

        public override void Return()
        {
            HttpManager.Instance.GetPersonInformation(UserInformation.Instance.id.ToString());
            UIManager.Instance.PopPanel(UIFramework.Animation.leftToright, 0.2f);
        }
    }
}