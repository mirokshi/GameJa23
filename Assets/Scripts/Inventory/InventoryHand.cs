using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "inventoryName",menuName = "Inventory/Hand", order = 0)]
public class InventoryHand : Inventory
{
    private bool _canPlaceItemInInventory;

    public static Action<ItemData> OnUsePotion;

    protected override void OnEnable()
    {
        base.OnEnable();
        _canPlaceItemInInventory = true;
    }

    public override void RemoveItemFromGrid(InventorySlot item)
    {
        base.RemoveItemFromGrid(item);
        _canPlaceItemInInventory = true;
    }

    public override void AddItemToGrid(InventorySlot inventorySlot, int x, int y)
    {
        base.AddItemToGrid(inventorySlot, x, y);

        _canPlaceItemInInventory = false;
        
        ItemType itemType = inventorySlot.ItemData.itemType;
        
        if (itemType.Equals(ItemType.Potion))
        {
            OnUsePotion?.Invoke(inventorySlot.ItemData);
            _canPlaceItemInInventory = true;
            inventorySlot.OnDestroy();
        }
    }

    public override bool CanPlaceItem()
    {
        return _canPlaceItemInInventory;
    }
    
    public ItemData GetItemInHand()
    {
        for (var x = 0; x < gridSizeWidth; x++)
        {
            for (var y = 0; y < gridSizeHeight; y++)
            {
                if (_inventoryItemSlot[x,y] != null)
                {
                    return _inventoryItemSlot[x,y].ItemData;
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
                if (_inventoryItemSlot[x,y] != null)
                {
                    _inventoryItemSlot[x,y].OnDestroy();
                    _canPlaceItemInInventory = true;
                }
            }
        }
    }
}
