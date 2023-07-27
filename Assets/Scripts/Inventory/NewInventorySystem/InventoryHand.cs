using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "inventoryName",menuName = "Inventory/Hand", order = 0)]
public class InventoryHand : Inventory
{
    private bool _isItemInInventory;

    public static Action<ItemData> OnUsePotion;

    public override void AddItemToGrid(InventorySlot inventorySlot, int x, int y)
    {
        base.AddItemToGrid(inventorySlot, x, y);
        
        ItemType itemType = inventorySlot.ItemData.itemType;
        
        if (itemType.Equals(ItemType.Potion))
        {
            Debug.Log("Potion Activated");
            OnUsePotion?.Invoke(inventorySlot.ItemData);
            inventorySlot.OnDestroy();
        }
    }

    public bool IsItemInInventory()
    {
        return GetItemInHand() != null;
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
                }
            }
        }
    }
}
