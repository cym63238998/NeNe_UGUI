using NeNe.LGL.Data;
using NeNe.LGL.Net;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace NeNe.LGL.Panel
{
    public class Login_Code : BasePanel
    {
        InputField inputCodeText;//验证码输入框
        private float number = 0;
        private bool isNeedTime = false;
        private Text timeText;

        CanvasGroup canvasgroup;
        private Button returnButton;//返回按钮
        private Button loginButton;//登录按钮
        private Button resetButton;//重新发验证码
        private void Update()
        {
            if (number < 60 && isNeedTime == true)
            {
                number += Time.deltaTime;
                timeText.text = "重新发送" + (int)(60 - number);
            }
            if (number > 60)
            {
                timeText.text = "重新发送";
                isNeedTime = false;
            }
        }

        public async void Send()
        {
            if (isNeedTime == false)
            {
                number = 0;
                Debug.Log("可以重发验证码");
                WWWForm www = new WWWForm();
                www.AddField("phoneNumber", UserInformation.Instance.phoneNumber);
                isNeedTime = true;
                bool b = await HttpManager.Instance.VerificationCode(www);//无需等待 二次验证码验证
                if (b)
                {

                    UIManager.Instance.SystemDialog("重发验证码成功" + "", 500);
                }
                else
                {
                    UIManager.Instance.SystemDialog("重发验证码失败", 500);
                }
            }
        }

        public override void Return()
        {
            UIManager.Instance.PopPanel(UIFramework.Animation.leftToright, 0.2f);
        }

        /// 检测输入到4位
        /// </summary>
        /// <param name="value"></param>
        async void CodeInput(string value)
        {

            if (HttpManager.Instance.IsConnectNet() == false)
            {
                UIManager.Instance.SystemDialog("您没有联网", 500);
                return;
            }

            if (value.Length == 4)
            {
                Debug.Log("发送验证码请求");
                WWWForm www = new WWWForm();
                www.AddField("phoneNumber", UserInformation.Instance.phoneNumber);
                www.AddField("code", inputCodeText.text);
                bool b = await HttpManager.Instance.Login(www);
                if (b)
                {
                    inputCodeText.DeactivateInputField();
                    canvasgroup.blocksRaycasts = false;
                    UIManager.Instance.PopAllPanel();
                    UIManager.Instance.PushPanel("Page_Main", UIFramework.Animation.rightToleft, 0.2f);

                    UIManager.Instance.SystemDialog("登陆成功", 500);
                }
                else
                {
                    Debug.Log(b);
                }
            }
        }
        /// <summary>
        /// 登录
        /// </summary>
        async void Login()
        {
            if (HttpManager.Instance.IsConnectNet() == false)
            {
                UIManager.Instance.SystemDialog("您没有联网", 500);
                return;
            }

            if (inputCodeText.text.Length == 4)
            {
                Debug.Log("长度够");
                WWWForm www = new WWWForm();
                www.AddField("phoneNumber", UserInformation.Instance.phoneNumber);
                www.AddField("code", inputCodeText.text);
                bool result = await HttpManager.Instance.Login(www);
                if (result)
                {
                    canvasgroup.blocksRaycasts = false;
                    UIManager.Instance.PopAllPanel();
                    inputCodeText.text = null;
                    UIManager.Instance.PushPanel("Page_Main", UIFramework.Animation.rightToleft, 0.2f);
                }
            }
            else
            {
                UIManager.Instance.SystemDialog("验证码长度不够", 500);
            }
        }

        public override void OnEnter()
        {
            if (HttpManager.Instance.IsConnectNet() == false)
            {
                UIManager.Instance.SystemDialog("您没有联网", 500);
                return;
            }

            returnButton = transform.Find("Return").GetComponent<Button>();//返回按钮
            loginButton = transform.Find("LoginButton").GetComponent<Button>();//登录按钮
            resetButton = transform.Find("CodeButton").GetComponent<Button>();//长发验证码按钮
            canvasgroup = GetComponent<CanvasGroup>();//不解释
            inputCodeText = transform.Find("InputCode").GetComponent<InputField>();//输入框
            timeText = transform.Find("CodeButton").GetComponent<Text>();//倒计时文本
            AddListener();
        }

        public override void OnPause()
        {
            RemoveListener();
        }

        public override void OnResume()
        {
            AddListener();
        }

        public override void OnExit()
        {
            RemoveListener();
        }



        public override void AddListener()
        {
            returnButton.onClick.AddListener(Return);
            loginButton.onClick.AddListener(Login);
            resetButton.onClick.AddListener(Send);
            inputCodeText.onValueChanged.AddListener(CodeInput);
            canvasgroup.blocksRaycasts = true;
        }

        public override void RemoveListener()
        {
            returnButton.onClick.RemoveAllListeners();
            loginButton.onClick.RemoveAllListeners();
            resetButton.onClick.RemoveAllListeners();
            canvasgroup.blocksRaycasts = false;
        }
    }
}
