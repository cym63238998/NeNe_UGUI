using EnhancedUI.EnhancedScroller;
using NeNe.LGL.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自己的滑动Item
/// </summary>
public abstract class ItemCell : EnhancedScrollerCellView
{
    public abstract void SetData(ListData data);   
}
