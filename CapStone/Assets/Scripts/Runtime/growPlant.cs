using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class growPlant : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator animator;

    private bool delay = false; // ���� ���� �� ��������Ʈ �̵��� ���� ����

    public float totaltime = 0.0f;
    public int level = 0; // ���� �ܰ�    
    public bool droop = false; // �õ� ����
    public bool growReady = false; // ���� ���� ����


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

        if (delay == false && totaltime >= Random.Range(10, 30)) // ������ �õ��� ����, ���� ���� �� ��������Ʈ ��ġ �̵�
        {
            animator.SetBool("delay", true);
            Vector3 tmp = this.transform.position;
            this.transform.position = new Vector3(tmp.x, tmp.y - 0.2f, tmp.z);
            delay = true;
            totaltime = 0; // ���� �� �ð� �ʱ�ȭ
        }

        if(growReady && !droop && totaltime >= Random.Range(5, 10)) // ���� ���� ���¿��� ���� �ð��� ������ ����
        {
            animator.SetBool("growReady", true);
            growReady = false;
        }

    }
    void Droop()
    {
        totaltime += Time.deltaTime;
        if(level < 4 && totaltime >= Random.Range(10, 30)) // ���� �ð����� �Ĺ��� �õ��� (�� �ڶ�� �õ��� ����)
        {
            totaltime = 0; // �ð� �ʱ�ȭ
            droop = true;
            animator.SetBool("droop", true);
        }
    }

}
