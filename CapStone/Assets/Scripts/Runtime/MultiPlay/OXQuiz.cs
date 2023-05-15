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
                //CheckAnswer();
                m_countTime = m_limitTime;
            }
        }
    }

    public void StartQuiz()
    {
        m_quizTimer.SetActive(true);
        isRun = true;
    }
    #endregion

    #region PrivateMethod
     public void CheckAnswer(GameObject[] _playerList)
     {   
        foreach (var iter in _playerList){
            if(iter.GetComponent<player_controller>().m_quizState != m_answer)
            {
                iter.transform.position = Vector3.zero;
                Debug.Log("catch");
            }
        }
     }
    #endregion
}