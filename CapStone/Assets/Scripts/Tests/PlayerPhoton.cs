using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhoton : MonoBehaviourPunCallbacks
{
    #region PublicVariables
    public PhotonView m_photonView;
    public int m_photonViewID;
    public int cnt = 0;
    #endregion

    #region PrivateVariables
    private int m_requestWatchPhotonViewID;
    private List<int> m_photonViewIDs = new List<int>();
    #endregion

    #region Protected Variables
    #endregion

    #region PublicMethod
    private void Start()
    {
            m_photonViewID = m_photonView.ViewID;
            m_photonViewIDs.Add(m_photonViewID);
    }

    private void Update()
    {
        if (m_photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                RequestHouseWatch();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                ShowViewIDsList();
            }
        }
    }

    public void RequestHouseWatch()
    {
        SetRequestWatchPhotonViewID();

         m_photonView.RPC("CheckRequestHouseWatch", RpcTarget.AllBuffered, m_requestWatchPhotonViewID, m_photonViewID);

    }
    #endregion

    #region PrivateMethod
    private void SetRequestWatchPhotonViewID()
    {
        m_requestWatchPhotonViewID = 1001;
    }

    [PunRPC]
    private void CheckRequestHouseWatch(int _requestWatchPhotonViewID, int _requestViewID)
    {
        cnt++;
        if (m_photonView.ViewID == _requestWatchPhotonViewID)
        {
            m_photonViewIDs.Add(_requestViewID);
            Debug.Log(m_photonViewIDs[0]);
            Debug.Log(m_photonViewIDs[1]);
        }
    }

    private void ShowViewIDsList()
    {
        Debug.Log(cnt);
    }
    #endregion

    #region ProtectedMethod
    #endregion
}
