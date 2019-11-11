
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NeNe.LGL.Data
{
    [Serializable]
    public class DataManager : SingleTon<DataManager>
    {
        //粉丝列表
        public List<PersonInformationResult> followerList;
        //关注列表
        public List<PersonInformationResult> followingList;



        public void SetfollowerListJson(string jsonStr)
        {
            // 将Json中的数组用一个list包裹起来，变成一个Wrapper对象
            jsonStr = "{ \"list\": " + jsonStr + "}";
            Response<PersonInformationResult> userList = JsonUtility.FromJson<Response<PersonInformationResult>>(jsonStr);
            followerList = userList.list;

        }
        public void SetfollowingListJson(string jsonStr)
        {
            // 将Json中的数组用一个list包裹起来，变成一个Wrapper对象
            jsonStr = "{ \"list\": " + jsonStr + "}";
            Response<PersonInformationResult> userList = JsonUtility.FromJson<Response<PersonInformationResult>>(jsonStr);
            followingList = userList.list;

        }




        // Json解析为该对象
        public class Response<T>
        {
            public List<T> list;
        }

    }
    /// <summary>
    /// 存放用户信息类
    /// </summary>
    public class User
    {


    }
}

