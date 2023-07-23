using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "itemName",menuName = "Item", order = 0)]
public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public int width = 1;
    public int height =1;
    
    public int peso = 1;
    public int valor = 1;
    
    public Sprite itemIcon;
}
