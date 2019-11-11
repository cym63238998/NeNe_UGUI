using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photo : MonoBehaviour
{
    private AndroidJavaObject javaObject;
    //安卓调用相册sdk
    private AndroidJavaClass javaClass;


    /// <summary>
    /// 打开相册
    /// </summary>
    public void OpenGallery()
    {
        javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        javaObject = javaClass.Get<AndroidJavaObject>("currentActivity");
        javaObject.Call("OpenGallery");
    }
    /// <summary>
    /// 打开相机
    /// </summary>
    public void OpenCamera()
    {
        javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        javaObject = javaClass.Get<AndroidJavaObject>("currentActivity");
        javaObject.Call("TakePhoto");
    }

}
