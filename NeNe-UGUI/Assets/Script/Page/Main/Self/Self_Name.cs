using NeNe.LGL.Data;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;
namespace NeNe.LGL.Panel
{
    public class Self_Name : BasePanel
    {
        private Button returnButton;//返回按钮
        private Button saveButton;//保存按钮
        private InputField nameInput;//名字输入
        CanvasGroup canvasGroup;//不解释
        public override void OnEnter()
        {
            returnButton = transform.Find("Return").GetComponent<Button>();
            saveButton = transform.Find("Save").GetComponent<Button>();
            nameInput = transform.Find("nameInput").GetComponent<InputField>();
            canvasGroup = GetComponent<CanvasGroup>();
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

        public override void AddListener()
        {
            returnButton.onClick.AddListener(Return);
            saveButton.onClick.AddListener(Save);
            canvasGroup.blocksRaycasts = true;
            nameInput.text = UserInformation.Instance.name;
        }

        public override void RemoveListener()
        {
            returnButton.onClick.RemoveListener(Return);
            saveButton.onClick.RemoveListener(Save);
            canvasGroup.blocksRaycasts = false;
        }


        /// <summary>
        /// 返回
        /// </summary>
        public override void Return()
        {
            Debug.Log("返回");
            UIManager.Instance.PopPanel(UIFramework.Animation.leftToright, 0.2f);
        }
        /// <summary>
        /// 保存功能
        /// </summary>
        public void Save()
        {
            if (UserInformation.Instance.name != nameInput.text)
            {
                UserInformation.Instance.name = nameInput.text;
                UIManager.Instance.SystemDialog("已保存", 500);
                UserInformation.Instance.Change();
            }
            else
            {
                UIManager.Instance.SystemDialog("已保存", 500);
            }
        }
    }
}