using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegexManager 
{
    /// <summary>
    /// 验证手机号
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public bool IsPhone(string str)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(str, @"^[1]+[3,5]+\d{9}");
    }
}
