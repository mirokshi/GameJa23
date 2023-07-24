using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [HideInInspector]
    private ItemGrid selectedItemGrid;

    [SerializeField] private ItemGrid handInventory;
    [SerializeField] private ItemGrid playerInventory;
    
    public ItemGrid SelectedItemGrid
    {
        get => selectedItemGrid;
        set
        {
            selectedItemGrid = value;
            _inventoryHighlight.SetParent(value);
        }
    }

    private InventoryItem _selectedItem;
    private InventoryItem _overlapItem;
    private RectTransform _rectTransform;

    [SerializeField] private List<ItemData> items;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform canvasTransform;

    private InventoryHighlight _inventoryHighlight;
    private InventoryItem _itemToHighlight;
    private Vector2 _oldPosition;

    private void Awake()
    {
        _inventoryHighlight = GetComponent<InventoryHighlight>();
    }

    private void Update()
    {
        ItemIconDrag();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_selectedItem==null)
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
        
        Debug.Log("Inventory Weight: " + playerInventory.GetTotalWeight());
    }

    private void RotateItem()
    {
        if (_selectedItem==null){return;}

        _selectedItem.Rotate();
    }

    private void InsertRandomItem()
    {
        if(selectedItemGrid ==  null) {return;}
        
        CreateRandomItem();
        InventoryItem itemToInsert = _selectedItem;
        _selectedItem = null;
        InsertItem(itemToInsert);
    }

    private void InsertItem(InventoryItem itemToInsert)
    {
        Vector2Int? posOnGrid = selectedItemGrid.FindSpaceForObject(itemToInsert);
        
        if (posOnGrid ==  null) {return;}
        
        selectedItemGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
    }

    private void HandleHighlight()
    {
        Vector2Int positionOnGrid = GetTileGridPosition();

        if (_oldPosition == positionOnGrid) { return; }
        
        _oldPosition = positionOnGrid;
        if (_selectedItem == null)
        {
            _itemToHighlight = selectedItemGrid.GetItem(positionOnGrid.x, positionOnGrid.y);
            if (_itemToHighlight != null )
            {
                _inventoryHighlight.Show(true);
                _inventoryHighlight.SetSize(_itemToHighlight);
                _inventoryHighlight.SetPosition(selectedItemGrid,_itemToHighlight);   
            }
            else
            {
                _inventoryHighlight.Show(false);
            }
        }
        else
        {
            _inventoryHighlight.Show(selectedItemGrid.BoundryCheck(positionOnGrid.x,positionOnGrid.y,_selectedItem.WIDTH,_selectedItem.HEIGHT));
            _inventoryHighlight.SetSize(_selectedItem);
            _inventoryHighlight.SetPosition(selectedItemGrid,_selectedItem,positionOnGrid.x,positionOnGrid.y);   
        }
    }
    

    private void CreateRandomItem()
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        _selectedItem = inventoryItem;
        _rectTransform = inventoryItem.GetComponent<RectTransform>();
        _rectTransform.SetParent(canvasTransform);
        _rectTransform.SetAsLastSibling();

        int selectedItemID = UnityEngine.Random.Range(0, items.Count);
        inventoryItem.Set(items[selectedItemID]);
    }

    private void PickUpItem(InventoryItem item)
    {
        if (!handInventory.IsItemInInventory())
        {
            var itemToInsert = CreateItem(item);
            InsertItemHand(itemToInsert);
        }
    }

    private InventoryItem CreateItem(InventoryItem item)
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        inventoryItem.Set(item._itemData);
        return inventoryItem;
    }
    
    private void InsertItemHand(InventoryItem itemToInsert)
    {
        Vector2Int? posOnGrid = handInventory.FindSpaceForObject(itemToInsert);
        
        if (posOnGrid ==  null) {return;}
        
        handInventory.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
    }

    private void LeftMouseButtonPress()
    {
        var tileGridPosition = GetTileGridPosition();

        if (_selectedItem == null)
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
        if (_selectedItem != null)
        {
            position.x -= (_selectedItem.WIDTH - 1) * ItemGrid.TileSizeWidth / 2;
            position.y += (_selectedItem.HEIGHT - 1) * ItemGrid.TileSizeHeight / 2;
        }
        
        return selectedItemGrid.GetTileGridPosition(position);
    }

    private void PlaceItem(Vector2Int tileGridPosition)
    {
        bool complete= selectedItemGrid.PlaceItem(_selectedItem, tileGridPosition.x, tileGridPosition.y, ref _overlapItem);
        
        if (complete)
        {
            _selectedItem = null;
            if (_overlapItem!=null)
            {
                _selectedItem = _overlapItem;
                _overlapItem = null;
                _rectTransform = _selectedItem.GetComponent<RectTransform>();
                _rectTransform.SetAsLastSibling();
            }
        }
    }

    private void PickUpItem(Vector2Int tileGridPosition)
    {
        _selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
        if (_selectedItem!=null)
        {
            _rectTransform = _selectedItem.GetComponent<RectTransform>();    
        }
        
    }

    private void ItemIconDrag()
    {
        if (_selectedItem != null)
        {
            _rectTransform.position = Input.mousePosition;
        }
    }

    private void OnEnable()
    {
        global::PickUpItem.OnPickUpItem += PickUpItem;
    }

    private void OnDisable()
    {
        global::PickUpItem.OnPickUpItem -= PickUpItem;
    }
}
