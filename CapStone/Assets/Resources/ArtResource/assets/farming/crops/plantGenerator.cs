using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject plant;
    private float px, py, pz;
    private int cnt = 0; // 식물 개수

    private void Start()
    {
        px = -4.1f; py = 4.07f; pz = 0.086f; // 처음 위치
    }
    public void Planting()
    {
        if (cnt > 2) // 개수 제한
            return;
        GameObject pt = Instantiate(plant) as GameObject;
        pt.name = "plant(Clone)" + cnt; // 클론 숫자 붙여서 구분
        pt.transform.position = new Vector3(px + 2.0f * cnt, py, pz);
        cnt++;
    }
}
