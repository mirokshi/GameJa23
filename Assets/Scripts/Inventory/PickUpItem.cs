using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public static Action<InventoryItem> OnPickUpItem;

    private InventoryItem _inventoryItem;

    private void Awake()
    {
        _inventoryItem = GetComponent<InventoryItem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            OnPickUpItem?.Invoke(_inventoryItem);
            Destroy(gameObject);
        }
    }
}
