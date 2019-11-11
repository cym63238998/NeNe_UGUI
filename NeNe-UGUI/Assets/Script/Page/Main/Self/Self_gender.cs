using NeNe.LGL.Data;
using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace NeNe.LGL.Panel
{
    public class Self_gender : BasePanel
    {

        CanvasGroup canvasGroup;
        private int gender;//判断是否改变
        public override void AddListener()
        {
            gender = UserInformation.Instance.gender;
            if (UserInformation.Instance.gender == 1)
            {
                transform.GetChild(0).Find("man").GetComponent<Toggle>().isOn = false;
                transform.GetChild(0).Find("woman").GetComponent<Toggle>().isOn = true;
            }
            else if (UserInformation.Instance.gender == 2)
            {
                transform.GetChild(0).Find("man").GetComponent<Toggle>().isOn = true;
                transform.GetChild(0).Find("woman").GetComponent<Toggle>().isOn = false;
            }
            canvasGroup.blocksRaycasts = true;
            transform.GetChild(0).Find("man").GetComponent<Toggle>().onValueChanged.AddListener(ChangeGender);
            //btransform.Find("Return").GetComponent<Button>().onClick.AddListener(Return);
        }

        public override void OnEnter()
        {
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

        public override void RemoveListener()
        {
            canvasGroup.blocksRaycasts = false;
            transform.GetChild(0).Find("man").GetComponent<Toggle>().onValueChanged.RemoveListener(ChangeGender);
            //  transform.Find("Return").GetComponent<Button>().onClick.RemoveListener(Return);
        }

        public override void Return()
        {
            Debug.Log("返回");
            UIManager.Instance.PopPanel(UIFramework.Animation.leftToright, 0.2f);
        }

        /// <summary>
        /// 改变性别
        /// </summary>
        public void ChangeGender(bool b)
        {
            if (b)
            {
                UserInformation.Instance.gender = 2;//男
                if (UserInformation.Instance.gender != gender)
                    UserInformation.Instance.Change();
            }

            else
            {
                UserInformation.Instance.gender = 1;//女
                if (UserInformation.Instance.gender != gender)
                    UserInformation.Instance.Change();
            }
            Return();
        }
    }
}