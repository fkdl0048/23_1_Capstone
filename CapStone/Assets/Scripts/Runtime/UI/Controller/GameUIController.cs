using System.Collections;
using System.Collections.Generic;
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
        
        _gameUIView.TestButyButton.onClick.AddListener(() =>
        {
            GameManager.Data.BuyItem("HardWood");
        });
        
        _gameUIView.TestSellButton.onClick.AddListener(() =>
        {
            GameManager.Data.SellItem("Fish");
        });
        
        GameManager.Data.GetPlayerMoney(money => { _gameUIView.MoneyText.text = money.ToString(); });

        GameManager.Data.OnUpdateMoney += () =>
        {
            GameManager.Data.GetPlayerMoney(money => { _gameUIView.MoneyText.text = money.ToString(); });
        };
        
        //_gameUIView.MoneyText.text = 
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
