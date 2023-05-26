using Photon.Pun;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;


public class PlayerManager : MonoBehaviourPunCallbacks
{
    #region PublicVariables
    public GameObject m_playerListParent;
    public static GameObject[] m_playerList;
    public PhotonView m_PV;
    #endregion

    #region PrivateVariables
    int m_playerCount = 0;
    [SerializeField] private Button m_visitBtn;
    [SerializeField] private GameObject m_visitBtnParent;
    [SerializeField] public GameObject m_plazaObject;
    [SerializeField] public GameObject m_houseObject;
    [SerializeField] private GameObject m_mainCamera; // ?�쏙?�占?�옙 카占?�띰??
    //[SerializeField] private GameObject playerCamera; // ?�시뤄옙?�싱?��? ?�쏙?�占?�옙?�占?카占?�띰??
    [SerializeField] private GameObject m_oxQuiz;
    private GameObject m_isMinePlayer;

    private float m_cameraSpeed = 10f;
    private bool m_mainCameraSetting = false;
    #endregion

    #region PublicMethod
    private void Start()
    {
        m_playerList = new GameObject[20];
        m_PV = GetComponent<PhotonView>();
        //m_visitBtnParent.SetActive(false);

    }

    private void LateUpdate()
    {
        if (m_mainCameraSetting) {
            //Vector3 dir = m_isMinePlayer.transform.position;
            //Vector3 moveVector = new Vector3(dir.x * m_cameraSpeed * Time.deltaTime, dir.y * m_cameraSpeed * Time.deltaTime, 0.0f);
            //m_mainCamera.transform.Translate(dir);

            Vector3 pos = new Vector3(m_isMinePlayer.transform.position.x, m_isMinePlayer.transform.position.y, -10);
            
            m_mainCamera.transform.position = pos;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShowVisitBtn();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ReturnPlaza();
        }

    }

    public void SpawnPlayer()
    {   
        m_isMinePlayer = PhotonNetwork.Instantiate("Prefabs/Test/player", new Vector3(Random.Range(-6f, 19f), 4, 0), Quaternion.identity);
        m_isMinePlayer.name = "player(Clone)" + m_playerCount;
        m_PV.RPC("SpawnPlayerPhoton", RpcTarget.AllBuffered, m_isMinePlayer.GetComponent<PhotonView>().ViewID);

        m_mainCameraSetting = true;
        //GameObject CharacterCamera = Instantiate(playerCamera) as GameObject; // ?�시뤄옙?�싱?�마?�쏙???�쏙?�占?�옙 카占?�띰??
        //CharacterCamera.GetComponent<cameraController>().target = m_isMinePlayer;
        //CharacterCamera.GetComponent<Camera>().depth = 0; // ?�시뤄옙?�싱?�옙 카占?�띰???�占?�옙??

        InitQuiz();
    }

    public void VisitPlayerHouse()
    {
        GameObject clickObj = EventSystem.current.currentSelectedGameObject;
        m_plazaObject.SetActive(false);
        m_houseObject.SetActive(true);

        m_PV.RPC("UpdatePlayerPosIndex", RpcTarget.AllBuffered, m_isMinePlayer.GetComponent<PhotonView>().ViewID , int.Parse(clickObj.name));
    }

    public void ReturnPlaza()
    {
        m_plazaObject.SetActive(true);
        m_houseObject.SetActive(false);

        m_PV.RPC("UpdatePlayerPosIndex", RpcTarget.AllBuffered, m_isMinePlayer.GetComponent<PhotonView>().ViewID, 0);
    }

    public void InitQuiz()
    {
        m_oxQuiz.GetComponent<OXQuiz>().InitQuiz(m_playerList);
    }
    #endregion

    #region PrivateMethod
    [PunRPC]
    private void SpawnPlayerPhoton(int _viewID)
    {
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Player");
        GameObject target = new GameObject();

        foreach(var iter in obj)
        {
            if(_viewID == iter.GetComponent<PhotonView>().ViewID)
            {
                target = iter;
                break;
            }
                
        }

        target.transform.SetParent(m_playerListParent.transform);

        Button _btn = Instantiate(m_visitBtn, new Vector3(Random.Range(-6f, 19f), 4, 0), Quaternion.identity);

        _btn.name = target.GetComponent<PhotonView>().ViewID.ToString();

        _btn.transform.SetParent(m_visitBtnParent.transform);
        _btn.transform.position = new Vector3(Random.Range(-6f, 19f), 4, 0);
        _btn.onClick.AddListener(VisitPlayerHouse);

        //if (PhotonNetwork.IsMasterClient)
        //{
        //    m_playerList[m_playerCount++] = target;
        //}

        target.name = "player(Clone)" + m_playerCount;
        m_playerList[m_playerCount++] = target;


    }

    [PunRPC]
    private void UpdatePlayerPosIndex(int _viewID, int _posIndex)
    {
        foreach (var iter in m_playerList)
        {
            if (_viewID == iter.GetComponent<PhotonView>().ViewID)
            {
                iter.GetComponent<player_controller>().m_playerPosIndex = _posIndex;
                break;
            }
        }

        int cnt = 0;

        _posIndex = m_isMinePlayer.GetComponent<player_controller>().m_playerPosIndex;

        foreach (var iter in m_playerList)
        {
            if (cnt == m_playerCount)
                break;

            cnt++;

            if (_posIndex != iter.GetComponent<player_controller>().m_playerPosIndex)
            {
                iter.SetActive(false);
                continue;
            }

            iter.SetActive(true); 
        }
    }

    private void ShowVisitBtn()
    {
        m_visitBtnParent.SetActive(true);
    }

    
    #endregion  
}
