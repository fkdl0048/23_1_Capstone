using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class PhotonTest : MonoBehaviourPunCallbacks
{


    #region PrivateVariables

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

    public void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("서버접속완료");
        PhotonNetwork.LocalPlayer.NickName = m_nickName.text;
    }

    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() => print("로비접속완료");

    public void CreatePhotonRoom()
    {
        PhotonNetwork.CreateRoom(m_roomName.text, new RoomOptions {MaxPlayers = 10 });
    }

    public void JoinPhotonRoom()
    {
        PhotonNetwork.JoinRoom(m_roomName.text);

    }

    public override void OnCreatedRoom() => print("방만들기완료");

    public override void OnJoinedRoom()
    {
        print("방참가완료");
        m_spawnPlayerBtn.gameObject.SetActive(true);   
    }

    public override void OnCreateRoomFailed(short returnCode, string message) => print("방만들기실패");

    public override void OnJoinRoomFailed(short returnCode, string message) => print("방참가실패");


    public void SpawnPlayer()
    {
        PhotonNetwork.Instantiate("PhotonTest/Player", new Vector3(Random.Range(-6f, 19f), 4, 0), Quaternion.identity);
    }
    #endregion
    #region PrivateMethod

    #endregion
    #region ProtectedMethod
    #endregion



}
