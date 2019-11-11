using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NeNe.LGL.Panel
{
    public class SystemDialogPanel : BasePanel
    {
        private Button CloseButton;

        private Text messageText;//文本框
        public Action func;//委托方法
        public string message;//消息
        CanvasGroup canvasgroup;
        public void Close()
        {
            Debug.Log("点击了");
            ObjectPool.GetInstance().RecycleObj(gameObject);//收进对象池
            try
            {
                func();
            }
            catch (Exception e)
            {

            }
            OnExit();
        }


        public override void OnEnter()
        {
            CloseButton = transform.Find("Close").GetComponent<Button>();
            messageText = transform.Find("DialogPanel").GetChild(0).GetComponent<Text>();
            messageText.text = message;
            canvasgroup = GetComponent<CanvasGroup>();
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
            CloseButton.onClick.RemoveListener(Close);
            canvasgroup.alpha = 0;
            canvasgroup.blocksRaycasts = false;
            canvasgroup.interactable = false;
        }

        public override void AddListener()
        {
            canvasgroup.alpha = 1;
            canvasgroup.blocksRaycasts = true;
            canvasgroup.interactable = true;
            CloseButton.onClick.AddListener(Close);
        }

        public override void Return()
        {
            throw new NotImplementedException();
        }
    }
}