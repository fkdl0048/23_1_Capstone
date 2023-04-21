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
        base.OnConnectedToMaster();
        PhotonNetwork.LocalPlayer.NickName = m_nickName.text;
    }

    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }

    public void CreatePhotonRoom()
    {
        PhotonNetwork.CreateRoom(m_roomName.text, new RoomOptions {MaxPlayers = 20 });
    }

    public void JoinPhotonRoom()
    {
        PhotonNetwork.JoinRoom(m_roomName.text);
    }
    #endregion
    #region PrivateMethod

    #endregion
    #region ProtectedMethod
    #endregion



}
