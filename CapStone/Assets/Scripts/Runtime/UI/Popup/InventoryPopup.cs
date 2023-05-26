using System;
using System.Collections.Generic;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPopup : UIView
{
    [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;
    
    private List<InventoryItem> _inventoryItems = new List<InventoryItem>();
    public void OnEnable()
    {
        UpdateInventory();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.gameObject.SetActive(false);
        }
    }

    private void InitItems(GetUserInventoryResult result)
    {
        ClearInventory();
        
        foreach (var data in result.Inventory)
        {
            InitItem(data);
        }
    }
    
    private void InitItem(ItemInstance item)
    {
        var inventoryItem = GameManager.Resource.Load<InventoryItem>("Prefabs/UI/InventoryItem");
        inventoryItem = Instantiate(inventoryItem, _horizontalLayoutGroup.transform);
        inventoryItem.SetItem(item.ItemId, item.RemainingUses.Value);

        _inventoryItems.Add(inventoryItem);
    }

    private void OnDisable()
    {
        ClearInventory();
    }
    
    private void ClearInventory()
    {
        for (int i = _inventoryItems.Count - 1; i >= 0; i--)
        {
            Destroy(_inventoryItems[i].gameObject);
        }
        _inventoryItems.Clear();
    }
    
    public void UpdateInventory()
    {
        GameManager.Data.GetPlayerItems(InitItems);
    }
}
