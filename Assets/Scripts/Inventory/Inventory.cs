using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newInventory", menuName = "Inventory/Inventory", order = 0)]
public class Inventory : ScriptableObject
{
    [SerializeField] private string Name;
    [SerializeField] private List<InventorySlot> Slots;
    [SerializeField] private int MaxSize = 100;

    public int Weight;
    public int Size => Slots.Count;
    public bool CanDrag { get; private set; }

    public static Action OnInventoryChanged;

    private void Awake()
    {
        CanDrag = true;
    }

    public void AddItem(ItemObject item)
    {
        // The same as Slots ??= new List<InventorySlot>();
        if (Slots == null)
            Slots = new List<InventorySlot>();

        // Search for a slot which already has the same type of item
        InventorySlot slot = SlotExists(item);

        // If we find it, we increase Amount. Else we create a new InventorySlot
        if (MaxSize > Size)
        {
            if (slot != null)
                slot.AddOne();
            else
            {
                slot = new InventorySlot(item, this);
                Slots.Add(slot);
            }
        }

        Weight += item.Weight;
        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(InventorySlot slot)
    {
        
        Debug.Log("Item removed: " + slot.ItemObject.Name);
        if (slot.Amount > 1)
        {
            slot.RemoveOne();
        }
        else
        {
            Slots.Remove(slot);
        }
        
        Weight += slot.ItemObject.Weight;
        OnInventoryChanged?.Invoke();
    }

    private InventorySlot SlotExists(ItemObject item)
    {
        // Traverse list, if we find it, RETURN item. Else RETURN null
        foreach (var slot in Slots)
        {
            if (slot.CanHold(item))
                return slot;
        }
        Debug.Log(item.Name + " not found");
        return null;
    }

    public InventorySlot GetSlot(int i)
    {
        return Slots[i];
    }

    public void Clear()
    {
        Slots.Clear();
    }

    public bool CanBuy(ItemObject item)
    {
        return Money >= item.Price && (item.Type == InventoryType || InventoryType == ItemType.All);
    }

    public void SetCanDrag(bool bol)
    {
        CanDrag = bol;
    }
}
