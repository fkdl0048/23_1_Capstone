using System;
using System.Collections.Generic;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPopup : UIView
{
    [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;
    [SerializeField] private HorizontalLayoutGroup _gridLayoutGroup;
    
    private List<InventoryItem> _inventoryItems = new List<InventoryItem>();
    public void OnEnable()
    {
        UpdateInventory();
        
    }

    private void Start()
    {
        StoreInitItems("bed");
        StoreInitItems("bird house");
        StoreInitItems("bookshelf");
        StoreInitItems("chair");
        StoreInitItems("kitchen table");
        StoreInitItems("refrigerator");
        StoreInitItems("sofa");
        StoreInitItems("table");
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
        inventoryItem.SetItem(item.ItemId, item.RemainingUses.Value, UpdateInventory);

        _inventoryItems.Add(inventoryItem);
    }
    
    private void StoreInitItems(string storeItemID)
    {
        var storeItem = GameManager.Resource.Load<StoreItem>("Prefabs/UI/StoreItem");
        storeItem = Instantiate(storeItem, _gridLayoutGroup.transform);
        storeItem.SetItem(storeItemID);
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
