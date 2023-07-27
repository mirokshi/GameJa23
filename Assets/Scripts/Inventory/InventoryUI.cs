using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class InventoryUI : MonoBehaviour
{
    public const float TileSizeWidth = 32;
    public const float TileSizeHeight = 32;
    
    [SerializeField] private Inventory inventory;

    public Inventory Inventory => inventory;
    
    private RectTransform _rectTransform;
    private Vector2 _positionOnTheGrid;
    private Vector2Int _tileGridPosition;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        Init(inventory.Width, inventory.Height);
    }
    
    private void Init(int width, int height)
    {
        Vector2 size = new Vector2(width * TileSizeWidth, height * TileSizeHeight);
        _rectTransform.sizeDelta = size;
    }
    
    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        var position = _rectTransform.position;
        _positionOnTheGrid.x = mousePosition.x - position.x;
        _positionOnTheGrid.y = position.y - mousePosition.y;
        _tileGridPosition.x = (int)(_positionOnTheGrid.x / TileSizeWidth);
        _tileGridPosition.y = (int)(_positionOnTheGrid.y / TileSizeHeight);

        return _tileGridPosition;
    }
    
    public bool PlaceItem(InventorySlot inventorySlot, int posX, int posY, ref InventorySlot overlapItem)
    {
        if (BoundryCheck(posX,posY,inventorySlot.Width,inventorySlot.Height)==false)
        {
            return false;
        }

        if (OverlapCheck(posX, posY, inventorySlot.Width,inventorySlot.Height, ref overlapItem) == false)
        {
            overlapItem = null;
            return false;
        }

        if (overlapItem != null)
        {
            inventory.RemoveItemFromGrid(overlapItem);
        }

        PlaceItem(inventorySlot, posX, posY);

        return true;
    }
    
    public void PlaceItem(InventorySlot inventorySlot, int posX, int posY)
    {
        RectTransform rectTransform = inventorySlot.GetComponent<RectTransform>();
        rectTransform.SetParent(_rectTransform);

        for (int x = 0; x < inventorySlot.Width; x++)
        {
            for (int y = 0; y < inventorySlot.Height; y++)
            {
                inventory.AddItemToGrid(inventorySlot, posX + x, posY + y);
            }
        }
        
        inventorySlot.SetGridPosition(inventory, posX, posY);

        var position = CalculatePositionGrid(inventorySlot, posX, posY);

        rectTransform.localPosition = position;
    }

    public InventorySlot PickUpItem(int x,int y)
    {
        var toReturn = inventory.GetItem(x,y);
        
        if (toReturn == null) { return null; }

        inventory.RemoveItemFromGrid(toReturn);

        return toReturn;
    }

    public Vector2 CalculatePositionGrid(InventorySlot inventoryItem, int posX, int posY)
    {
        var position = new Vector2
        {
            x = posX * TileSizeWidth + TileSizeWidth * inventoryItem.Width / 2,
            y = -(posY * TileSizeHeight + TileSizeHeight * inventoryItem.Height / 2)
        };

        return position;
    }
    
    private bool PositionCheck(int posX, int posY)
    {
        if (posX < 0 || posY < 0)
        {
            return false;
        }

        if (posX>= inventory.Width || posY >= inventory.Height)
        {
            return false;
        }

        return true;
    }

    internal bool BoundryCheck(int posX, int posY,int width,int height)
    {
        if (PositionCheck(posX,posY)==false)
        {
            return false;
        }

        posX += width-1;
        posY += height-1;

        if (PositionCheck(posX, posY) == false)
        {
            return false;
        }
        return true;
    }

    private bool OverlapCheck(int posX, int posY, int width, int height, ref InventorySlot overlapItem)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventory.GetItem(posX + x, posY + y) != null)
                {
                    if (overlapItem == null)
                    {
                        overlapItem = inventory.GetItem(posX + x, posY + y);
                    }
                    else
                    {
                        if (overlapItem != inventory.GetItem(posX + x, posY + y))
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
    
    public Vector2Int? FindSpaceForObject(InventorySlot itemToInsert)
    {
        int height = inventory.Height - itemToInsert.Height+1;
        int width = inventory.Width - itemToInsert.Width+1;
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (inventory.CheckAvailableSpace(x, y, itemToInsert.Width, itemToInsert.Height))
                {
                    return new Vector2Int(x, y);
                }
            } 
        }

        return null;
    }
}
