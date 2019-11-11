﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GallerySDKCallBack : MonoBehaviour
{
    public Action<Texture> SetRawImageActon;

    public void GetImagePath(string path)
    {
        StartCoroutine(GetImageByPath(path));
    }
    private IEnumerator GetImageByPath(string path)
    {
        yield return new WaitForSeconds(1);
        WWW www = new WWW("file://" + path);
        yield return www;
        if (www.error == null)
        {
            if (SetRawImageActon != null)
            {
                SetRawImageActon(www.texture);
            }
        }
        else
        {
            Debug.LogError("LoadImage>>>>www.error" + www.error);
        }
    }
}
