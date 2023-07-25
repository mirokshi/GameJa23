using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class ItemGrid : MonoBehaviour
{
    public const float TileSizeWidth = 32;
    public const float TileSizeHeight = 32;
    
    public static Action<InventoryItem> OnThrowItem;
    
    private RectTransform _rectTransform;
    
    private Vector2 _positionOnTheGrid;
    private Vector2Int _tileGridPosition;

    private InventoryItem[,] InventoryItemSlot;

    private bool _isItemInInventory;
    private int _totalWeight;
    private int _totalValue;

    [SerializeField] private int gridSizeWidth=7;
    [SerializeField] private int gridSizeHeight=4;
    [SerializeField] private ItemGridType itemGridType;

    public static Action<ItemData> OnUsePotion;

    private void Awake()
    {
        _totalWeight = 0;
        _totalValue = 0;
        _rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth,gridSizeHeight);
    }

    private void Init(int width, int height)
    {
        InventoryItemSlot = new InventoryItem[width, height];
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

    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem overlapItem)
    {
        if (BoundryCheck(posX,posY,inventoryItem.WIDTH,inventoryItem.HEIGHT)==false)
        {
            return false;
        }

        if (OverlapCheck(posX, posY, inventoryItem.WIDTH,inventoryItem.HEIGHT, ref overlapItem) == false)
        {
            overlapItem = null;
            return false;
        }
        
        if (itemGridType == ItemGridType.Hand && _isItemInInventory && overlapItem == null)
        {
            return false;
        }

        if (overlapItem != null)
        {
            ClearGridReference(overlapItem);
        }

        PlaceItem(inventoryItem, posX, posY);
        
        if (itemGridType == ItemGridType.Throw)
        {
            OnThrowItem?.Invoke(inventoryItem);
        }

        return true;
    }

    public void PlaceItem(InventoryItem inventoryItem, int posX, int posY)
    {
        if (itemGridType == ItemGridType.Hand)
        {
            _isItemInInventory = true;
            
            ItemType itemType = inventoryItem._itemData.itemType;
        
            if (itemType.Equals(ItemType.Potion))
            {
                Debug.Log("Potion Activated");
                OnUsePotion?.Invoke(inventoryItem._itemData);
                _isItemInInventory = false;
                inventoryItem.OnDestroy();
            }
        }

        _totalWeight += inventoryItem._itemData.weight;
        _totalValue += inventoryItem._itemData.value;
        
        RectTransform rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(_rectTransform);

        for (int x = 0; x < inventoryItem.WIDTH; x++)
        {
            for (int y = 0; y < inventoryItem.HEIGHT; y++)
            {
                InventoryItemSlot[posX + x, posY + y] = inventoryItem;
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
        position.x = posX * TileSizeWidth + TileSizeWidth * inventoryItem.WIDTH / 2;
        position.y = -(posY * TileSizeHeight + TileSizeHeight * inventoryItem.HEIGHT / 2);

        return position;
    }
    

    public InventoryItem PickUpItem(int x,int y)
    {
        InventoryItem toReturn = InventoryItemSlot[x, y];
        
        if (toReturn == null) { return null; }

        if (itemGridType == ItemGridType.Hand)
        {
            _isItemInInventory = false;
        }
        
        _totalWeight -= toReturn._itemData.weight;
        _totalValue -= toReturn._itemData.value;

        ClearGridReference(toReturn);

        return toReturn;
    }

    private void ClearGridReference(InventoryItem item)
    {
        for (int xi = 0; xi < item.WIDTH; xi++)
        {
            for (int yi = 0; yi < item.HEIGHT; yi++)
            {
                InventoryItemSlot[item.onGridPositionX + xi, item.onGridPositionY + yi] = null;
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
                 if (InventoryItemSlot[posX + x, posY + y] != null)
                 {
                     if (overlapItem == null)
                     {
                         overlapItem = InventoryItemSlot[posX + x, posY + y];
                     }
                     else
                     {
                         if (overlapItem != InventoryItemSlot[posX + x, posY + y])
                         {
                             return false;
                         }
                     }
                 }
             }
         }
         return true;
     }

     internal InventoryItem GetItem(int x, int y)
    {
        return InventoryItemSlot[x, y];
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
                if (InventoryItemSlot[posX+x,posY+y] != null)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool IsItemInInventory()
    {
        if (itemGridType == ItemGridType.Hand)
        {
            return _isItemInInventory;
        }

        return false;
    }

    public ItemData GetItemInHand()
    {
        for (var x = 0; x < gridSizeWidth; x++)
        {
            for (var y = 0; y < gridSizeHeight; y++)
            {
                if (InventoryItemSlot[x,y] != null)
                {
                    return  InventoryItemSlot[x,y]._itemData;
                }
            }
        }

        return null;
    }

    public void OnDestroyItemInHand()
    {
        for (var x = 0; x < gridSizeWidth; x++)
        {
            for (var y = 0; y < gridSizeHeight; y++)
            {
                if (InventoryItemSlot[x,y] != null)
                {
                    InventoryItemSlot[x,y].OnDestroy();
                    _isItemInInventory = false;
                }
            }
        }
    }

    public int GetTotalWeight()
    {
        if (_totalWeight == 0)
        {
            return 1;
        }
        
        return _totalWeight;
    }

    public int GetTotalValue()
    {
        return _totalValue;
    }
    
}
