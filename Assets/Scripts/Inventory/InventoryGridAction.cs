using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGridAction : MonoBehaviour
{
    public void OnEnable()
    {
        ItemGrid.OnThrowItem += OnThrowItem;
    }

    private void OnDisable()
    {
        ItemGrid.OnThrowItem -= OnThrowItem;
    }

    private void OnThrowItem(InventoryItem item)
    {
        item.OnDestroy();
    }
}
