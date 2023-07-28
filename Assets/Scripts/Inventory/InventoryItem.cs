using System;
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
    
    
    internal void Set(ItemData itemData, float scaleFactor)
    {
        _itemData = itemData;
        GetComponent<Image>().sprite = itemData.itemIcon;
        
        Vector2 size = new Vector2
        {
            x = WIDTH * ItemGrid.TileSizeWidth,
            y = HEIGHT * ItemGrid.TileSizeHeight
        };

        GetComponent<RectTransform>().sizeDelta = size * scaleFactor;
    }

    public void Rotate()
    {
        rotated = !rotated;
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0,0, rotated? 90f:0f);
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}
