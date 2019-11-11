using EnhancedUI.EnhancedScroller;
using NeNe.LGL.Data;
using NeNe.LGL.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;

public class FollowerCell : EnhancedScrollerCellView
{
    //这是一个示例
    public RawImage headIcon;//头像icon
    public Button followButton;//关注按钮
    public Text nameText;//名字文本
    public Text introText;//介绍文本
    public RawImage genderIcon;//性别图标
    private int isFollow;//是否关注了对方
    public int id;
    public virtual void SetData(Data data)
    {
        followButton.onClick.RemoveAllListeners();
        nameText.text = DataManager.Instance.followerList[data.id].name;
        id = DataManager.Instance.followerList[data.id].id;
        isFollow = DataManager.Instance.followerList[data.id].isFollowing;
        followButton.onClick.AddListener(Follow);
        Debug.Log(isFollow);
        Debug.Log(id);
        if (isFollow==0)
        {
            followButton.transform.GetChild(0).GetComponent<Text>().text = "关注"+isFollow;
        }
        else
        {
            followButton.transform.GetChild(0).GetComponent<Text>().text = "取消关注" + isFollow;
        }
    }

    /// <summary>
    /// 关注???
    /// </summary>
    public async void Follow()
    {
        //followerId: "4"(粉丝用户id）
        //followingId: "1"(被关注用户id)
        if (isFollow == 0)
        {
            WWWForm www = new WWWForm();
            www.AddField("followerId", UserInformation.Instance.id);
            www.AddField("followingId", id);
            if (await HttpManager.Instance.Follow(www))
            {
                Debug.Log("已成为他的粉丝");
                UIManager.Instance.SystemDialog("已成为他的粉丝",800);
                followButton.transform.GetChild(0).GetComponent<Text>().text = "取消关注";
                isFollow = 1;
                HttpManager.Instance.GetPersonInformation(UserInformation.Instance.id.ToString());
            }
            else
            {
                Debug.Log("已成为他的粉丝失败");
                UIManager.Instance.SystemDialog("关注失败", 800);
            }
        }
        else
        {
            WWWForm www = new WWWForm();
            www.AddField("followerId", UserInformation.Instance.id);//粉丝
            www.AddField("followingId", id);//关注
            if (await HttpManager.Instance.CancelFollow(www))
            {
                UIManager.Instance.SystemDialog("取消关注", 800);
                followButton.transform.GetChild(0).GetComponent<Text>().text = "关注";
                isFollow = 0;
                HttpManager.Instance.GetPersonInformation(UserInformation.Instance.id.ToString());
            }
            else
            {
                UIManager.Instance.SystemDialog("取消关注失败", 800);
            }
        }
    }
}
