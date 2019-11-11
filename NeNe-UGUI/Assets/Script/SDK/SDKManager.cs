using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SDKManager : SingleTonMono<SDKManager>
{
    public Text text;//用来显示界面的文本
    public Text text1;
    public Text text2;
    private Button btn;//前端的按钮
    
    /// <summary>
    /// 打开相册
    /// </summary>
    public void OpenPhoto()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("OpenGallery");
    }  

    public void Toast(string message)
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("Toast",message);
    }

    /// <summary>
    /// 打开箱机
    /// </summary>
    public void OpenCamera()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("takephoto");
    }


    public void GetImagePath(string imagePath)
    {
        if (imagePath == null)
        {
            text.text = "图片路径不对";
            return;
        }
        text.text = "图片进入Unity" + imagePath;
        StartCoroutine("LoadImage",imagePath);
    }

    public void GetTakeImagePath(string imagePath)
    {
        if (imagePath == null)
            return;
        StartCoroutine("LoadImage", imagePath);
    }

    private IEnumerator LoadImage(string imagePath) 
    {
        text1.text = "携程开启";
        WWW www = new WWW("file://"+imagePath);
        yield return www;
        if (www.error==null)
        {
            try
            {
                Debug.Log(www.texture.name);
            }
            catch (Exception e) { }

            text.text = "成功了";

        }
        else
        {
            text.text = ("www.errot"+www.error);
        }
        text2.text = "2  " + www.ToString();
    }
}
