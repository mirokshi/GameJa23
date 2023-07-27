using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;
    [SerializeField] private Inventory _inventory;

    public ItemData ItemData => _itemData;
    public Inventory Inventory => _inventory;

    public int OnGridPositionX { get; private set; }
    public int OnGridPositionY { get; private set; }
    private bool Rotated { get; set; }

    public int Height
    {
        get
        {
            if (Rotated == false)
            {
                return _itemData.height;
            }

            return _itemData.width;
        }
    }

    public int Width
    {
        get
        {
            if (Rotated == false)
            {
                return _itemData.width;
            }

            return _itemData.height;
        }
    }
    
    private RectTransform _rectTransform;
    private Image _image;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }
    
    internal void Set(ItemData newItemData)
    {
        _itemData = newItemData;
        _image.sprite = newItemData.itemIcon;
        
        var size = new Vector2
        {
            x = Width * InventoryUI.TileSizeWidth,
            y = Height * InventoryUI.TileSizeHeight
        };

        _rectTransform.sizeDelta = size;
    }

    public void Rotate()
    {
        Rotated = !Rotated;
        _rectTransform.rotation = Quaternion.Euler(0,0, Rotated? 90f:0f);
    }

    public void SetGridPosition(Inventory inventory, int posX, int posY)
    {
        _inventory = inventory;
        OnGridPositionX = posX;
        OnGridPositionY = posY;
    }
    
    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}
