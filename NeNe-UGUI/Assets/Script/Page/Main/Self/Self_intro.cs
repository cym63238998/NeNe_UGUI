using NeNe.LGL.Data;
using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;
namespace NeNe.LGL.Panel
{
    public class Self_intro : BasePanel
    {

        CanvasGroup canvasGroup;
        private InputField introInput;//个人介绍文本框
        private Button saveButton;//保存按钮
        private Button returnButton;//返回按钮
        public override void OnEnter()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            saveButton = transform.Find("Save").GetComponent<Button>();
            introInput = transform.Find("introInput").GetComponent<InputField>();
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

        public override void AddListener()
        {
            canvasGroup.blocksRaycasts = true;
            saveButton.onClick.AddListener(Save);
            returnButton.onClick.AddListener(Return);
            introInput.text = UserInformation.Instance.intro;
        }

        public override void RemoveListener()
        {
            canvasGroup.blocksRaycasts = false;
            saveButton.onClick.RemoveListener(Save);
            returnButton.onClick.RemoveListener(Return);
        }
        public void Save()
        {

            if (introInput.text != UserInformation.Instance.intro)
            {
                UserInformation.Instance.intro = introInput.text;//获取值 
                UserInformation.Instance.Change();
            }

            UIManager.Instance.SystemDialog("保存了", 500);
        }
        public override void Return()
        {
            UIManager.Instance.PopPanel(UIFramework.Animation.leftToright, 0.2f);
        }

        
    }

}