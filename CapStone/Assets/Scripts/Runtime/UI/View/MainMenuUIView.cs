using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIView : UIView
{
    [SerializeField] private TextMeshProUGUI _emailInput;
    [SerializeField] private TextMeshProUGUI _passwordInput;
    [SerializeField] private Button _loginButton;
    [SerializeField] private Button _registerButton;
    
    public TextMeshProUGUI EmailInput => _emailInput;
    public TextMeshProUGUI PasswordInput => _passwordInput;
    public Button LoginButton => _loginButton;
    public Button RegisterButton => _registerButton;

    public override void Show()
    {
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }
}
