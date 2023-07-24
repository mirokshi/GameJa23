using System;
using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "itemName",menuName = "Object/Item", order = 0)]
public class ItemData : ScriptableObject
{
    public ObstacleType obstacleType;
    public ItemType itemType;
    public new string name;
    public int width = 1;
    public int height =1;
    
    public int peso = 1;
    public int valor = 1;
    
    public Sprite itemIcon;
    
}
