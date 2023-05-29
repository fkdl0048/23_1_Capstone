using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    private UINavigation _uiNavigation;
    private GameUIView _gameUIView;
    // Start is called before the first frame update
    void Start()
    {
        _uiNavigation = new UINavigation();
        _gameUIView = _uiNavigation.UIViewPush("GameView") as GameUIView;
       
        GameManager.Data.GetPlayerMoney(money => { _gameUIView.MoneyText.text = money.ToString(); });

        GameManager.Data.OnUpdateMoney += () =>
        {
            GameManager.Data.GetPlayerMoney(money => { _gameUIView.MoneyText.text = money.ToString(); });
        };
        
        
        PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest(), result =>
        {
            _gameUIView.CharacterName.text = result.PlayerProfile.DisplayName;
        }, error => Debug.LogWarning("불러오기 실패"));
    }
    
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            var popup = GameManager.UI.UINavigation.PopupPush("InventoryPopup") as InventoryPopup;
        }
    }
}
