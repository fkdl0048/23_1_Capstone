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
            // ó�� ��ġ
            px = farm.GetComponent<BoxCollider2D>().offset.x - 28.9f;
            py = farm.GetComponent<BoxCollider2D>().offset.y - 17.7f;
            pz = 0f;
            farmSet = false;
        }
    }

    public void Planting()
    {
        print("���� ���� �� = " + farm.GetComponent<farming>().cnt);
        if (farm.GetComponent<farming>().cnt > 14) return; // ���� ����
        GameObject pt = Instantiate(plant, new Vector3(px + 2.0f * farm.GetComponent<farming>().cnt, py, pz), Quaternion.identity);
        pt.name = "plant(Clone)" + farm.GetComponent<farming>().cnt; // Ŭ�� ���� �ٿ��� ����
        farm.GetComponent<farming>().plants[farm.GetComponent<farming>().cnt] = pt;
        farm.GetComponent<farming>().cnt++;
        farm.GetComponent<farming>().timerOn = true; // �Ĺ��� �ɾ��� �Ŀ� Ÿ�̸� �۵�
    }

}
