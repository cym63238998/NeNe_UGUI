using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NeNe.LGL.Tool
{
    public class DateTool : SingleTon<DateTool>
    {
        /// <summary>
        /// 是否闰年？？？？
        /// </summary>
        public int IsRunYear(int year)
        {
            if ((year % 4) == 0)
            {
                if ((year % 100) == 0)
                {
                    if ((year % 400) == 0)
                    {
                        return 366;
                    }
                    else
                    {
                        return 365;
                    }
                }
                else
                {
                    return 366;
                }
            }
            else
            {
                return 365;
            }
        }

        /// <summary>
        /// 返回一个月有几天
        /// </summary>
        /// <returns></returns>
        public int MonthHaveDay(int month, int year)
        {
            switch (month)
            {
                case 1: return 31;
                case 2:
                    if (IsRunYear(year) == 365)
                        return 28;
                    else
                        return 29;
                case 3: return 31;
                case 4: return 30;
                case 5: return 31;
                case 6: return 30;
                case 7: return 31;
                case 8: return 31;
                case 9: return 30;
                case 10: return 31;
                case 11: return 30;
                case 12: return 31;
                default: return 0;
            }
        }
        /// <summary>
        /// 蔡勒公式判断星期几
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public int ZellerWeek(int year, int month, int day)
        {
            int m = month;
            int d = day;
            if (month <= 2) /*对小于2的月份进行修正*/
            {
                year--;
                m = month + 12;
            }
            int y = year % 100;
            int c = year / 100;
            int w = (y + y / 4 + c / 4 - 2 * c + (13 * (m + 1) / 5) + d - 1) % 7;
            if (w < 0) /*修正计算结果是负数的情况*/
                w += 7;
            return w;
        }
        public string Week(string englishWeek)
        {
            switch (englishWeek)
            {
                case "Monday":
                    return "周一";
                case "Tuesday":
                    return "周二";
                case "Wednesday":
                    return "周三";
                case "Thursday":
                    return "周四";
                case "Friday":
                    return "周五";
                case "Saturday":
                    return "周六";
                case "Sunday":
                    return "周日";
                default:
                    return null;
            }
        }
    }
}