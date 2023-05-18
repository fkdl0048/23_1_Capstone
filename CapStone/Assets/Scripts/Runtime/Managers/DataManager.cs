using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.EconomyModels;
using PlayFab.SharedModels;

public class DataManager
{
    private const string VirtualCurrency = "CH";
    private const string CatalogVersion  = "Item";
    
    // private async Task<int> GetItemPrice(string itmeID)
    // {
    //     int price = 0;
    //     
    //     var catalogItem = await PlayFabClientAPI.GetCatalogItemsAsync(new GetCatalogItemsRequest());
    //
    //     
    //     Debug.Log("가격 !!" + price);
    //
    //     return price;
    // }
    
    // 인벤토리에 아이템 추가, 삭제, 해당 아이템 리스트 가져오기 등등 필요

    // 아이템 구매
    public void BuyItem(string itemID)
    {
        int price = 0;
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest(), result =>
        {
            var catalogItem = result.Catalog;
            foreach (var item in catalogItem)
            {
                if (item.ItemId == itemID)
                {
                    price = (int)item.VirtualCurrencyPrices[VirtualCurrency];
                }
            }
            
            PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest
            {
                CatalogVersion = CatalogVersion,
                ItemId = itemID,
                Price = price,
                VirtualCurrency = VirtualCurrency,
            
            }, LogSuccess, LogFailure);
        }, LogFailure);
    }
    
    
    public void SetPlayerMoney(int money)
    {
        if (money >= 0)
        {
            var request = new AddUserVirtualCurrencyRequest
            {
                VirtualCurrency = VirtualCurrency,
                Amount = money
            };
            PlayFabClientAPI.AddUserVirtualCurrency(request, result =>
            {
                Debug.Log("AddUserVirtualCurrency Success");
            }, error =>
            {
                Debug.Log(error.ErrorMessage);
            });
        }
        else
        {
            var request = new SubtractUserVirtualCurrencyRequest
            {
                VirtualCurrency = VirtualCurrency,
                Amount = money
            };
        
            PlayFabClientAPI.SubtractUserVirtualCurrency(request, result =>
            {
                Debug.Log("SubtractUserVirtualCurrency Success");
            }, error =>
            {
                Debug.Log(error.ErrorMessage);
            });
        }
    }
    
    private void LogSuccess(PlayFabResultCommon result) {
        var requestName = result.Request.GetType().Name;
        Debug.Log(requestName + " successful");
    }

    void LogFailure(PlayFabError error) {
        Debug.LogError(error.GenerateErrorReport());
    }
}
