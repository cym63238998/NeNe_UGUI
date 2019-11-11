using NeNe.LGL.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using UIFramework;
using UnityEngine;
using UnityEngine.Networking;

namespace NeNe.LGL.Net
{
    //从http header里面取错误信息
    //user-code : 错误码
    //user-msg：错误信息（decode 编码方式：UTF-8)
    public class HttpManager : SingleTon<HttpManager>, IHttpRequest
    {
        private const string user_code = "user-code";//header
        private const string user_msg = "user-msg";//header
        private const string user_token = "user-token";
        
        
        /// <summary>
        /// 修改个人信息
        /// </summary>
        public async Task<bool> ChangeInforMation(WWWForm www)
        {
            string url = @"http://test.poliq.net:8080/user/profile";
            string msg = null;
            string code = null;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(user_token, PlayerPrefs.GetString("TOKEN"));

            UnityWebRequest request = await HttpPost(url, www, dic);
            Debug.Log(request.responseCode);
            switch (request.responseCode)
            {
                case 200:
                    Debug.Log(request.downloadHandler.text);
                    return true;
                case 500:
                    msg = HttpUtility.UrlDecode(request.GetResponseHeader(user_msg), Encoding.UTF8);
                    code = request.GetResponseHeader(user_code);
                    UIManager.Instance.SystemDialog(msg + "   " + code, 500);
                    return false;
                default:
                    UIManager.Instance.SystemDialog("发生未知错误", 500);
                    return false;
            }
        }

