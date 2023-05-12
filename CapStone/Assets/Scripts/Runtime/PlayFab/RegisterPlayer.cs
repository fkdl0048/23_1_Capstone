using TMPro;
using UnityEngine;

public class LoginPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _emailInputField;
    [SerializeField] private TextMeshProUGUI _passwordInputField;
    
    public void Login()
    {
        string email = _emailInputField.text;
        string password = _passwordInputField.text;
        
        GameManager.Playfab.PlayerLogin(email, password);
    }
}
