using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private UINavigation _uiNavigation;
    private MainMenuView _mainMenuView;
    
    private void Start()
    {
        // initialize the UI Navigation
        _uiNavigation = new UINavigation();
        _mainMenuView = _uiNavigation.UIViewPush("MainMenuView") as MainMenuView;
        
        _mainMenuView.LoginButton.onClick.AddListener(() =>
        {
            PlayerLogin(_mainMenuView.EmailInput.text, _mainMenuView.PasswordInput.text);
        });
        
        _mainMenuView.RegisterButton.onClick.AddListener(() =>
        {
            PlayerRegister(_mainMenuView.EmailInput.text, _mainMenuView.PasswordInput.text);
        });
    }

    private void OnDisable()
    {
        // remove listeners from the buttons
        _mainMenuView.LoginButton.onClick.RemoveAllListeners();
        _mainMenuView.RegisterButton.onClick.RemoveAllListeners();
    }
    
    private void PlayerRegister(string email, string password)
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = email,
            Password = password,
            RequireBothUsernameAndEmail = false
        };
        
        PlayFabClientAPI.RegisterPlayFabUser(request,
            result =>
            {
                var popup = _uiNavigation.PopupPush("InputPopup") as InputPopup;
                popup.SetInputPopup("이름을 입력하세요", "이름 등록", PlayerNameRegister);
                
                //GameManager.UI.SetInputTextAndAction("이름을 입력하세요", "이름 등록", PlayerNameRegister);
            },
            error =>
            {
                var popup = _uiNavigation.PopupPush("DefalutPopup") as DefalutPopup;
                popup.SetText("회원가입 실패!");
            });
    }
    
    public void PlayerNameRegister(string name)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = name
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request,
            result =>
            {
                // 이동
                Debug.Log("씬 이동");
            },
            error =>
            {
                var popup = _uiNavigation.PopupPush("DefalutPopup") as DefalutPopup;
                popup.SetText("중복된 닉네임이 있습니다!");
            });
    }
    
    public void PlayerLogin(string email, string password)
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password
        };

        PlayFabClientAPI.LoginWithEmailAddress(request,
            result =>
            {
                var popup = _uiNavigation.PopupPush("DefalutPopup") as DefalutPopup;
                popup.SetText("로그인 성공!");
            }, error =>
            {
                var popup = _uiNavigation.PopupPush("DefalutPopup") as DefalutPopup;
                popup.SetText("로그인 실패!");
            });
    }
}
