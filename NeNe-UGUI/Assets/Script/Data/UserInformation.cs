using NeNe.LGL.Net;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeNe.LGL.Data
{
    /// <summary>
    /// 保存数据类 在内存中
    /// </summary>
    public class UserInformation : SingleTon<UserInformation>
    {
        public string phoneNumber;//手机号
        public int id;
        public int role;
        public string name;
        public int gender;
        public string birthday;
        public string intro;
        public string email;
        public string avatar;
        public string area;
        public string streamCount;
        public int followerCount;
        public int followingCount;
        public string createTime;
        public string updateTime;
        public int isLogin;//0代表未登录 1代表登录
        public bool isChange = false;
        /// <summary>
        /// 初始化数据
        /// </summary> 
        public void Init(string json)
        {
            PersonInformationResult pr = new PersonInformationResult();
            pr = JsonUtility.FromJson<PersonInformationResult>(json);
            // pr = JsonUtility.FromJson<PersonInformationResult>(PlayerPrefs.GetString("information"));

            id = pr.id;
            role = pr.role;
            name = pr.name;
            gender = pr.gender;
            birthday = pr.birthday.Substring(0, 10).Replace('-', '/');//生日需要切割
            intro = pr.intro;
            email = pr.email;
            avatar = pr.avatar;
            area = pr.area;
            streamCount = pr.streamCount;
            followerCount = pr.followerCount;
            followingCount = pr.followingCount;
            createTime = pr.createTime;
            updateTime = pr.updateTime;

            //11是北京 
            //12是双鸭山卡
        }

        public void Change()
        {
            // 测试中保存数据
            WWWForm www = new WWWForm();
            www.AddField("id", UserInformation.Instance.id);
            www.AddField("name", UserInformation.Instance.name);
            Debug.Log(name);
            www.AddField("gender", UserInformation.Instance.gender);
            www.AddField("birthday", UserInformation.Instance.birthday);
            Debug.Log(birthday);
            www.AddField("intro", UserInformation.Instance.intro);
            Debug.Log(intro);
            www.AddField("avatar", "666");
            www.AddField("area", UserInformation.Instance.area);
            HttpManager.Instance.ChangeInforMation(www);//保存无需等待结果 客户端做好选择标签
        }
    }

    //token Json
    [SerializeField]
    public class PersonToken
    {
        public int id;
    }
}

