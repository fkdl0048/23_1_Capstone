using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _emailInputField;
    [SerializeField] private TextMeshProUGUI _passwordInputField;
    
    public void Register()
    {
        string email = _emailInputField.text;
        string password = _passwordInputField.text;
        
        GameManager.Playfab.PlayerRegister(email, password);
    }
}
