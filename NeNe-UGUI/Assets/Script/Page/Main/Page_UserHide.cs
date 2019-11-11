using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace NeNe.LGL.Panel
{
    public class Page_UserHide : BasePanel
    {
        CanvasGroup canvasGroup;
        public override void AddListener()
        {
            returnButton.onClick.AddListener(Return);
            canvasGroup.blocksRaycasts = true;
        }

        public override void OnEnter()
        {
            canvasGroup = GetComponent<CanvasGroup>();
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
        private Button returnButton;
        public override void Return()
        {

            UIManager.Instance.PopPanel(UIFramework.Animation.leftToright, 0.25f);
        }
    }
}