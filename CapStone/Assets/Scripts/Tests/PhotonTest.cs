using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class PhotonTest : MonoBehaviourPunCallbacks
{
    #region PrivateVariables

    private string m_playerName = "random";
    #endregion

    #region Protected Variables
    #endregion

    #region PublicVariables
    public TMP_InputField m_roomName;
    public TMP_InputField m_nickName;
    public Button m_spawnPlayerBtn;
    #endregion

     #region PublicMethod
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public async void LoginToPhotonServer(string _playerName)
    {
        m_playerName = _playerName;
        await ConnectToServer();
        await Task.Delay(1000);
        await JoinLobby();
        await Task.Delay(1000);
        await JoinCreateRoom();
        
    }

    public Task ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();

        return Task.Delay(1000);
    }

    public override void OnConnectedToMaster()
    {
        print("�������ӿϷ�");
        PhotonNetwork.LocalPlayer.NickName = m_playerName;
    }

    public Task JoinLobby()
    {
        PhotonNetwork.JoinLobby();
        
        return Task.Delay(1000);
    }

    public override void OnJoinedLobby() => print("�κ����ӿϷ�");

    public void CreatePhotonRoom()
    {
        PhotonNetwork.CreateRoom(m_roomName.text, new RoomOptions {MaxPlayers = 10 });
    }

    public void JoinPhotonRoom()
    {
        PhotonNetwork.JoinRoom(m_roomName.text);
    }

    public Task JoinCreateRoom()
    {
        PhotonNetwork.JoinOrCreateRoom("Room1", new RoomOptions { MaxPlayers = 10 }, null);
        
        return Task.Delay(1000);
    }

    public override void OnCreatedRoom() => print("�游���Ϸ�");

    public override void OnJoinedRoom()
    {
        print("�������Ϸ�");
        m_spawnPlayerBtn.gameObject.SetActive(true);   
    }

    public override void OnCreateRoomFailed(short returnCode, string message) => print("�游������");

    public override void OnJoinRoomFailed(short returnCode, string message) => print("����������");
   

    #endregion

    #region PrivateMethod

    #endregion

    #region ProtectedMethod
    #endregion
}
