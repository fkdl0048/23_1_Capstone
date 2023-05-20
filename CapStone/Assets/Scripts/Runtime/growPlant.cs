using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class growPlant : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator animator;

    private bool delay = false; // 최초 성장 시 스프라이트 이동을 위한 지연

    public float totaltime = 0.0f;
    public int level = 0; // 성장 단계    
    public bool droop = false; // 시듦 상태
    public bool growReady = false; // 성장 가능 상태


    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Grow();
        Droop();
    }

    void Grow()
    {

        if (delay == false && totaltime >= Random.Range(10, 30)) // 씨앗은 시들지 않음, 최초 성장 시 스프라이트 위치 이동
        {
            animator.SetBool("delay", true);
            Vector3 tmp = this.transform.position;
            this.transform.position = new Vector3(tmp.x, tmp.y - 0.2f, tmp.z);
            delay = true;
            totaltime = 0; // 성장 시 시간 초기화
        }

        if(growReady && !droop && totaltime >= Random.Range(5, 10)) // 성장 가능 상태에서 일정 시간이 지나면 성장
        {
            animator.SetBool("growReady", true);
            growReady = false;
        }

    }
    void Droop()
    {
        totaltime += Time.deltaTime;
        if(level < 4 && totaltime >= Random.Range(10, 30)) // 일정 시간마다 식물이 시들음 (다 자라면 시들지 않음)
        {
            totaltime = 0; // 시간 초기화
            droop = true;
            animator.SetBool("droop", true);
        }
    }

}
