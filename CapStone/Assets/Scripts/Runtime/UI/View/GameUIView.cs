using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIView : UIView
{
    [SerializeField] private Button _testButyButton;
    [SerializeField] private Button _testsellButton;
    [SerializeField] private TextMeshProUGUI _moneyText;
    
    // Start is called before the first frame update
    
    public Button TestButyButton => _testButyButton;
    public Button TestSellButton => _testsellButton;
    public TextMeshProUGUI MoneyText => _moneyText;
    public override void Show()
    {
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }
}
