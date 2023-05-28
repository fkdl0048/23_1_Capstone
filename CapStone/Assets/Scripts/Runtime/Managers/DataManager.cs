using System;
using System.Collections.Generic;
using BuildingSystem.Models;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.SharedModels;

public class DataManager
{
    private const string VirtualCurrency = "CH";
    private const string CatalogVersion  = "Item";
    
    public Action OnUpdateMoney;
    
    private Dictionary<string, int> _storeInventory = new Dictionary<string, int>();

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

            }, result =>
            {
                var popup = GameManager.UI.UINavigation.PopupPush("DefalutPopup") as DefalutPopup;
                popup.SetText("구매 성공!");
                OnUpdateMoney?.Invoke();
            }, error =>
            {
                var popup = GameManager.UI.UINavigation.PopupPush("DefalutPopup") as DefalutPopup;
                popup.SetText("구매 실패!");
            });
        }, LogFailure);
    }
    
    // 아이템 판매
    public void SellItem(string itemID, Action SetInventory)
    {
        string itemInstanceId = null;
        int price = 0;
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), result =>
        {
            var inventory = result.Inventory;
            foreach (var item in inventory)
            {
                if (item.ItemId == itemID)
                {
                    itemInstanceId = item.ItemInstanceId;
                    price = (int)item.UnitPrice;
                }
            }

            var request = new ConsumeItemRequest() {ConsumeCount = 1, ItemInstanceId = itemInstanceId};
            PlayFabClientAPI.ConsumeItem(request, result =>
            {

                var popup = GameManager.UI.UINavigation.PopupPush("DefalutPopup") as DefalutPopup;
                popup.SetText("판매 성공!");
                SetPlayerMoney(50);
                OnUpdateMoney?.Invoke();
                SetInventory?.Invoke();
            }, error =>
            {
                var popup = GameManager.UI.UINavigation.PopupPush("DefalutPopup") as DefalutPopup;
                popup.SetText("판매 실패!");
            });
            
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
                OnUpdateMoney?.Invoke();
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
                OnUpdateMoney?.Invoke();
            }, error =>
            {
                Debug.Log(error.ErrorMessage);
            });
        }
    }

    public void GetPlayerMoney(Action<int> onGetPlayerMoney)
    {
        var request = new GetUserInventoryRequest();
        PlayFabClientAPI.GetUserInventory(request, result =>
        {
            Debug.Log("GetUserInventory Success");
            onGetPlayerMoney?.Invoke(result.VirtualCurrency[VirtualCurrency]);
        }, error =>
        {
            Debug.Log(error.ErrorMessage);
            onGetPlayerMoney?.Invoke(0);
        });
    }
    
    public void AddPlayerItem(string itemID)
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
            
            SetPlayerMoney(price);

            PlayFabClientAPI.PurchaseItem(new PurchaseItemRequest
            {
                CatalogVersion = CatalogVersion,
                ItemId = itemID,
                Price = price,
                VirtualCurrency = VirtualCurrency,

            }, result =>
            {
                Debug.Log($"GetItem {result.GetType()}");
            },
            LogFailure);
        }, LogFailure);
    }

    private void LogSuccess(PlayFabResultCommon result) {
        var requestName = result.Request.GetType().Name;
        Debug.Log(requestName + " successful");
    }

    void LogFailure(PlayFabError error) {
        Debug.LogError(error.GenerateErrorReport());
    }
    
    // 인벤 가져오기
    public void GetPlayerItems(Action<GetUserInventoryResult> onGetPlayerItem)
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), result =>
        {
            Debug.Log("GetUserInventory Success");
            onGetPlayerItem?.Invoke(result);
        }, error =>
        {
            Debug.Log(error.ErrorMessage);
            onGetPlayerItem?.Invoke(null);
        });
    }
    
    public void AddStoreInventory(string item)
    {
        if (_storeInventory.ContainsKey(item))
        {
            _storeInventory[item]++;
        }
        else
        {
            _storeInventory.Add(item, 1);
        }
        
        Debug.Log("추가 확인");
    }
    
    public Dictionary<string, int> GetStoreInventory()
    {
        return _storeInventory;
    }
}
