using NeNe.LGL.Data;
using NeNe.LGL.Net;
using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;
namespace NeNe.LGL.Panel
{
    public class Page_Self : BasePanel
    {
        private Button name;
        private Button gender;
        private Button birthday;
        private Button intro;
        private Button email;
        private Button avatar;
        private Button area;
        private Button streamCount;
        private Button createTime;
        private Button updateTime;
        private Button returnButton;
        CanvasGroup canvasGroup;

        public override void OnEnter()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            name = transform.GetChild(1).GetChild(0).Find("name").GetComponent<Button>();
            gender = transform.GetChild(1).GetChild(0).Find("gender").GetComponent<Button>();
            birthday = transform.GetChild(1).GetChild(0).Find("birthday").GetComponent<Button>();
            intro = transform.GetChild(1).GetChild(0).Find("intro").GetComponent<Button>();
            email = transform.GetChild(1).GetChild(0).Find("email").GetComponent<Button>();
            avatar = transform.GetChild(1).GetChild(0).Find("avatar").GetComponent<Button>();
            area = transform.GetChild(1).GetChild(0).Find("area").GetComponent<Button>();
            streamCount = transform.GetChild(1).GetChild(0).Find("production").GetComponent<Button>();
            createTime = transform.GetChild(1).GetChild(0).Find("createTime").GetComponent<Button>();
            updateTime = transform.GetChild(1).GetChild(0).Find("updateTime").GetComponent<Button>();
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
            name.GetComponent<Button>().onClick.RemoveAllListeners();
            gender.GetComponent<Button>().onClick.RemoveAllListeners();
            birthday.GetComponent<Button>().onClick.RemoveAllListeners();
            intro.GetComponent<Button>().onClick.RemoveAllListeners();
            area.GetComponent<Button>().onClick.RemoveAllListeners();
            returnButton.onClick.RemoveListener(Return);
        }



        public override void AddListener()
        {
            Init();
            canvasGroup.blocksRaycasts = true;
            name.GetComponent<Button>().onClick.AddListener(() => ChangeAnything("Self_Name"));
            gender.GetComponent<Button>().onClick.AddListener(() => ChangeAnything("Self_gender"));
            birthday.GetComponent<Button>().onClick.AddListener(() => ChangeAnything("Self_birthday"));
            intro.GetComponent<Button>().onClick.AddListener(() => ChangeAnything("Self_intro"));
            area.GetComponent<Button>().onClick.AddListener(() => ChangeAnything("Self_area"));
            returnButton.onClick.AddListener(Return);
            HttpManager.Instance.GetPersonInformation(UserInformation.Instance.id.ToString()); //获取个人信息
        }

        /// <summary>
        /// 可能更改任何
        /// </summary>
        public void ChangeAnything(string anything)
        {
            UIManager.Instance.PushPanel(anything, UIFramework.Animation.rightToleft, 0.2f);
        }

        public override void Return()
        {
            //userId: "4"(用户id）
            //name: "poliqqq"(用户名）
            //gender: "2"(性别 0 默认 1 女性 2 男性）
            //birthday: "2019/01/01(出生日期）
            //intro: "hhhh"(个人简介）
            //avatar: "www.poliq.net"（个人头像）
            //area: "beijing"(地区)

            UIManager.Instance.PopPanel(UIFramework.Animation.leftToright, 0.2f);


            //
        }
        bool isChange = false;
        void Init()
        {
            Debug.Log("初始化");
            if (name.transform.GetChild(0).GetComponent<Text>().text != UserInformation.Instance.name)
            {
                isChange = true;
                name.transform.GetChild(0).GetComponent<Text>().text = UserInformation.Instance.name;
            }


            switch (UserInformation.Instance.gender)
            {
                default:
                    gender.transform.GetChild(0).GetComponent<Text>().text = "还未备注性别";
                    break;
                case 1:
                    gender.transform.GetChild(0).GetComponent<Text>().text = "女";
                    break;
                case 2:
                    gender.transform.GetChild(0).GetComponent<Text>().text = "男";
                    break;
            }

            birthday.transform.GetChild(0).GetComponent<Text>().text = UserInformation.Instance.birthday;
            intro.transform.GetChild(0).GetComponent<Text>().text = UserInformation.Instance.intro;
            avatar.transform.GetChild(0).GetComponent<Text>().text = UserInformation.Instance.avatar;
            area.transform.GetChild(0).GetComponent<Text>().text = UserInformation.Instance.area;
            streamCount.transform.GetChild(0).GetComponent<Text>().text = UserInformation.Instance.streamCount;
            createTime.transform.GetChild(0).GetComponent<Text>().text = UserInformation.Instance.createTime;
            updateTime.transform.GetChild(0).GetComponent<Text>().text = UserInformation.Instance.updateTime;
        }
    }
}