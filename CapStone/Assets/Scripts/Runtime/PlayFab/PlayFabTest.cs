using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class PlayFabTest : MonoBehaviour
{
    public TextMeshProUGUI EmailInput, PasswordInput, UsernameInput;

    public void LoginTest()
    {
        var request = new LoginWithEmailAddressRequest() { Email = EmailInput.text, Password = PasswordInput.text};
        PlayFabClientAPI.LoginWithEmailAddress(request, (e) => Debug.Log($"{e.LastLoginTime} 로그인 성공") , error => Debug.LogError(error.GenerateErrorReport()));
    }

    public void ResisterTest()
    {
        var request = new RegisterPlayFabUserRequest() {Email = EmailInput.text, Password = PasswordInput.text, Username = "abcasds"};
        PlayFabClientAPI.RegisterPlayFabUser(request, result => Debug.Log($" 회원가입 성공"), error => Debug.LogError(error.GenerateErrorReport()));
    }
}
