using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image _itemImage;
    [SerializeField] TextMeshProUGUI _itemCountText;

    private string _itemName;
    private Action _updateInventory;
    
    public void SetItem(string itemName, int count, Action UpdateInventory)
    {
        _itemName = itemName;
        _itemImage.sprite = Resources.Load<Sprite>("sprite/item/" + _itemName);
        _itemCountText.text = count.ToString();
        _updateInventory = UpdateInventory;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Data.SellItem(_itemName, _updateInventory);
    }
}
