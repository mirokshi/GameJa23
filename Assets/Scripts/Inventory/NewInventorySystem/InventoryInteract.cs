using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private InventoryManager _inventoryManager;
    private InventoryUI _inventoryUI;
    private void Awake()
    {
        _inventoryManager = FindObjectOfType(typeof(InventoryManager)) as InventoryManager;
        _inventoryUI = GetComponent<InventoryUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _inventoryManager.SelectedInventory = _inventoryUI;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _inventoryManager.SelectedInventory = null;
    }
}
