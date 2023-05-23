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
    private GameObject[] m_playerList;
    #endregion

    #region PublicMethod
    private void Start()
    {   
        m_quizTimer.SetActive(false);
    }

    private void Update()
    {
        if (isRun)
        {
            m_countTime -= Time.deltaTime;
            m_quizTimer.GetComponent<TMP_Text>().text = Mathf.Round(m_countTime).ToString();

            if(m_countTime < 0f)
            {
                isRun = false;
                m_quizTimer.SetActive(false);
                CheckAnswer();
                m_countTime = m_limitTime;
            }
        }
    }

    public void StartQuiz(GameObject[] _playerList)
    {
        m_quizTimer.SetActive(true);
        isRun = true;
        m_playerList = _playerList;
    }
    #endregion

    #region PrivateMethod
     public void CheckAnswer()
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
    #endregion
}
