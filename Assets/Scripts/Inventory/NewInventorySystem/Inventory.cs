using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory : ScriptableObject
{
    protected InventorySlot[,] _inventoryItemSlot;
    
    [SerializeField] protected int gridSizeWidth=7;
    [SerializeField] protected int gridSizeHeight=4;
    
    public int Width => gridSizeWidth;
    public int Height => gridSizeHeight;

    protected virtual void OnEnable()
    {
        _inventoryItemSlot = new InventorySlot[gridSizeWidth, gridSizeHeight];
    }

    public virtual void RemoveItemFromGrid(InventorySlot item)
    {
        for (int xi = 0; xi < item.Width; xi++)
        {
            for (int yi = 0; yi < item.Height; yi++)
            {
                _inventoryItemSlot[item.OnGridPositionX + xi, item.OnGridPositionY + yi] = null;
            }
        }
    }

    public bool CheckAvailableSpace(int posX, int posY, int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (_inventoryItemSlot[posX+x,posY+y] != null)
                {
                    return false;
                }
            }
        }
        return true;
    }
    
    public InventorySlot GetItem(int x, int y)
    {
        return _inventoryItemSlot[x, y];
    }

    public virtual void AddItemToGrid(InventorySlot inventorySlot, int x, int y)
    {
        _inventoryItemSlot[x, y] = inventorySlot;
    }
}
