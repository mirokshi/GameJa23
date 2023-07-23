using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public static Action<InventoryItem> OnPickUpItem;

    private InventoryItem _inventoryItem;

    public LayerMask objectName;

    private void Awake()
    {
        _inventoryItem = GetComponent<InventoryItem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Item Triggered");
            Destroy(gameObject);
            OnPickUpItem?.Invoke(_inventoryItem);
        }
    }
}
