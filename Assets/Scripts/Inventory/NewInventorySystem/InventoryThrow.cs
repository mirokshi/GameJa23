using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "inventoryName",menuName = "Inventory/Throw", order = 0)]
public class InventoryThrow : Inventory
{
    public static Action<InventorySlot> OnThrowItem;

    public override void AddItemToGrid(InventorySlot inventorySlot, int x, int y)
    {
        base.AddItemToGrid(inventorySlot, x, y);
        
        inventorySlot.OnDestroy();
    }
}
