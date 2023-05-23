using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    #region PublicVariables

    #endregion

    #region PrivateVariables
    private string m_roomName;
    private string m_playerName;

    [SerializeField] private GameObject m_playerManager;
    #endregion

    #region PublicMethod
    private void Awake()
    {
        PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest(), request =>
        {
            m_playerName = request.PlayerProfile.DisplayName;
        }, errorCallback => Debug.Log("Fail"));

        m_roomName = "Room1";
        print("Yes");
        ConnectPhotonServer();
        Invoke("RequestSpawnPlayer", 3f);
    }



    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LocalPlayer.NickName = m_playerName;
        print("SuccessConnectServer");
    }

    public void JoinLobby() => PhotonNetwork.JoinLobby();

    public override void OnJoinedLobby() => print("SuccessJoinedLobby");

    public void JoinCreateRoom() => PhotonNetwork.JoinOrCreateRoom(m_roomName, new Photon.Realtime.RoomOptions { MaxPlayers = 10 }, null);

    public override void OnCreatedRoom() => print("SuccessCreateRoom");

    public void CreatePhotonRoom()
    {
        PhotonNetwork.CreateRoom(m_roomName, new RoomOptions { MaxPlayers = 10 });
    }

    public void JoinPhotonRoom()
    {
        PhotonNetwork.JoinRoom(m_roomName);
    }

    public override void OnJoinedRoom() => print("SuccessJoinedRoom");

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("FailedCreateRoom");
        JoinPhotonRoom();
    }


    public override void OnJoinRoomFailed(short returnCode, string message) => print("FailJoinedRoom");
    #endregion

    #region PrivateVariables
    private async void ConnectPhotonServer()
    {
        ConnectToServer();
        Invoke("JoinLobby", 1f);
        Invoke("CreatePhotonRoom", 2f);
        //JoinLobby();
        //CreatePhotonRoom();
    }

    private void RequestSpawnPlayer()
    {
        m_playerManager.GetComponent<PlayerManager>().SpawnPlayer();
    }

    #endregion
}
