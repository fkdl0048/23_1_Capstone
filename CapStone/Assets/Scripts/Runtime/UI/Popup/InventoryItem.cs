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
    
    public void SetItem(string itemName, int count)
    {
        _itemImage.sprite = Resources.Load<Sprite>("sprite/item/" + itemName);
        _itemCountText.text = count.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Check");
    }
}
