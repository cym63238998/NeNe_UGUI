using EnhancedUI.EnhancedScroller;
using System.Collections.Generic;
using UnityEngine;

public class FollowerScroll : MonoBehaviour, IEnhancedScrollerDelegate
{
    public EnhancedScroller scroller;//滚动框
    public EnhancedScrollerCellView cellViewPrefab;//预制体
    public List<Data> _data;//轻量级数组
    public string path;//路径加载

    public void OnEnter()
    {
        // tell the scroller that this script will be its delegate
        cellViewPrefab = transform.Find("Follower_Panel").GetComponent<EnhancedScrollerCellView>();
        _data = new List<Data>();
        scroller.Delegate = this;
    }

   

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {
        Debug.Log(dataIndex);
        cellViewPrefab = Resources.Load<EnhancedScrollerCellView>(path);
        // it will create a new cell.
        FollowerCell cellView = scroller.GetCellView(cellViewPrefab) as FollowerCell;     
        cellView.name = "Cell " + dataIndex.ToString();
        cellView.SetData(_data[dataIndex]);//
        cellView.gameObject.SetActive(true);
        return cellView;
    }
    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 200f;
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
        _data.Add(new Data() {id=index });

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
