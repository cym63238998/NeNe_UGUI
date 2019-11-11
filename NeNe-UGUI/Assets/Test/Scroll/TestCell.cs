using System;
using UnityEngine;

/// <summary>
/// 可扩展性开发Cell
/// </summary>
public class TestCell : ItemCell
{
    public Action func;//未知方法
    public int id;//id
    
    public override void SetData(ListData data)
    {
        Debug.Log("测试Call"+data.id);
    }   
   
}
