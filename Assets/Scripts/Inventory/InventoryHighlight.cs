using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class InventoryHighlight : MonoBehaviour
{
    [SerializeField] private RectTransform highlighter;

    public void Show(bool b)
    {
        highlighter.gameObject.SetActive(b);
    }
    
    public void SetSize(InventorySlot targetItem)
    {
        Vector2 size = new Vector2();
        size.x = targetItem.Width * InventoryUI.TileSizeWidth;
        size.y = targetItem.Height * InventoryUI.TileSizeHeight;

        highlighter.sizeDelta = size;
    }

    public void SetPosition(InventoryUI targetGrid, InventorySlot targetItem)
    {
        var pos = targetGrid.CalculatePositionGrid(targetItem, targetItem.OnGridPositionX, targetItem.OnGridPositionY);

        highlighter.localPosition = pos;
    }

    public void SetParent(InventoryUI targetGrid)
    {
        if (targetGrid ==  null) { return;}
        highlighter.SetParent(targetGrid.GetComponent<RectTransform>());
    }
    
    public void SetPosition(InventoryUI targetGrid, InventorySlot targetItem, int posX, int posY)
    {
        var pos = targetGrid.CalculatePositionGrid(targetItem,posX,posY);

        highlighter.localPosition = pos;
    }
}
