using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// UI全局控制
/// </summary>
namespace UIFramework
{
    public enum UIState {

        animation//动画状态禁用焦点 tp:切换页面 等待
    }


    public class UIController : SingleTonMono<UIController>
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape)) // 返回键
            {
                //出栈以及判断栈
            }
        }
    }
}
