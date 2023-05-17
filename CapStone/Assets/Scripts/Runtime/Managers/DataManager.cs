using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.EconomyModels;

public class DataManager
{
    private int GetItemPrice(string itmeID)
    {
        int price = 0;
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest(), result =>
        {
            Debug.Log($"아이템 이름 : {itmeID}");
            var catalogItem = result.Catalog;
            foreach (var item in catalogItem)
            {
                if (item.ItemId == itmeID)
                {
                    Debug.Log($"현재 : {item.ItemId }");
                    price = (int)item.VirtualCurrencyPrices["RM"];
                    
                }
            }
        }, error =>
        {
            Debug.Log(error.ErrorMessage);
        });
        
        Debug.Log("가격 !!" + price);

        return price;
    }

    // 아이템 구매
    public void GetItem(string itemID)
    {
        var request = new PurchaseItemRequest
        {
            ItemId = itemID,
            VirtualCurrency = "CH",
            Price = GetItemPrice(itemID)
        };
        
        Debug.Log("Price" + request.Price);
        Debug.Log("VirtualCurrency" + request.VirtualCurrency);
        
        PlayFabClientAPI.PurchaseItem(request, result =>
        {
            Debug.Log("PurchaseItem Success");
        }, error =>
        {
            Debug.Log(error.ErrorMessage);
        });
    }
}
