using NeNe.LGL.Data;
using NeNe.LGL.Tool;
using System;
using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace NeNe.LGL.Panel
{
    public class Self_birthday : BasePanel
    {
        private GameObject datePanel;//数据面板
        private Text choiceText;
        public int yearOld;//未修改年
        public int monthOld;//未修改月
        public int dayOld;//未修改日
        public int year;//年
        public int month;//月
        public int day;//日
        private Button returnButton;//取消按钮本业没有 保存按钮
        private Button saveButton;//储存按钮
        private Button yearButton;
        public GameObject yearPanel;//年份载体
        CanvasGroup canvasGroup;
        GameObject Calendar;//日历
        private Text datetoday;//今天
        private Button buttonYear;//年的按钮
        private Button nextButton;//下一个
        private Button lastButton;//上一个

        //四年一闰，百年不闰，四百年再闰。
        public override void OnEnter()
        {
            Calendar = transform.Find("Calendar").gameObject;
            datetoday = Calendar.transform.GetChild(0).GetComponent<Text>();
            datetoday.text = DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日" + DateTool.Instance.Week(DateTime.Now.DayOfWeek.ToString());
            buttonYear = Calendar.transform.Find("buttonYear").GetComponent<Button>();
            buttonYear.transform.GetChild(0).GetComponent<Text>().text = "2019";
            datePanel = Calendar.transform.Find("datePanel").gameObject;
            choiceText = Calendar.transform.Find("Choice").GetComponent<Text>();
            yearOld = year = int.Parse(UserInformation.Instance.birthday.Split('/')[0]);//获取生日年
            monthOld = month = int.Parse(UserInformation.Instance.birthday.Split('/')[1]);//获取生日月
            dayOld = day = int.Parse(UserInformation.Instance.birthday.Split('/')[2]);//获取生日天
            Date(year, month, day);
            nextButton = Calendar.transform.Find("Next").GetComponent<Button>();
            yearButton = Calendar.transform.Find("buttonYear").GetComponent<Button>();
            lastButton = Calendar.transform.Find("Last").GetComponent<Button>();
            saveButton = Calendar.transform.Find("Save").GetComponent<Button>();
            returnButton = Calendar.transform.Find("NoSave").GetComponent<Button>();
            yearPanel = Calendar.transform.Find("yearPanel").gameObject;
            canvasGroup = GetComponent<CanvasGroup>();
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
        public override void AddListener()
        {
            canvasGroup.blocksRaycasts = true;
            nextButton.onClick.AddListener(Next);
            lastButton.onClick.AddListener(Last);
            saveButton.onClick.AddListener(Save);
            returnButton.onClick.AddListener(Return);
            yearButton.onClick.AddListener(() =>
            {
                yearPanel.SetActive(true);
            });
            for (int i = 0; i < yearPanel.transform.GetChild(0).childCount; i++)
            {
                int index = i + 1900;
                yearPanel.transform.GetChild(0).GetChild(i).GetComponent<Button>().onClick.AddListener(() => SetYear(index));
                yearPanel.transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<Text>().text = (i + 1900).ToString();
            }
            Date(year, month, day);

        }
        public override void RemoveListener()
        {
            canvasGroup.blocksRaycasts = false;
            nextButton.onClick.RemoveAllListeners();
            lastButton.onClick.RemoveAllListeners();
            saveButton.onClick.RemoveAllListeners();
            returnButton.onClick.RemoveAllListeners();
            for (int i = 0; i < yearPanel.transform.GetChild(0).childCount; i++)
            {
                yearPanel.transform.GetChild(0).GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();
            }
        }

        public override void Return()
        {
            UIManager.Instance.PopPanel(UIFramework.Animation.leftToright, 0.2f);
        }

        /// <summary>
        /// 刷新日期方法
        /// </summary>
        public void Date(int year, int month, int day)
        {
            for (int i = 0; i < 49; i++)
            {
                datePanel.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = "";
                datePanel.transform.GetChild(i).GetComponent<Image>().color = new Color(255, 255, 255, 255);
                datePanel.transform.GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();
            }
            datePanel.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "日";
            datePanel.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "一";
            datePanel.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "二";
            datePanel.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "三";
            datePanel.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = "四";
            datePanel.transform.GetChild(5).GetChild(0).GetComponent<Text>().text = "五";
            datePanel.transform.GetChild(6).GetChild(0).GetComponent<Text>().text = "六";
            int week = DateTool.Instance.ZellerWeek(year, month, 1);//先获取星期几
            for (int i = 7 + week; i < 7 + week + DateTool.Instance.MonthHaveDay(month, year); i++)
            {
                if (day == i - (6 + week))
                    datePanel.transform.GetChild(i).GetComponent<Image>().color = new Color(255, 0, 0, 255);
                datePanel.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = (i - (6 + week)).ToString();


                int index = i;
                datePanel.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(() => ChoiceDay(index));
            }
            choiceText.text = year + "年" + month + "月";
        }

        /// <summary>
        /// 
        /// </summary>
        public void Next()
        {
            if (month >= 12)
            {
                month = 1;
                year += 1;
            }
            else
            {
                month += 1;
            }
            Date(year, month, day);
        }

        /// <summary>
        /// 上一页
        /// </summary>
        public void Last()
        {
            if (month == 1)
            {
                month = 12;
                year -= 1;
            }
            else
            {
                month -= 1;
            }
            Date(year, month, day);
        }
        /// <summary>
        /// 选择哪一天
        /// </summary>
        public void ChoiceDay(int index)
        {
            datePanel.transform.GetChild(DateTool.Instance.ZellerWeek(year, month, 1) + 6 + day).GetComponent<Image>().color = new Color(255, 255, 255, 255);
            datePanel.transform.GetChild(index).GetComponent<Image>().color = new Color(255, 0, 0, 255);//选中变色
            day = int.Parse(datePanel.transform.GetChild(index).GetChild(0).GetComponent<Text>().text);
            datePanel.transform.GetChild(index).GetComponent<Image>().color = new Color(255, 0, 0, 255);
            Debug.Log(year + "/" + month + "/" + day);

        }
        /// <summary>
        /// 保存功能
        /// </summary>
        public void Save()
        {
            if (year == yearOld && month == monthOld && day == dayOld)
            {

            }
            else
            {
                Debug.Log(day);
                UserInformation.Instance.birthday = year + "/" + month + "/" + day;
                UserInformation.Instance.Change();
            }
            Return();
        }

        /// <summary>
        /// 设置年份
        /// </summary>
        public void SetYear(int year)
        {
            this.year = year;
            Date(year, month, day);
            yearPanel.SetActive(false);
        }
    }
}