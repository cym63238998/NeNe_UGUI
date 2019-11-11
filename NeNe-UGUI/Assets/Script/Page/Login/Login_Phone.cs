using NeNe.LGL.Data;
using NeNe.LGL.Net;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace NeNe.LGL.Panel
{
    /// <summary>
    /// 手机登录
    /// </summary>
    public class Login_Phone : BasePanel
    {
        Button userButton;//用户必读
        Button user_hideButton;//用户隐私

        Button loginButton;//请求验证码登录
        Button returnButton;//返回登录
        CanvasGroup canvasgroup;
        InputField inputNumber;//输入框

        public override void OnEnter()
        {
            canvasgroup = GetComponent<CanvasGroup>();
            returnButton = transform.Find("Return").GetComponent<Button>();
            loginButton = transform.Find("LoginButton").GetComponent<Button>();
            inputNumber = transform.Find("InputPhone").GetComponent<InputField>();
            userButton = transform.Find("User_Button").GetComponent<Button>();
            user_hideButton = transform.Find("UserHide_Button").GetComponent<Button>();
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
            loginButton.onClick.AddListener(GetCode);
            userButton.onClick.AddListener(() =>
            {
                UIManager.Instance.PushPanel("Page_User", UIFramework.Animation.rightToleft, 0.2f);
            });
            user_hideButton.onClick.AddListener(() =>
            {
                UIManager.Instance.PushPanel("Page_UserHide", UIFramework.Animation.rightToleft, 0.2f);
            });
            canvasgroup.blocksRaycasts = true;
        }

        public override void RemoveListener()
        {
            returnButton.onClick.RemoveListener(Return);
            loginButton.onClick.RemoveListener(GetCode);
            userButton.onClick.RemoveAllListeners();
            user_hideButton.onClick.RemoveAllListeners();
            canvasgroup.blocksRaycasts = false;
        }
        /// <summary>
        /// 返回
        /// </summary>
        public override void Return()
        {
            UIManager.Instance.PopPanel(UIFramework.Animation.leftToright, 0.2f);
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        async void GetCode()
        {
            if (HttpManager.Instance.IsConnectNet() == false)
            {
                UIManager.Instance.SystemDialog("您没有联网", 500);
                return;
            }
            if (inputNumber.text.Length == 11)
            {

                UserInformation.Instance.phoneNumber = inputNumber.text;
                WWWForm www = new WWWForm();
                www.AddField("phoneNumber", inputNumber.text);
                bool b = await HttpManager.Instance.VerificationCode(www);
                if (b == true)
                {   //登陆成功
                    inputNumber.text = "";
                    UIManager.Instance.PushPanel("Login_Code", UIFramework.Animation.rightToleft, 0.2f);

                }

            }
            else
            {
                UIManager.Instance.SystemDialog("手机号长度不够", 500);
            }

        }
    }
}