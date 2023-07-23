using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [HideInInspector]
    private ItemGrid selectedItemGrid;
    
    public ItemGrid SelectedItemGrid
    {
        get => selectedItemGrid;
        set
        {
            selectedItemGrid = value;
            _inventoryHighlight.SetParent(value);
        }
    }

    private InventoryItem selectedItem;
    private InventoryItem overlapItem;
    private RectTransform _rectTransform;

    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] private Transform canvasTransform;

    private InventoryHighlight _inventoryHighlight;
    InventoryItem itemToHighlight;

    Vector2 oldPosition;

    private void Awake()
    {
        _inventoryHighlight = GetComponent<InventoryHighlight>();
    }

    private void Update()
    {
        ItemIconDrag();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (selectedItem==null)
            {
                CreateRandomItem();    
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            InsertRandomItem();
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateItem();
        }


        if (selectedItemGrid == null)
        {
            _inventoryHighlight.Show(false);
            return;
        }
        
        HandleHighlight();

        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonPress();
        }

    }

    private void RotateItem()
    {
        if (selectedItem==null){return;}

        selectedItem.Rotate();
    }

    private void InsertRandomItem()
    {
        if(selectedItemGrid ==  null) {return;}
        
        CreateRandomItem();
        InventoryItem itemToInsert = selectedItem;
        selectedItem = null;
        InsertItem(itemToInsert);
    }

    private void InsertItem(InventoryItem itemToInsert)
    {
        //Inserta objects al inventario
        
        Vector2Int? posOnGrid = selectedItemGrid.FindSpaceForObject(itemToInsert);
        if (posOnGrid ==  null) {return;}

        selectedItemGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
    }

    private void HandleHighlight()
    {
        Vector2Int positionOnGrid = GetTileGridPosition();

        if (oldPosition == positionOnGrid) { return; }
        
        oldPosition = positionOnGrid;
        if (selectedItem == null)
        {
            itemToHighlight = selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);
            if (itemToHighlight != null )
            {
                _inventoryHighlight.Show(true);
                _inventoryHighlight.SetSize(itemToHighlight);
                _inventoryHighlight.SetPosition(selectedItemGrid,itemToHighlight);   
            }
            else
            {
                _inventoryHighlight.Show(false);
            }
        }
        else
        {
            _inventoryHighlight.Show(selectedItemGrid.BoundryCheck(positionOnGrid.x,positionOnGrid.y,selectedItem.WIDTH,selectedItem.HEIGHT));
            _inventoryHighlight.SetSize(selectedItem);
            _inventoryHighlight.SetPosition(selectedItemGrid,selectedItem,positionOnGrid.x,positionOnGrid.y);   
        }
    }
    

    private void CreateRandomItem()
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;
        _rectTransform = inventoryItem.GetComponent<RectTransform>();
        _rectTransform.SetParent(canvasTransform);
        _rectTransform.SetAsLastSibling();

        int selectedItemID = UnityEngine.Random.Range(0, items.Count);
        inventoryItem.Set(items[selectedItemID]);
    }

    private void LeftMouseButtonPress()
    {
        var tileGridPosition = GetTileGridPosition();

        if (selectedItem == null)
        {
            PickUpItem(tileGridPosition);
        }
        else
        {
            PlaceItem(tileGridPosition);
        }
    }

    private Vector2Int GetTileGridPosition()
    {
        Vector2 position = Input.mousePosition;
        if (selectedItem != null)
        {
            position.x -= (selectedItem.WIDTH - 1) * ItemGrid.tileSizeWidth / 2;
            position.y += (selectedItem.HEIGHT - 1) * ItemGrid.tileSizeHeight / 2;
        }
        
        return selectedItemGrid.GetTileGridPosition(position);
    }

    private void PlaceItem(Vector2Int tileGridPosition)
    {
        bool complete= selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overlapItem);
        if (complete)
        {
            selectedItem = null;
            if (overlapItem!=null)
            {
                selectedItem = overlapItem;
                overlapItem = null;
                _rectTransform = selectedItem.GetComponent<RectTransform>();
                _rectTransform.SetAsLastSibling();
            }
        }
        
    }
    private void PickUpItem(Vector2Int tileGridPosition)
    {
        selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
        if (selectedItem!=null)
        {
            _rectTransform = selectedItem.GetComponent<RectTransform>();    
        }
        
    }

    private void ItemIconDrag()
    {
        if (selectedItem != null)
        {
            _rectTransform.position = Input.mousePosition;
        }
    }
}
