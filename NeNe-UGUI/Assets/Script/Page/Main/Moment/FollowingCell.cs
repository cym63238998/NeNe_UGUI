using EnhancedUI.EnhancedScroller;
using NeNe.LGL.Data;
using NeNe.LGL.Net;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;


//CellView

public class FollowingCell : EnhancedScrollerCellView
{
    //就是说每个c
    public RawImage headIcon;//头像icon
    public Button followButton;//关注按钮
    public Text nameText;//名字文本
    public Text introText;//介绍文本
    public RawImage genderIcon;//性别图标
    private int followed;//0代表未关注  1代表关注
    private int id;
    public virtual void SetData(Data data)
    {
        followButton.onClick.RemoveAllListeners();
        nameText.text = DataManager.Instance.followingList[data.id].name;
        introText.text = DataManager.Instance.followingList[data.id].intro;
        id = DataManager.Instance.followingList[data.id].id;
        followed = DataManager.Instance.followingList[data.id].isFollowing;
        followButton.onClick.AddListener(CancelFollow);
        Debug.Log(followed);
        if (followed == 0) 
            followButton.transform.GetChild(0).GetComponent<Text>().text = "关注";
        else
            followButton.transform.GetChild(0).GetComponent<Text>().text = "取消关注";
    }


    /// <summary>
    /// 取消关注
    /// </summary>
    public async void CancelFollow()
    {
        //followerId: "4"(粉丝用户id）
        //followingId: "1"(被关注用户id)
        if (followed == 0)
        {
            WWWForm www = new WWWForm();
            www.AddField("followerId", UserInformation.Instance.id);
            www.AddField("followingId", id);
            if (await HttpManager.Instance.Follow(www))
            {
                Debug.Log("已成为他的粉丝");
                UIManager.Instance.SystemDialog("关注成功",800);
                followButton.transform.GetChild(0).GetComponent<Text>().text = "取消关注";
                followed = 1;
                HttpManager.Instance.GetPersonInformation(UserInformation.Instance.id.ToString());
            }
            else
            {
                UIManager.Instance.SystemDialog("关注失败", 800);
                Debug.Log("已成为他的粉丝失败");
            }
        }
        else
        {
            WWWForm www = new WWWForm();
            www.AddField("followerId", UserInformation.Instance.id);
            www.AddField("followingId", id);
            if (await HttpManager.Instance.CancelFollow(www))
            {
                UIManager.Instance.SystemDialog("已经取消关注", 800);
                followButton.transform.GetChild(0).GetComponent<Text>().text = "关注";
                followed = 0;
                HttpManager.Instance.GetPersonInformation(UserInformation.Instance.id.ToString());
            }
            else
            {
                UIManager.Instance.SystemDialog("取消关注失败", 800);
            }
        }
      
    }
}
