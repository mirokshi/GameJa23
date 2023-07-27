using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventoryUI handInventory;
    [SerializeField] private InventorySlot itemPrefab;

    private InventoryUI _selectedInventory;
    
    public InventoryUI SelectedInventory
    {
        get => _selectedInventory;
        set
        {
            _selectedInventory = value;
            _inventoryHighlight.SetParent(value);
        }
    }
    
    private InventorySlot _selectedItem;
    private InventorySlot _overlapItem;
    private RectTransform _rectTransform;

    public InventoryHighlight _inventoryHighlight;
    private InventorySlot _itemToHighlight;
    private Vector2 _oldPosition;
    
    private void Awake()
    {
        _inventoryHighlight = GetComponent<InventoryHighlight>();
    }

    private void Update()
    {
        ItemIconDrag();

        if (_selectedInventory == null)
        {
            _inventoryHighlight.Show(false);
            return;
        }
        
        HandleHighlight();

        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateItem();
        }

        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonPress();
        }
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
    
    private void PlaceItem(Vector2Int tileGridPosition)
    {
        bool complete = _selectedInventory.PlaceItem(_selectedItem, tileGridPosition.x, tileGridPosition.y, ref _overlapItem);
        
        if (complete)
        {
            if (_selectedInventory.Inventory is InventoryPlayer player)
            {
                player.SetValues(_selectedItem.ItemData.weight, _selectedItem.ItemData.value);
            }
            
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
        _selectedItem = _selectedInventory.PickUpItem(tileGridPosition.x, tileGridPosition.y);
        
        if (_selectedItem!=null)
        {
            _rectTransform = _selectedItem.GetComponent<RectTransform>();    
        }
    }
    
    private void RotateItem()
    {
        if (_selectedItem != null)
        {
            _selectedItem.Rotate();
        }
    }
    
    private void PickUpItemForHand(InventorySlot item)
    {
        var hand = (InventoryHand) handInventory.Inventory;
        if (hand is not null) {
            hand = (InventoryHand) handInventory.Inventory;
            if (!hand.IsItemInInventory())
            {
                InventorySlot itemToInsert = CreateItem(item);
                InsertItemHand(itemToInsert);
            }
        }
    }

    private InventorySlot CreateItem(InventorySlot item)
    {
        InventorySlot inventoryItem = Instantiate(itemPrefab).GetComponent<InventorySlot>();
        inventoryItem.Set(item.ItemData);
        return inventoryItem;
    }

    private void InsertItemHand(InventorySlot itemToInsert)
    {
        Vector2Int? posOnGrid = handInventory.FindSpaceForObject(itemToInsert);
        
        if (posOnGrid ==  null) {return;}
        
        handInventory.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
    }
    
    private void HandleHighlight()
    {
        Vector2Int positionOnGrid = GetTileGridPosition();

        if (_oldPosition == positionOnGrid) { return; }
        
        _oldPosition = positionOnGrid;
        
        if (_selectedItem == null && _selectedInventory != null)
        {
            _itemToHighlight = _selectedInventory.Inventory.GetItem(positionOnGrid.x, positionOnGrid.y);
            if (_itemToHighlight != null)
            {
                _inventoryHighlight.Show(true);
                _inventoryHighlight.SetSize(_itemToHighlight);
                _inventoryHighlight.SetPosition(_selectedInventory,_itemToHighlight);   
            }
            else
            {
                _inventoryHighlight.Show(false);
            }
        }
        else
        {
            _inventoryHighlight.Show(_selectedInventory.BoundryCheck(positionOnGrid.x,positionOnGrid.y,_selectedItem.Width,_selectedItem.Height));
            _inventoryHighlight.SetSize(_selectedItem);
            _inventoryHighlight.SetPosition(_selectedInventory,_selectedItem,positionOnGrid.x,positionOnGrid.y);   
        }
    }
    
    private Vector2Int GetTileGridPosition()
    {
        Vector2 position = Input.mousePosition;
        if (_selectedItem != null)
        {
            position.x -= (_selectedItem.Width - 1) * InventoryUI.TileSizeWidth / 2;
            position.y += (_selectedItem.Height - 1) * InventoryUI.TileSizeHeight / 2;
        }
        
        return _selectedInventory.GetTileGridPosition(position);
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
        global::PickUpItem.OnPickUpItem += PickUpItemForHand;
    }

    private void OnDisable()
    {
        global::PickUpItem.OnPickUpItem -= PickUpItemForHand;
    }
}
