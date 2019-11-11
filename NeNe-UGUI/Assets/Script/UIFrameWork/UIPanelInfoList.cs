using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIFramework
{
    [Serializable]
    public class UIPanelInfoList
    {
        public List<UIPanelInfo> panelInfoList;

        public UIPanelInfoList() { }
    }
    [Serializable]
    public class UIPanelInfo
    {
        public string panelType;
        public string path;

        public UIPanelInfo()
        {

        }
    }
    //19072122504389001
    public class UIPanelType
    {
        public const string Login = "Login";     
        public const string Task = "Task";
    }
}


