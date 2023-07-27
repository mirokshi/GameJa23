using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "inventoryName",menuName = "Inventory/Player", order = 0)]
public class InventoryPlayer : Inventory
{
    private float _totalWeight;
    private int _totalValue;

    protected override void OnEnable()
    {
        base.OnEnable();
        _totalWeight = 0;
        _totalValue = 0;
    }
    
    public override void RemoveItemFromGrid(InventorySlot inventorySlot)
    {
        base.RemoveItemFromGrid(inventorySlot);
        SetValues(-inventorySlot.ItemData.weight, -inventorySlot.ItemData.value);
    }
    
    public override void AddItemToGrid(InventorySlot inventorySlot, int x, int y)
    {
        base.AddItemToGrid(inventorySlot, x, y);
    }
    
    public float GetTotalWeight()
    {
        Debug.Log("Player weight: " + _totalWeight);
        if (_totalWeight == 0)
        {
            return 1;
        }
        
        return _totalWeight;
    }

    public int GetTotalValue()
    {
        return _totalValue;
    }

    public void SetValues(int weight, int value)
    {
        _totalWeight += weight;
        _totalValue += value;
    }
}
