using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private ItemObject _itemObject;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _amount;
    [SerializeField] private TextMeshProUGUI _priceText;

    private InventorySlot _slot;
    private Inventory _inventory;
    private Transform _mama;
    private Canvas _canvas;
    private GraphicRaycaster _graphicRaycaster;
    private int _siblingIndex;
    
    public void SetStuff(InventorySlot slot, Inventory inventory)
    {
        _slot = slot;
        _image.sprite = slot.ItemObject.ImageUI;
        _amount.text = slot.Amount.ToString();
        _priceText.text = slot.ItemObject.Value + "€";
        _itemObject = slot.ItemObject;
        _inventory = inventory;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _mama = transform.parent;
        // Save position where it was
        _siblingIndex = transform.GetSiblingIndex();
        // Start moving object from beginning
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0) / transform.lossyScale.x;
        
        // We need few references from UI
        if (!_canvas)
        {
            _canvas = GetComponentInParent<Canvas>();
            _graphicRaycaster = _canvas.GetComponent<GraphicRaycaster>();
        }

        // Change parent of our item to the canvas
        transform.SetParent(_canvas.transform);
        
        // And set it as the last child to be rendered on top of UI
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Continue moving objects around screen
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0) / transform.lossyScale.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Find object in Canvas
        var result = new List<RaycastResult>();
        _graphicRaycaster.Raycast(eventData, result);
        foreach (var hit in result)
        {
            Debug.Log(hit.gameObject.name);
            var inventory = hit.gameObject.GetComponent<InventoryUI>();
            if (inventory != null)
            {
                Debug.Log("Can drag is" + _inventory.CanDrag);
                Debug.Log("Can buy is"+ inventory.Inventory.CanBuy(_itemObject));
                if (_inventory.CanDrag && inventory.Inventory.CanBuy(_itemObject))
                {
                    inventory.Inventory.AddItem(_itemObject);
                    _inventory.RemoveItem(_slot);
                    transform.SetParent(_canvas.transform);
                    Destroy(transform.gameObject);
                }
            }
        }
        // Find scene objects
        RaycastHit2D hitData = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
        
        Debug.Log("HitData " + hitData.transform);
        
        if (hitData)
        {
            Debug.Log(hitData.transform);
        }

        // Return item into inventory
        transform.SetParent(_mama.transform);
        // Return item in the same position it was
        transform.SetSiblingIndex(_siblingIndex);
    }
}
