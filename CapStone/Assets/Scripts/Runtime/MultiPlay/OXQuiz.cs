using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using PlayFab.ClientModels;
using TMPro;

public class OXQuiz : MonoBehaviourPunCallbacks
{
    #region PublicVariables
    public GameObject m_quizTimer;
    #endregion

    #region PrivateVariables
    private float m_limitTime = 5f;
    private float m_countTime = 5f;
    private bool isRun = false;
    private int m_answer = 1;
    private PhotonView m_PV;
    private static GameObject[] m_playerList;
    private TextMeshProUGUI m_quizTimerText;
    #endregion

    #region PublicMethod
    private void Start()
    {
        GameObject sub = Resources.Load("Prefabs/Test/OXQuiz/QuizTimer") as GameObject;
        m_quizTimer = Instantiate(sub);
        m_quizTimer.SetActive(false);
        m_PV = this.GetComponent<PhotonView>();
        
        m_quizTimerText = m_quizTimer.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (isRun)
        {   
            m_countTime -= Time.deltaTime;
            
            m_quizTimerText.text = Mathf.Round(m_countTime).ToString();

            //TO-DO : Show OX Problem UI
            

            if(m_countTime < 0f)
            {
                isRun = false;


                StartCoroutine(SetAnswer());

                //TO-DO : Set m_answer code : O == 1, X == 0;

                CheckAnswer();
                m_countTime = m_limitTime;
            }
        }
    }
    
    private IEnumerator SetAnswer()
    {
        m_quizTimerText.text = m_answer == 1 ? "O" : "X";
        yield return new WaitForSeconds(2f);
        m_quizTimer.SetActive(false);
    }

    public void InitQuiz(GameObject[] _playerList)
    {
        m_playerList = _playerList;
    }

    public void StartQuiz()
    {
        m_PV.RPC("RPC_StartQuiz", RpcTarget.AllBuffered);
    }
    #endregion

    #region PrivateMethod
     private void CheckAnswer()
     {   
        foreach (var iter in m_playerList){
            if (iter == null)
                break;

            if (iter.GetComponent<player_controller>().m_quizState == m_answer)
            {
                Debug.Log("Correct!");
            }
            else if (iter.GetComponent<player_controller>().m_quizState == 0)
            {
                Debug.Log("Not Participate in");
            }
            else
            {
                iter.transform.position = Vector3.zero;
                Debug.Log("Incorrect!");
            }
        }
     }

    [PunRPC]
    private void RPC_StartQuiz()
    {
        if (m_playerList == null)
            return;

        foreach (var iter in m_playerList)
        {
            if (iter == null)
                break;

            if (iter.GetComponent<player_controller>().m_quizState != 0)
            {
                m_quizTimer.SetActive(true);
                isRun = true;
            }
        }
    }
    #endregion
}
