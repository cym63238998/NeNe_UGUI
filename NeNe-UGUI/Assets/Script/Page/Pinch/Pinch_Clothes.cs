
using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace NeNe.LGL.Panel
{
    public class Pinch_Clothes : BasePanel
    {
        private Button returnButton;
        private CanvasGroup canvasGroup;
        private ToggleScroll scroll;
        private ToggleScroll scrollsenior;
        public override void AddListener()
        {
            
        }
        private void Start()
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>() ?? transform.GetComponent<CanvasGroup>();
            scroll = transform.GetChild(0).GetChild(0).GetComponent<ToggleScroll>();
            for (int i=0;i<100 ;i++ )
            {
                scroll.AddNewRow(i);
            }
        }

        public override void OnEnter()
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>() ?? transform.GetComponent<CanvasGroup>();

        }

        public override void OnExit()
        {

        }

        public override void OnPause()
        {
            
        }

        public override void OnResume()
        {
            
        }

        public override void RemoveListener()
        {
            
        }

        public override void Return()
        {
            UIManager.Instance.PopPanel(UIFramework.Animation.rightToleft,0.3f);
        }


        /// <summary>
        /// 滑动控制器
        /// </summary>
        public void SliderControl(float value)
        {

        }

    }
}
