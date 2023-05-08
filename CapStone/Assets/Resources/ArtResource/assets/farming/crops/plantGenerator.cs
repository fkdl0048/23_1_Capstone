using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject plant;
    private float px, py, pz;
    private int cnt = 0; // �Ĺ� ����

    private void Start()
    {
        px = -4.1f; py = 4.07f; pz = 0.086f; // ó�� ��ġ
    }
    public void Planting()
    {
        if (cnt > 2) // ���� ����
            return;
        GameObject pt = Instantiate(plant) as GameObject;
        pt.name = "plant(Clone)" + cnt; // Ŭ�� ���� �ٿ��� ����
        pt.transform.position = new Vector3(px + 2.0f * cnt, py, pz);
        cnt++;
    }
}
