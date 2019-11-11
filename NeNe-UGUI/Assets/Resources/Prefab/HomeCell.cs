using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeCell : EnhancedScrollerCellView
{
    public Text dataText;


    public void SetData(string data)
    {
        dataText.text = data;
    }
}
