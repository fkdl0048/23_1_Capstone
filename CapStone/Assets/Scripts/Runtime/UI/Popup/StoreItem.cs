using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image _itemImage;
    [SerializeField] Image _itemBackgroundImage;
    [SerializeField] Sprite _normalSprite;
    [SerializeField] Sprite _selectedSprite;

    private string _itemName;

    public void SetItem(string itemName)
    {
        _itemName = itemName;
        _itemImage.sprite = Resources.Load<Sprite>("sprite/item/" + _itemName);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Data.AddStoreInventory(_itemName);
        GameManager.Data.SetPlayerMoney(100);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _itemBackgroundImage.sprite = _selectedSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _itemBackgroundImage.sprite = _normalSprite;
    }
}