using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridInteract : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private InventoryController _inventoryController;
    private ItemGrid _itemGrid;
    private void Awake()
    {
        _inventoryController=FindObjectOfType(typeof(InventoryController)) as InventoryController;
        _itemGrid = GetComponent<ItemGrid>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _inventoryController.SelectedItemGrid = _itemGrid;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _inventoryController.SelectedItemGrid = null;
    }
}
