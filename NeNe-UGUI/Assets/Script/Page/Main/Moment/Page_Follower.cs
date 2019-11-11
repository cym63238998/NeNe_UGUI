using NeNe.LGL.Data;
using NeNe.LGL.Net;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;
namespace NeNe.LGL.Panel
{
    public class Page_Follower : BasePanel
    {
        private FollowerScroll scroller;
        private CanvasGroup canvasGroup;
        private Button returnButton;//返回按钮

        public override void AddListener()
        {
            canvasGroup.blocksRaycasts = true;
            returnButton.onClick.AddListener(Return);
            LoadFollowerList();
        }

        public async void LoadFollowerList()
        {
            if (await HttpManager.Instance.GetFollowerList(UserInformation.Instance.id.ToString(), "0", "2"))
            {

                scroller.OnEnter();
                //加载数据
                for (int i = 0; i < DataManager.Instance.followerList.Count; i++)
                {
                    Debug.Log(DataManager.Instance.followerList.Count);
                    // GameFacade.Instance.text.text = "follower" + DataManager.Instance.followingList.Count;
                    scroller.AddNewRow(i);
                }
            }
            else
            {
                UIManager.Instance.SystemDialog("数据拉取失败，请重试或检查网络", 800);
            }
        }
        public override void OnEnter()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            scroller = GetComponent<FollowerScroll>();
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

