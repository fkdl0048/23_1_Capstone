using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class growPlant : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator animator;

    public bool delay = false;
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

        delay = false;
        droop = false;
        growReady = false;
}


    public void Grow()
    {
        if (delay == false) // ������ �õ��� ����, ���� ���� �� ��������Ʈ ��ġ �̵�
        {
            animator.SetBool("delay", true);
            Vector3 tmp = this.transform.position;
            this.transform.position = new Vector3(tmp.x, tmp.y - 0.2f, tmp.z);
            delay = true;
        }
    }
    public void Droop()
    {          
            droop = true;
            animator.SetBool("droop", true);
    }

}
