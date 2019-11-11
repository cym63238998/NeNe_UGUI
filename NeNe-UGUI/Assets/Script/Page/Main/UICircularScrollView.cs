
using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnhancedScrollerDemos.SuperSimpleDemo;
using UnityEngine.UI;

public class Data {
    
    public int id;
    public int cellSize;
}


public class UICircularScrollView : MonoBehaviour, IEnhancedScrollerDelegate
{

    public EnhancedScroller scroller;//滚动框
    public EnhancedScrollerCellView cellViewPrefab;//预制体
    public List<Data> _data;//轻量级数组

    public void OnEnter()
    {
        _data = new List<Data>();
        // tell the scroller that this script will be its delegate
        cellViewPrefab = transform.Find("FollowingPanel").GetComponent<EnhancedScrollerCellView>();
        scroller.Delegate = this;
    }




    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            AddNewRow(0);
        }
    }

  
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
    {

        // it will create a new cell.
        FollowingCell cellView = scroller.GetCellView(cellViewPrefab) as FollowingCell;
        cellView.SetData(_data[dataIndex]);
        // set the name of the game object to the cell's data index.
        // this is optional, but it helps up debug the objects in 
        // the scene hierarchy.
        cellView.name = "Cell " + dataIndex.ToString();
        cellView.gameObject.SetActive(true);
        // in this example, we just pass the data to our cell's view which will update its UI
        //cellView.SetData(_data[dataIndex]);

        // return the cell to the scroller
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
        foreach (var item in _data)
        {
            item.cellSize = 0;
        }

        // now we can add the data row
        _data.Add(new Data() { id=index});

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





    




