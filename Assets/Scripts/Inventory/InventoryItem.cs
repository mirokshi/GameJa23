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
    
    
    internal void Set(ItemData itemData)
    {
        _itemData = itemData;
        GetComponent<Image>().sprite = itemData.itemIcon;
        
        Vector2 size = new Vector2();
        size.x = WIDTH * ItemGrid.TileSizeWidth;
        size.y = HEIGHT * ItemGrid.TileSizeHeight;
        
        GetComponent<RectTransform>().sizeDelta = size;
    }

    public void Rotate()
    {
        rotated = !rotated;
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.rotation = Quaternion.Euler(0,0, rotated? 90f:0f);
    }

    public void OnDestroy()
    {
        Debug.Log("GameObject " + _itemData.name + " is being destroyed!"); // DELETE LATER
        Destroy(gameObject);
    }
}
