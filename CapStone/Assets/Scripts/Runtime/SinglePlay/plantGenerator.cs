using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class plantGenerator : MonoBehaviour
{
    public GameObject farm;
    public bool farmSet;

    [SerializeField]
    private GameObject plant;
    private float px, py, pz;

    void Start()
    {
        farmSet = false;
    }

    void Update()
    {
        if (farmSet)
        {
            // 처음 위치
            px = farm.transform.position.x - 2.0f;
            py = farm.transform.position.y;
            pz = 0f;
            farmSet = false;
        }
    }

    public void Planting()
    {
        print("현재 씨앗 수 = " + farm.GetComponent<farming>().cnt);
        if (farm.GetComponent<farming>().cnt > 2) return; // 개수 제한
        GameObject pt = Instantiate(plant, new Vector3(px + 2.0f * farm.GetComponent<farming>().cnt, py, pz), Quaternion.identity);
        pt.name = "plant(Clone)" + farm.GetComponent<farming>().cnt; // 클론 숫자 붙여서 구분
        farm.GetComponent<farming>().plants[farm.GetComponent<farming>().cnt] = pt;
        farm.GetComponent<farming>().cnt++;
        farm.GetComponent<farming>().timerOn = true; // 식물이 심어진 후에 타이머 작동
    }

}
