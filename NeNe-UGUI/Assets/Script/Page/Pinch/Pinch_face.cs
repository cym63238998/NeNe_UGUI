using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;


namespace NeNe.LGL.Panel
{
    public class Pinch_face : BasePanel
    {
        CanvasGroup canvasGroup;
        private Slider faceSlider;
        private Button returnButton;
        public override void AddListener()
        {
            canvasGroup.blocksRaycasts = true;
            returnButton.onClick.AddListener(Return);
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
            returnButton.onClick.RemoveListener(Return);
        }

        public override void Return()
        {
            UIManager.Instance.PopPanel(UIFramework.Animation.leftToright, 0.3f);
        }

        /// <summary>
        /// 脸部标签方法 //默认脸型1 默认脸型2 默认脸型3
        /// </summary>
        public void ToggleFace(bool b)
        {

        }
    }
}
