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
    public int cnt = 0; // 식물 개수
    public int harvest_cnt = 0; // 수확한 식물 수
    public GameObject[] plants = { null, null, null }; // 식물
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
        if (plants[nowPlant].GetComponent<growPlant>().droop == true) // 한번 시든 후에 성장함
        {
            plants[nowPlant].GetComponent<growPlant>().droop = false;
            plants[nowPlant].GetComponent<Animator>().SetBool("droop", false);
            plants[nowPlant].GetComponent<growPlant>().growReady = true;
            if (plants[nowPlant].GetComponent<growPlant>().growReady)
            { // 성장
                plants[nowPlant].GetComponent<growPlant>().level++;
                plants[nowPlant].GetComponent<Animator>().SetInteger("level", plants[nowPlant].GetComponent<growPlant>().level);
                plants[nowPlant].GetComponent<growPlant>().growReady = false;
            }
        }
    }

    public void harvest()
    {
        if (plants[nowPlant].GetComponent<growPlant>().level >= 4 && plants[nowPlant].GetComponent<growPlant>().droop == false) // 성장 완료인 식물은 수확 가능
        {
            plants[nowPlant].GetComponent<Animator>().SetBool("harvest", true);
            Destroy(plants[nowPlant], 3);
            plants[nowPlant] = null;
            if(harvest_cnt == plants.Length) // 새로운 식물은 현재 밭에 심은 식물을 모두 수확하고나서 심을 수 있음
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
            totaltime = 0; // 성장 시 시간 초기화
            plants[plantIdx].GetComponent<growPlant>().Grow();
    }

    [PunRPC]
    void droopRPC(int plantIdx)
    {
        if (plants[plantIdx].GetComponent<growPlant>().level < 4 && plants[plantIdx].GetComponent<growPlant>().delay) // 일정 시간마다 식물이 시들음 (다 자라면 시들지 않음)
        {
            totaltime = 0; // 시간 초기화
            plants[plantIdx].GetComponent<growPlant>().Droop();
        }
    }
}
