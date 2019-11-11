using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePage : MonoBehaviour, IEnhancedScrollerDelegate
{

    public EnhancedScroller scroller;//滚动框
    public EnhancedScrollerCellView cellViewPrefab;//预制体
    public List<Data> _data;//轻量级数组
    public int count;//数量
    public int Length;//长度
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        HomeCell cellView = scroller.GetCellView(cellViewPrefab) as HomeCell;   
        cellView.SetData("6666");
        return cellView;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        //if (_data[dataIndex])
        //{

        //}
        //做数据判断
        return 200f;
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return _data.Count;
    }

    // Start is called before the first frame update
    void Start()
    {
        scroller.Delegate = this;
        LoadLargeData();
    }
    private void LoadLargeData()
    {
        _data = new List<Data>();
        for (int i = 0; i < count; i++)
            _data.Add(new Data() { });

    }

}
