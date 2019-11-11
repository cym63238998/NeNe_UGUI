using NeNe.LGL.Data;
using NeNe.LGL.Net;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;

public class GameFacade : SingleTonMono<GameFacade>
{
    public Text text;
    public Transform background;
   
    private void Start()
    {
        //初始化数据
        if (PlayerPrefs.HasKey("information"))
        {
            Debug.Log("有key");
            UserInformation.Instance.Init(PlayerPrefs.GetString("information"));
            UIManager.Instance.PushPanel("Page_Main", UIFramework.Animation.noAnimation, 0.2f);
        }
        else
        {
            Debug.Log("没有key");
            UIManager.Instance.PushPanel("Login_Main", UIFramework.Animation.noAnimation, 0.2f);
        }

    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.Return();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            WWWForm www = new WWWForm();
            www.AddField("followerId",11);//粉丝用户
            www.AddField("followingId",12);//被关注用户
            HttpManager.Instance.Follow(www);
        }
    }

    private void OnApplicationQuit()
    {
        
    }
}
