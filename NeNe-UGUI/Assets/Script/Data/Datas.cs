using System;

namespace NeNe.LGL.Data
{
    //token Json
    [Serializable]
    //个人信息Json
    public class PersonInformationResult
    {
        public int id;//用户ID
        public int role;//角色ID
        public string name;//姓名
        public int gender;//性别
        public string birthday;//生日
        public string intro;//介绍
        public string email;//邮箱
        public string avatar;//头像
        public string area;//地区
        public string streamCount;//动态数
        public int followerCount;//粉丝数
        public int followingCount;//关注数
        public string createTime;//创立时间
        public string updateTime;//更新时间
        public int isFollowing;//你是否也是他的粉丝 0没有 1已经是
    }

    /// <summary>
    /// 开发中Feed流
    /// </summary>
    public class Feed
    {
        public int id;
        public string[] pictureName;//图片名字
        public string say;//文本内容
        public int zan;//点赞数
    }

}