using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItem", menuName = "Inventory/Item", order = 0)]
public class ItemObject : ScriptableObject
{
    public string Name;
    public Sprite ImageUI;
    public Sprite ImageScene;
    public ItemType ItemType;
    public int[] dimensions;
    public int Value;
    public int Weight;
}