        /// <summary>
        /// 获取粉丝列表 pamara为userid
        /// </summary>
        /// <param name="pamra"></param> 
        /// <returns></returns>
        public async Task<bool> GetFollowerList(string userid, string pageNo, string pageSize)
        {
            string url = @"http://test.poliq.net:8080/user/getFollowerList/" + userid + "?" + "pageNo=" + pageNo + "&" + "pageSize=" + pageSize;
            Debug.Log(url);
            string msg = null;
            string code = null;
            string token = null;
            UnityWebRequest request = await HttpGet(url);
            GameFacade.Instance.text.text = request.responseCode.ToString();
            Debug.Log(request.responseCode);
            switch (request.responseCode)
            {
                case 200:
                    Debug.Log(request.downloadHandler.text);
                    DataManager.Instance.SetfollowerListJson(request.downloadHandler.text);
                    GameFacade.Instance.text.text = DataManager.Instance.followerList.Count.ToString();
                    return true;
                case 500:
                    msg = HttpUtility.UrlDecode(request.GetResponseHeader(user_msg), Encoding.UTF8);
                    code = request.GetResponseHeader(user_code);
                    UIManager.Instance.SystemDialog(msg + "   " + code, 500);
                    return false;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 获取关注列表 id 页码pageNo pageSize:"2"
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public async Task<bool> GetFollowingList(string userid, string pageNo, string pageSize)
        {
            string url = @"http://test.poliq.net:8080/user/getFollowingList/" + userid + "?" + "pageNo=" + pageNo + "&" + "pageSize=" + pageSize;
            Debug.Log(url);
            string msg = null;
            string code = null;
            UnityWebRequest request = await HttpGet(url);
            Debug.Log(request.responseCode);
            GameFacade.Instance.text.text = request.responseCode.ToString();
            Debug.Log(request.responseCode);
            switch (request.responseCode)
            {
                case 200:
                    Debug.Log(request.downloadHandler.text);
                    DataManager.Instance.SetfollowingListJson(request.downloadHandler.text);
                    GameFacade.Instance.text.text = DataManager.Instance.followingList.Count.ToString();
                    return true;
                case 500:
                    msg = HttpUtility.UrlDecode(request.GetResponseHeader(user_msg), Encoding.UTF8);
                    code = request.GetResponseHeader(user_code);
                    UIManager.Instance.SystemDialog(msg + "   " + code, 500);
                    return false;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 请求登录
        /// </summary>
        /// <param name="req"></param>
        public async Task<bool> Login(WWWForm www)
        {
            string url = @"http://test.poliq.net:8080/login";
            string msg = null;
            string code = null;
            string token = null;
            UnityWebRequest request = await HttpPost(url, www);
            Debug.Log(request.responseCode);
            switch (request.responseCode)
            {
                case 200:
                    try
                    {
                        token = request.GetResponseHeader(user_token);
                        if (token != null)
                        {
                            Debug.Log("设置");
                            PlayerPrefs.SetString("information", request.downloadHandler.text);
                            UserInformation.Instance.Init(request.downloadHandler.text);
                            PlayerPrefs.SetString("TOKEN", token);
                        }
                    }
                    catch (Exception e) { Debug.Log("获取失败"); }
                    return true;
                case 500:
                    msg = HttpUtility.UrlDecode(request.GetResponseHeader(user_msg), Encoding.UTF8);
                    code = request.GetResponseHeader(user_code);
                    UIManager.Instance.SystemDialog(msg + "   " + code, 500);
                    return false;
                default:
                    UIManager.Instance.SystemDialog("发生未知错误,请检查网络或重试", 500);
                    return false;
            }


        }

        /// <summary>
        /// 请求验证码
        /// </summary>
        /// <param name="req"></param>
        /// <param name="buildContext"></param>
        public async Task<bool> VerificationCode(WWWForm www)
        {
            string url = @"http://test.poliq.net:8080/sendLoginSms";
            string msg = null;
            string code = null;
            
            UnityWebRequest request = await HttpPost(url, www);
            Debug.Log(request.responseCode);
            switch (request.responseCode)
            {
                case 200:
                    Debug.Log(request.downloadHandler.text);
                    return true;
                case 500:
                    msg = HttpUtility.UrlDecode(request.GetResponseHeader(user_msg), Encoding.UTF8);
                    code = request.GetResponseHeader(user_code);
                    UIManager.Instance.SystemDialog(msg + "   " + code, 500);
                    return false;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetPersonInformation(string id)
        {
            string url = @"http://test.poliq.net:8080/user/" + id;
            string msg = null;
            string code = null;
            UnityWebRequest request = await HttpGet(url);
            Debug.Log(request.responseCode);
            switch (request.responseCode)
            {
                case 200:
                    Debug.Log(request.downloadHandler.text);
                    UserInformation.Instance.Init(request.downloadHandler.text);
                    return true;
                case 500:
                    msg = HttpUtility.UrlDecode(request.GetResponseHeader(user_msg), Encoding.UTF8);
                    code = request.GetResponseHeader(user_code);
                    UIManager.Instance.SystemDialog(msg + "   " + code, 500);
                    return false;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 关注请求
        /// </summary>
        /// <param name="www"></param>
        /// <returns></returns>
        public async Task<bool> Follow(WWWForm www)
        {
            string url = @"http://test.poliq.net:8080/user/doFollowing";
            string msg = null;
            string code = null;
            UnityWebRequest request = await HttpPost(url, www);
            switch (request.responseCode)
            {
                case 200:
                    return true;
                case 500:
                    msg = HttpUtility.UrlDecode(request.GetResponseHeader(user_msg), Encoding.UTF8);
                    code = request.GetResponseHeader(user_code);
                    UIManager.Instance.SystemDialog(msg + "   " + code, 500);
                    return false;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 取消关注
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CancelFollow(WWWForm www)
        {
            string url = @"http://test.poliq.net:8080/user/undoFollowing";
            string msg = null;
            string code = null;
            UnityWebRequest request = await HttpPost(url, www);
            Debug.Log("responseCode:" + request.responseCode);
            switch (request.responseCode)
            {
                case 200:
                    Debug.Log(request.downloadHandler.text);
                    return true;
                case 500:
                    try
                    {
                        Debug.Log(500);
                        msg = HttpUtility.UrlDecode(request.GetResponseHeader(user_msg), Encoding.UTF8);
                        code = request.GetResponseHeader(user_code);
                        UIManager.Instance.SystemDialog(msg + "   " + code, 500);
                    }
                    catch (Exception e)
                    {
                        Debug.Log("异常");
                    }
                    return false;
                default:
                    UIManager.Instance.SystemDialog("未知错误", 500);
                    return false;
            }

        }


        public async Task<UnityWebRequest> HttpPost(string url, WWWForm www, Dictionary<string, string> header = null)
        {
            UnityWebRequest req = UnityWebRequest.Post(url, www);
            if (header!=null)
            {
                foreach (KeyValuePair<string, string> keyValuePair in header)
                {
                    Debug.Log(keyValuePair.Key + keyValuePair.Value);
                    req.SetRequestHeader(keyValuePair.Key, keyValuePair.Value);
                }
            }
            UnityWebRequestAsyncOperation result = (UnityWebRequestAsyncOperation)await req.SendWebRequest();
            return req;
        }
       
        
        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<UnityWebRequest> HttpGet(string url)
        {
            UnityWebRequest req = UnityWebRequest.Get(url);
            UnityWebRequestAsyncOperation result = (UnityWebRequestAsyncOperation)await req.SendWebRequest();
            return req;
        }



        /// <summary>
        /// 判断是否有网
        /// </summary>
        /// <returns></returns>
        public bool IsConnectNet()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                //  Debug.Log("没有联网！！！");
                return false;
            }
            if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
            {
                //  Debug.Log("使用Wi-Fi！！！");
                return true;
            }
            if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
            {
                //  Debug.Log("使用移动网络！！！");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

