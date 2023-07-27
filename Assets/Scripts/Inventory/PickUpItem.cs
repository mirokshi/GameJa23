using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public static Action<InventorySlot> OnPickUpItem;

    private InventorySlot _inventoryItem;

    private void Awake()
    {
        _inventoryItem = gameObject.GetComponent<InventorySlot>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Pick Up Item");
            OnPickUpItem?.Invoke(_inventoryItem);
            Destroy(gameObject);
        }
    }
}
