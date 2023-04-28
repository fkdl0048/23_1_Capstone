using System.Collections;
using System.Collections.Generic;
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

        PlayFabClientAPI.RegisterPlayFabUser(request,  result => GameManager.UI.SetNotiText("회원가입 성공"), error => GameManager.UI.SetWarningText("회원가입 실패"));
    }
    
}
