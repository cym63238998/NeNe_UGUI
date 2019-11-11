using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using NeNe.LGL.Panel;

namespace UIFramework
{
    //UI适配 720×1280），1080p（1080×1920）
    //720*1560 小米CC9e
    //正常屏幕略高分辨率 OPPO Reno 2340*1080  小米9 1080×2340    红米K20Pro 1080x2340
    //水滴屏  华为P30 2340x1080像素 小米CC9
    //刘海屏  华为P20 2244x1080  小米8 2248x1080像素
    public class UIManager : SingleTon<UIManager>//单例模式
    {
        private Transform canvasTransform;
        private Transform canvasTransform2;
        public Transform CanvasTransform
        {
            get
            {
                if (canvasTransform == null)
                {
                    //如果为第一次或者对象长时间不使用被回收掉重新赋值
                    return canvasTransform = GameObject.Find("Canvas").transform;
                }
                return canvasTransform;
            }
        }
        public Transform CanvasTransform2
        {
            get
            {
                if (canvasTransform2 == null)
                {
                    //如果为第一次或者对象长时间不使用被回收掉重新赋值
                    return canvasTransform2 = GameObject.Find("Canvas2").transform;
                }
                return canvasTransform2;
            }
        }//永不被遮挡的Cavnvas


        /// <summary>
        /// 构造初始化JSON
        /// </summary>
        public UIManager()
        {
            ParseUIPanelTypeJson();
        }
        //存储UI路径 类型
        private Dictionary<string, string> panelPathDic;

        //加载面板Json
        private void ParseUIPanelTypeJson()
        {
            panelPathDic = new Dictionary<string, string>();
            TextAsset textUIPanelType = Resources.Load<TextAsset>("UIPanelTypeJson");//Json解析     
            //序列化
            UIPanelInfoList panelInfoList = JsonUtility.FromJson<UIPanelInfoList>(textUIPanelType.text);
            foreach (var temp in panelInfoList.panelInfoList)
            {
                panelPathDic.Add(temp.panelType, temp.path);//路径和type放入字典
                //Debug.Log(temp.panelType);
            }
        }
        private Stack<BasePanel> panelStack;//栈存储部分 控制UI的 出入栈                                     
        private Dictionary<string, BasePanel> panelDic;  //存储panel信息

        /// <summary>
        /// 得到Panel
        /// </summary>
        public BasePanel GetPanel(string paneltype)
        {
            //如果==null
            if (panelDic == null)
            {
                panelDic = new Dictionary<string, BasePanel>();
            }
            BasePanel panel = null;//根据key找到值
            try
            {
                panelDic.TryGetValue(paneltype, out panel);
            }
            catch (Exception e)
            {
                //不知是否有空值异常
                Debug.Log(e.Message);
            }

            //空就去Json表字典找到
            if (panel == null)
            {
                string path = null;

                try
                {
                    panelPathDic.TryGetValue(paneltype, out path);
                }
                catch (Exception e)
                {
                    //空异常
                    Debug.Log(e.Message);
                }
                GameObject panelGo = ObjectPool.GetInstance().GetObj(path.Split('/')[1], CanvasTransform);
                panel = panelGo.GetComponent<BasePanel>();

                panelDic.Add(paneltype, panel);//放入字典
            }
            panel.gameObject.SetActive(true);
            return panel;

        }
        //入栈
        public void PushPanel(string panelType, Animation anim, float time)
        {

            if (panelStack == null)
            {
                panelStack = new Stack<BasePanel>();
            }
            //停止上一个界面
            if (panelStack.Count > 0)
            {
                BasePanel topPanel = panelStack.Peek();
                topPanel.OnPause();
            }
            BasePanel panel = GetPanel(panelType);
            panel.OnEnter();
            panelStack.Push(panel);
            PushAnim(anim, panel.transform, 0.2f);
            panel.gameObject.SetActive(true);
        }
        //出栈
        public void PopPanel(Animation anim, float time)
        {
            if (panelStack == null)
            {
                panelStack = new Stack<BasePanel>();
            }
            if (panelStack.Count <= 0)
            {
                Debug.Log("没有界面");
                return;
            }
            //退出栈顶面板
            BasePanel topPanel = panelStack.Pop();
            //恢复上一个面板放到动画里 播放完成在恢复上一个Panel的焦点
            PopAnim(anim, topPanel.transform, time);

            topPanel.OnExit();
        }


        GameObject systemDialog;//等待框引用
        /// <summary>
        /// 等待提示
        /// </summary>
        public void ShowWaitDialog()
        {
            if (systemDialog == null)
                systemDialog = ObjectPool.GetInstance().GetObj("WaitDialog", CanvasTransform2);
        }
        /// <summary>
        /// 取消等待提示
        /// </summary>
        public void HideWaitDialog()
        {
            ObjectPool.GetInstance().RecycleObj(systemDialog);
        }
        /// <summary>
        /// 用户提示消息不携带方法
        /// </summary>
        public async void SystemDialog(string message, int time)
        {
            //生成预制体
            SystemDialogPanel panel = ObjectPool.GetInstance().GetObj("SystemDialog", CanvasTransform2).GetComponent<SystemDialogPanel>();
            panel.message = message;
            panel.OnEnter();
            await Task.Delay(time);
            ObjectPool.GetInstance().RecycleObj(panel.gameObject);
            
        }

