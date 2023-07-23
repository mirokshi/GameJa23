using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ItemGrid : MonoBehaviour
{
    public const float tileSizeWidth = 32;
    public const float tileSizeHeight = 32;

    private RectTransform _rectTransform;
    
    private Vector2 positionOnTheGrid = new Vector2();
    private Vector2Int tileGridPosition = new Vector2Int();

    private InventoryItem[,] _inventoryItemSlot;
    
    [SerializeField] private int gridSizeWidth=7;
    [SerializeField] private int gridSizeHeight=4;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth,gridSizeHeight);
    }

    private void Init(int width, int height)
    {
        _inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width * tileSizeWidth, height * tileSizeHeight);
        _rectTransform.sizeDelta = size;
    }

    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        var position = _rectTransform.position;
        positionOnTheGrid.x = mousePosition.x - position.x;
        positionOnTheGrid.y = position.y - mousePosition.y;
        tileGridPosition.x = (int)(positionOnTheGrid.x / tileSizeWidth);
        tileGridPosition.y = (int)(positionOnTheGrid.y / tileSizeHeight);

        return tileGridPosition;
    }

    public bool PlaceItem(InventoryItem inventoryItem,int posX,int posY,ref InventoryItem overlapITem)
    {
        if (BoundryCheck(posX,posY,inventoryItem.WIDTH,inventoryItem.HEIGHT)==false)
        {
            return false;
        }

        if (OverlapCheck(posX,posY,inventoryItem.WIDTH,inventoryItem.HEIGHT,ref overlapITem)== false)
        {
            overlapITem = null;
            return false;
        }

        if (overlapITem != null)
        {
            ClearGridReference(overlapITem);
        }
        
        PlaceItem(inventoryItem, posX, posY);

        return true;
    }

    public void PlaceItem(InventoryItem inventoryItem, int posX, int posY)
    {
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(_rectTransform);

        for (int x = 0; x < inventoryItem.WIDTH; x++)
        {
            for (int y = 0; y < inventoryItem.HEIGHT; y++)
            {
                _inventoryItemSlot[posX + x, posY + y] = inventoryItem;
            }
        }

        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;

        Vector2 position = CalculatePositionGrid(inventoryItem, posX, posY);

        rectTransform.localPosition = position;
    }

    public Vector2 CalculatePositionGrid(InventoryItem inventoryItem, int posX, int posY)
    {
        Vector2 position = new Vector2();
        position.x = posX * tileSizeWidth + tileSizeWidth * inventoryItem.WIDTH / 2;
        position.y = -(posY * tileSizeHeight + tileSizeHeight * inventoryItem.HEIGHT / 2);

        return position;
    }
    

    public InventoryItem PickUpItem(int x,int y)
    {
        InventoryItem toReturn = _inventoryItemSlot[x, y];
        
        if (toReturn == null) { return null; }
        
        ClearGridReference(toReturn);

        return toReturn;
    }

    private void ClearGridReference(InventoryItem item)
    {
        for (int xi = 0; xi < item.WIDTH; xi++)
        {
            for (int yi = 0; yi < item.HEIGHT; yi++)
            {
                _inventoryItemSlot[item.onGridPositionX + xi, item.onGridPositionY + yi] = null;
            }
        }
    }

    bool PositionCheck(int posX, int posY)
    {
        if (posX < 0 || posY < 0)
        {
            return false;
        }

        if (posX>= gridSizeWidth || posY >= gridSizeHeight)
        {
            return false;
        }

        return true;
    }

    public bool BoundryCheck(int posX, int posY,int width,int height)
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

    private bool OverlapCheck(int posX, int posY, int width, int height, ref InventoryItem overlapItem)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (_inventoryItemSlot[posX+x,posY+y] != null)
                {
                    overlapItem = _inventoryItemSlot[posX + x, posY + y];
                }
                else
                {
                    if (overlapItem != _inventoryItemSlot[posX + x, posY + y])
                    {
                        return false;   
                    }
                }
            }
        }
        return true;
    }

    internal InventoryItem GetItem(int x, int y)
    {
        return _inventoryItemSlot[x, y];
    }

    public Vector2Int? FindSpaceForObject(InventoryItem itemToInsert)
    {
        int height = gridSizeHeight - itemToInsert.HEIGHT+1;
        int width = gridSizeWidth - itemToInsert.WIDTH+1;
        for (int y = 0; y < height; y++)
        {
           for (int x = 0; x < width; x++)
           {
               if (CheckAvailableSpace(x, y, itemToInsert.WIDTH, itemToInsert.HEIGHT))
               {
                   return new Vector2Int(x, y);
               }
           } 
        }

        return null;
    }
    
    private bool CheckAvailableSpace(int posX, int posY, int width, int height)
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
    
}
