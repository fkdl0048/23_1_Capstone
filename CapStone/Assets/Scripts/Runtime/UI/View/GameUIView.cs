using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIView : UIView
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _CharacterName;
    
    // Start is called before the first frame update
    
    public TextMeshProUGUI MoneyText => _moneyText;
    public TextMeshProUGUI CharacterName => _CharacterName;
    public override void Show()
    {
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }
}
