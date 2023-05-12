using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager
{
    public void Init()
    {
        
    }

    public void PlayerRegister(string email, string password)
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
                GameManager.UI.SetInputTextAndAction("이름을 입력하세요", "이름 등록", PlayerNameRegister);
            },
            error => GameManager.UI.SetWarningText("회원가입 실패")
            );
    }
    
    public void PlayerLogin(string email, string password)
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, result => GameManager.UI.SetNotiText("로그인 성공"), error => GameManager.UI.SetWarningText("로그인 실패"));
    }
    
    public bool PlayerNameRegister(string name)
    {
        bool returnType = false;
        
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = name
        };
        
        PlayFabClientAPI.UpdateUserTitleDisplayName(request,
            result =>
            {
                returnType = true;
                GameManager.UI.SetNotiText("이름 등록 성공");
                
            }, error => GameManager.UI.SetWarningText("이름 등록 실패")
        );

        return returnType;
    }
}
