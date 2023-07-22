using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private ItemObject _itemObject;
    [SerializeField] private int _amount;
    private Inventory _inventory;
    public ItemObject ItemObject => _itemObject;
    public Inventory Inventory => _inventory;
    public int Amount => _amount;

    public InventorySlot(ItemObject item, Inventory inventory)
    {
        _itemObject = item;
        _amount = 1;
        _inventory = inventory;
    }
    
    public InventorySlot(ItemObject item, int amount, Inventory inventory)
    {
        _itemObject = item;
        _amount = amount;
        _inventory = inventory;
    }

    public void AddOne()
    {
        _amount++;
    }
    
    public void RemoveOne()
    {
        _amount--;
    }

    public bool CanHold(ItemObject item)
    {
        return item == _itemObject && Amount < item.StackAmount;
    }
}
