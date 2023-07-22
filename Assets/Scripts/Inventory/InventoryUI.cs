using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private InventorySlotUI SlotPrefab;
    [SerializeField] private TextMeshProUGUI MoneyText;

    private List<GameObject> slotsShownList = new List<GameObject>();

    public Inventory Inventory => _inventory;

    private void Start()
    {
        Show(_inventory);
    }

    private void OnEnable()
    {
        Inventory.OnInventoryChanged += UpdateInventory;
    }
    
    private void OnDisable()
    {
        Inventory.OnInventoryChanged -= UpdateInventory;
    }

    private void UpdateInventory()
    {
        ClearAll();
        Show(_inventory);
    }

    private void ClearAll()
    {
        foreach (var slots in slotsShownList)
        {
            Destroy(slots);
        }
        slotsShownList.Clear();
    }
    
    public void Show(Inventory inventory)
    {
        for (int i = 0; i < inventory.Size; i++)
        {
            MakeNewEntry(inventory.GetSlot(i));
        }
        MoneyText.text = _inventory.Money + "€";
    }

    private void MakeNewEntry(InventorySlot slot)
    {
        var slotUI = Instantiate(SlotPrefab, Vector3.zero, Quaternion.identity, transform);
        slotUI.SetStuff(slot, _inventory);
        slotsShownList.Add(slotUI.gameObject);
    }
    
    public void SetInventory(Inventory newInv)
    {
        _inventory = newInv;
        UpdateInventory();
    }
}
