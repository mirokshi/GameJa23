using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItem", menuName = "Inventory/Item", order = 0)]
public class ItemObject : ScriptableObject
{
    public string itemName;             // Nombre del objeto
    public Sprite itemIconUI;            // Icono del objeto a mostrar en el inventario
    public Sprite itemIcon;             // Objeto en el mundo 
    public int width;                  // Ancho del objeto en slots
    public int height;                 // Alto del objeto en slots

    
}


