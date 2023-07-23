using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public ItemData _itemData;

    public int HEIGHT
    {
        get
        {
            if (rotated == false)
            {
                return _itemData.height;
            }

            return _itemData.width;
        }
    }

    public int WIDTH
    {
        get
        {
            if (rotated == false)
            {
                return _itemData.width;
            }

            return _itemData.height;
        }
    }
    
    public int onGridPositionX;
    public int onGridPositionY;

    public bool rotated = false;
    
    
    internal void Set(ItemData itemData)
    {
        this._itemData = itemData;
        GetComponent<Image>().sprite = itemData.itemIcon;
        
        Vector2 size = new Vector2();
        size.x = WIDTH * ItemGrid.tileSizeWidth;
        size.y = HEIGHT * ItemGrid.tileSizeHeight;
        GetComponent<RectTransform>().sizeDelta = size;
    }

    public void Rotate()
    {
        rotated = !rotated;
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0,0, rotated? 90f:0f);
    }
}
