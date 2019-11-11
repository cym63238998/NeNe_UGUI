using NeNe.LGL.Data;
using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace NeNe.LGL.Panel
{
    public class Self_area : BasePanel
    {
        private Dropdown area;
        private Dictionary<string, string> city = new Dictionary<string, string>();
        private Button saveBtn;//保存按钮
        private Button noSaveBtn;//取消按钮
        CanvasGroup canvasgroup;
        public override void AddListener()
        {
            saveBtn.onClick.AddListener(Save);
            noSaveBtn.onClick.AddListener(Return);
            canvasgroup.blocksRaycasts = true;
        }
        int index = 0;
        public override void OnEnter()
        {
            canvasgroup = GetComponent<CanvasGroup>();
            area = transform.Find("area").GetComponent<Dropdown>();
            saveBtn = transform.Find("Save").GetComponent<Button>();
            noSaveBtn = transform.Find("NoSave").GetComponent<Button>();

            //初始化加入城市
            if (city.Count == 0)
            {
                city.Add("Beijing", "北京");
                city.Add("Shanghai", "上海");
                city.Add("Tianjin", "天津");
                city.Add("chongqing", "重庆");
                city.Add("Harbin", "哈尔滨");
                city.Add("Shenyang", "沈阳");
                city.Add("Changchun", "长春");
                List<Dropdown.OptionData> dp = new List<Dropdown.OptionData>();
                foreach (KeyValuePair<string, string> str in city)
                {
                    dp.Add(new Dropdown.OptionData(str.Value));
                    Debug.Log(str.Value + UserInformation.Instance.area);
                    if (str.Value == UserInformation.Instance.area)
                    {
                        Debug.Log("相同");
                        Debug.Log(index);
                        area.value = index;
                    }
                    index += 1;
                }
                area.AddOptions(dp);
            }
            AddListener();
        }
        public override void OnExit()
        {
            RemoveListener();
        }

        public override void OnPause()
        {
            RemoveListener();
        }

        public override void OnResume()
        {
            AddListener();
        }

        public override void RemoveListener()
        {
            canvasgroup.blocksRaycasts = false;
            area.onValueChanged.RemoveListener(CityValue);
            saveBtn.onClick.RemoveListener(Save);
            noSaveBtn.onClick.RemoveListener(Return);
        }

        public void Save()
        {
            Debug.Log(area.options[area.value].text);
            if (UserInformation.Instance.area != area.options[area.value].text)
            {
                UserInformation.Instance.area = area.options[area.value].text;
                UserInformation.Instance.Change();
            }

            Return();
        }

        /// <summary>
        /// 下拉城市
        /// </summary>
        public void CityValue(int value)
        {

        }

        public override void Return()
        {
            UIManager.Instance.PopPanel(UIFramework.Animation.noAnimation, 0.2f);
            Debug.Log("返回");
        }
    }
    //city.Add("changchun", "呼和浩特");
    //city.Add("changchun", "石家庄");
    //city.Add("changchun", "太原");
    //city.Add("changchun", "济南");
    //city.Add("changchun", "郑州");
    //city.Add("changchun", "武汉");
    //city.Add("changchun", "长沙");
    //city.Add("changchun", "合肥");
    //city.Add("changchun", "南京");
    //city.Add("changchun", "杭州");
    //city.Add("changchun", "南昌");
    //city.Add("changchun", "福州");
    //city.Add("Guangzhou", "广州");
    //city.Add("changchun", "南宁");
    //city.Add("changchun", "成都");
    //city.Add("changchun", "昆明");
    //city.Add("changchun", "贵阳");
    //city.Add("changchun", "西安");
    //city.Add("changchun", "兰州");
    //city.Add("changchun", "乌鲁木齐");
    //city.Add("changchun", "拉萨");
}