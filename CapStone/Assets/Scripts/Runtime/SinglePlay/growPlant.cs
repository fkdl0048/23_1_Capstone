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
    public int level = 0; // 성장 단계    
    public bool droop = false; // 시듦 상태
    public bool growReady = false; // 성장 준비 완료

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
        if (delay == false) // 씨앗은 시들지 않음, 최초 성장 시 스프라이트 위치 이동
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
