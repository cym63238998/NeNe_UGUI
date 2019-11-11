using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SdkPhoto : MonoBehaviour
{
    public Button mOpenPhoto;
    public Image mImage;
    public Text mLog;

    private void Start()
    {
        mOpenPhoto.onClick.AddListener(OpenPhoto);
    }

    //接通Android
    private void OpenPhoto()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("OpenGallery");
    }

    //获取到图片的地址
    public void GetImagePath(string imagePath)
    {
        if (imagePath == null)
            return;
        mLog.text = imagePath;
        StartCoroutine("LoadImage", imagePath);
    }

    //加载纹理
    private IEnumerator LoadImage(string imagePath)
    {
        WWW www = new WWW("file://" + imagePath);
        mLog.text = mLog.text + "\n www开始加载";
        yield return www;
        mLog.text = mLog.text + "\n www加载完成";
        if (www.error == null)
            StartCoroutine("UpdataImage", www.texture);
        else
            mLog.text = mLog.text + "\n" + www.error;
    }

    //纹理转化为精灵
    private IEnumerator UpdataImage(Texture2D texture)
    {
        mLog.text = mLog.text + "\n 开始转化为精灵";
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        mImage.sprite = sprite;
        mLog.text = mLog.text + "\n 转换结束";
        yield return new WaitForSeconds(0.01f);
        Resources.UnloadUnusedAssets();
    }
}