        /// <summary>
        /// 用户提示消息 携带方法
        /// </summary>
        public void SystemDialog(string message, float time, Action func)
        {
            SystemDialogPanel panel = ObjectPool.GetInstance().GetObj("SystemDialog", CanvasTransform2).GetComponent<SystemDialogPanel>();
            panel.func = func;
            panel.message = message;
            panel.OnEnter();
        }

        /// <summary>
        /// 清栈功能！
        /// </summary>
        public void PopAllPanel()
        {
            foreach (BasePanel y in panelStack)
            {
                //可能会有问题
                y.OnExit();
                ObjectPool.GetInstance().RecycleObj(y.gameObject);
            }
            panelStack.Clear();
        }
        /// <summary>
        /// 栈动画 不知道叫什么好了暂叫StackAnim
        /// </summary>
        private void PushAnim(Animation anim, Transform transform, float time)
        {
            float width = transform.GetComponent<RectTransform>().rect.width;
            float height = transform.GetComponent<RectTransform>().rect.height;
            Tweener tween;
            switch (anim)
            {
                case Animation.downTotop:
                    //   transform.position = new Vector3(transform.position.x, transform.position.y-height, transform.position.z);
                    break;
                case Animation.topTodown:
                    //  transform.position = new Vector3(transform.position.x, transform.position.y+height, transform.position.z);
                    break;
                case Animation.leftToright:
                    transform.position = new Vector3(GameFacade.Instance.background.position.x - width, transform.position.y, transform.position.z);
                    tween = transform.DOMoveX(GameFacade.Instance.background.position.x, time);
                    tween.OnComplete(() =>
                    {
                        Debug.Log("完成动画");
                    });
                    break;
                case Animation.rightToleft:
                    transform.position = new Vector3(GameFacade.Instance.background.position.x + width, transform.position.y, transform.position.z);
                    tween = transform.DOMoveX(GameFacade.Instance.background.position.x, time);
                    tween.OnComplete(() =>
                    {
                        Debug.Log("完成动画");
                    });
                    break;
                case Animation.noAnimation:

                    //  transform.position = new Vector3(transform.position.x, transform.position.y ,transform.position.z);
                    break;
                default:
                    Debug.Log("暂时没想好");
                    break;
            }

        }

        /// <summary>
        /// 出栈动画
        /// </summary>
        /// <param name="anim"></param>
        /// <param name="transform"></param>
        /// <param name="time"></param>
        private void PopAnim(Animation anim, Transform transform, float time)
        {
            float width = transform.GetComponent<RectTransform>().rect.width;
            float height = transform.GetComponent<RectTransform>().rect.height;
            Tweener tween;
            BasePanel panel;
            switch (anim)
            {
                case Animation.downTotop:

                    break;
                case Animation.topTodown:
                    break;
                case Animation.leftToright:
                    tween = transform.DOMoveX(transform.position.x + width, time);
                    tween.OnComplete(() =>
                    {
                        //收进对象池
                        ObjectPool.GetInstance().RecycleObj(transform.gameObject);
                        transform.position -= new Vector3(width, 0, 0);
                        if (panelStack.Count > 0)
                        {
                            panel = panelStack.Peek();
                            panel.OnResume();
                        }
                    });

                    break;
                case Animation.rightToleft:

                    tween = transform.DOMoveX(transform.position.x - width, time);
                    tween.OnComplete(() =>
                    {
                        //收进对象池
                        ObjectPool.GetInstance().RecycleObj(transform.gameObject);
                        transform.position += new Vector3(width, 0, 0);
                        if (panelStack.Count > 0)
                        {
                            panel = panelStack.Peek();
                            panel.OnResume();
                        }
                    });

                    break;
                case Animation.noAnimation:
                    tween = transform.DOMoveX(transform.position.x, time);
                    tween.OnComplete(() =>
                    {
                        //收进对象池
                        ObjectPool.GetInstance().RecycleObj(transform.gameObject);
                        transform.position += new Vector3(width, 0, 0);
                        if (panelStack.Count > 0)
                        {
                            panel = panelStack.Peek();
                            panel.OnResume();
                        }
                    });

                    break;
                default:
                    Debug.Log("暂时没想好");
                    break;
            }
        }
        /// <summary>
        /// 测试中全局返回方法
        /// </summary>
        public void Return()
        {
            if (panelStack.Count > 1)
                panelStack.Peek().Return();
            else
                Debug.Log("您是否要退出游戏？");   
        }


    }

    /// <summary>
    /// 动画类型
    /// </summary>
    public enum Animation
    {
        leftToright,
        rightToleft,
        topTodown,
        downTotop,
        noAnimation
    }
}


