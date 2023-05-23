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
        ConnectToServer();
        await Task.Delay(1000);
        JoinLobby();
        await Task.Delay(1000);
        JoinCreateRoom();
        
    }

    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("�������ӿϷ�");
        PhotonNetwork.LocalPlayer.NickName = m_playerName;
    }

    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
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

    public void JoinCreateRoom()
    {
        PhotonNetwork.JoinOrCreateRoom("Room1", new RoomOptions { MaxPlayers = 10 }, null);
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
