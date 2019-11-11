using EnhancedUI.EnhancedScroller;
using NeNe.LGL.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleScroll : MonoBehaviour, IEnhancedScrollerDelegate
{
    public string path;//路径加载
    public List<ListData> _data = new List<ListData>();
    public EnhancedScroller scroller;//滚动框
    public EnhancedScrollerCellView cellViewPrefab;//预制体 这个预制体也继承了 插件的类
    public float length = 100;
    public Slider slider;
  
    /// <summary>
    /// 生成Item的初始化
    /// </summary>
    /// <param name="scroller"></param>
    /// <param name="dataIndex"></param>
    /// <param name="cellIndex"></param>
    /// <returns></returns>
    void Start()
    {
        
        scroller.Delegate = this;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddNewRow(UnityEngine.Random.Range(0, 10));
        }
    }
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {

        cellViewPrefab = Resources.Load<GameObject>(path).GetComponent<ItemCell>();
        ItemCell cellView = scroller.GetCellView(cellViewPrefab) as ItemCell;
        cellView.SetData(_data[dataIndex]);
        cellView.gameObject.SetActive(true);
        return cellView;
    }
    Action<EnhancedScrollerCellView> prefab;
    /// <summary>
    /// 生成预制体
    /// </summary>
    /// <param name="scroller"></param>
    /// <param name="dataIndex"></param>
    /// <returns></returns>
    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        //可根据索引更改
        return length;
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return _data.Count;
    }

    /// <summary>
    /// 增加数据
    /// </summary>.
    public void AddNewRow(int index)
    {
        // first, clear out the cells in the scroller so the new text transforms will be reset
        scroller.ClearAll();
        // reset the scroller's position so that it is not outside of the new bounds
        scroller.ScrollPosition = 0;

        // second, reset the data's cell view sizes
        foreach (var item in _data)//重置单元格大小
        {
            item.cellSize = 0;
        }

        // now we can add the data row
        _data.Add( new ListData() { id = index });

        ResizeScroller();

        // optional: jump to the end of the scroller to see the new content
        scroller.JumpToDataIndex(_data.Count - 1, 1f, 1f);
    }


    /// <summary>
    /// This function will exand the scroller to accommodate the cells, reload the data to calculate the cell sizes,
    /// reset the scroller's size back, then reload the data once more to display the cells.
    /// </summary>
    private void ResizeScroller()
    {
        // capture the scroller dimensions so that we can reset them when we are done
        var rectTransform = scroller.GetComponent<RectTransform>();
        var size = rectTransform.sizeDelta;

        // set the dimensions to the largest size possible to acommodate all the cells
        rectTransform.sizeDelta = new Vector2(size.x, float.MaxValue);

        // First Pass: reload the scroller so that it can populate the text UI elements in the cell view.
        // The content size fitter will determine how big the cells need to be on subsequent passes.
        _calculateLayout = true;
        scroller.ReloadData();

        // reset the scroller size back to what it was originally
        rectTransform.sizeDelta = size;

        // Second Pass: reload the data once more with the newly set cell view sizes and scroller content size
        _calculateLayout = false;
        scroller.ReloadData();
    }
    private bool _calculateLayout;
}
