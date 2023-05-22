using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class growPlant : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator animator;

    private bool delay = false; // ���� ���� �� ��������Ʈ �̵��� ���� ����

    public float totaltime = 0.0f;
    public int level = 0; // ���� �ܰ�    
    public bool droop = false; // �õ� ����
    public bool growReady = false; // ���� �غ� �Ϸ�

    public PhotonView PV;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        PV = GetComponent<PhotonView>();

        droop = false;
        growReady = false;
}

    // Update is called once per frame
    void Update()
    {
        Grow();
        Droop();
    }

    void Grow()
    {
        if (delay == false && totaltime >= Random.Range(10, 30)) // ������ �õ��� ����, ���� ���� �� ��������Ʈ ��ġ �̵�
        {
            //PV.RPC("timeRPC", RpcTarget.AllBuffered, false);
            animator.SetBool("delay", true);
            Vector3 tmp = this.transform.position;
            this.transform.position = new Vector3(tmp.x, tmp.y - 0.2f, tmp.z);
            delay = true;
            totaltime = 0; // ���� �� �ð� �ʱ�ȭ
        }
    }
    void Droop()
    {
        totaltime += Time.deltaTime;
        if (level < 4 && totaltime >= Random.Range(10, 30)) // ���� �ð����� �Ĺ��� �õ��� (�� �ڶ�� �õ��� ����)
        {
            totaltime = 0; // �ð� �ʱ�ȭ
            droop = true;
            animator.SetBool("droop", true);
        }
    }

    [PunRPC]

    void timeRPC(bool reset)
    {
        if (reset)
            totaltime = 0;
    }

}
