using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGridAction : MonoBehaviour
{
    public void OnEnable()
    {
        ItemGrid.OnThrowItem += OnThrowItem;
        ItemGrid.OnOnlyOneItem += OnOnlyOneItem;
    }

    private void OnDisable()
    {
        ItemGrid.OnThrowItem -= OnThrowItem;
        ItemGrid.OnOnlyOneItem -= OnOnlyOneItem;
    }

    private void OnThrowItem(InventoryItem item)
    {
        item.OnDestroy();
    }

    private void OnOnlyOneItem()
    {
        
    }
}
