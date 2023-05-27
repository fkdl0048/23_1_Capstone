using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class farming : MonoBehaviour
{
    private float totaltime = 0.0f;
    private float delaytime = 0.0f;

    public bool timerOn = false;
    public int cnt = 0; // �Ĺ� ����
    public int harvest_cnt = 0; // ��Ȯ�� �Ĺ� ��
    public GameObject[] plants = { null, null, null }; // �Ĺ�
    public int nowPlant;
    public PhotonView PV;

    void Start()
    {
        totaltime = 0.0f;
        PV = GetComponent<PhotonView>();
        cnt = 0;
        harvest_cnt = 0;
        timerOn = false;
    }


    void Update()
    {
        timer();
        for (int i = 0; i < plants.Length; i++)
        {
                if (plants[i] != null && totaltime >= Random.Range(10, 15))
                {
                    PV.RPC("growRPC", RpcTarget.AllBuffered, i);
                    PV.RPC("droopRPC", RpcTarget.AllBuffered, i);
                }
        }
    }

    public void watering()
    {
        if (plants[nowPlant].GetComponent<growPlant>().droop == true) // �ѹ� �õ� �Ŀ� ������
        {
            plants[nowPlant].GetComponent<growPlant>().droop = false;
            plants[nowPlant].GetComponent<Animator>().SetBool("droop", false);
            plants[nowPlant].GetComponent<growPlant>().growReady = true;
            if (plants[nowPlant].GetComponent<growPlant>().growReady)
            { // ����
                plants[nowPlant].GetComponent<growPlant>().level++;
                plants[nowPlant].GetComponent<Animator>().SetInteger("level", plants[nowPlant].GetComponent<growPlant>().level);
                plants[nowPlant].GetComponent<growPlant>().growReady = false;
            }
        }
    }

    public void harvest()
    {
        if (plants[nowPlant].GetComponent<growPlant>().level >= 4 && plants[nowPlant].GetComponent<growPlant>().droop == false) // ���� �Ϸ��� �Ĺ��� ��Ȯ ����
        {
            plants[nowPlant].GetComponent<Animator>().SetBool("harvest", true);
            Destroy(plants[nowPlant], 3);
            plants[nowPlant] = null;
            if(harvest_cnt == plants.Length) // ���ο� �Ĺ��� ���� �翡 ���� �Ĺ��� ��� ��Ȯ�ϰ��� ���� �� ����
            {
                cnt = 0;
                harvest_cnt = 0;
            }
        }     
    }

    void timer()
    {
        if (timerOn)
        {
            totaltime += Time.deltaTime;
        }
    }

    [PunRPC]
    void growRPC(int plantIdx)
    {
            totaltime = 0; // ���� �� �ð� �ʱ�ȭ
            plants[plantIdx].GetComponent<growPlant>().Grow();
    }

    [PunRPC]
    void droopRPC(int plantIdx)
    {
        if (plants[plantIdx].GetComponent<growPlant>().level < 4 && plants[plantIdx].GetComponent<growPlant>().delay) // ���� �ð����� �Ĺ��� �õ��� (�� �ڶ�� �õ��� ����)
        {
            totaltime = 0; // �ð� �ʱ�ȭ
            plants[plantIdx].GetComponent<growPlant>().Droop();
        }
    }
}
