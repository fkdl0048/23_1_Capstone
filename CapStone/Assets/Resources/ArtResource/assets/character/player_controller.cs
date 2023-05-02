using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class player_controller : MonoBehaviour
{   
    [SerializeField]
    private float speed = 5f; // �̵� �ӵ�
    private float xMove, yMove;
    private bool isThereTree = false; // ��ó�� ������ �� �ִ� ������ �ִ���
    private bool isThereWater = false; // ��ó�� ���ð����� ���� �ִ���
    private bool isThereFarm = false; // ��ó�� ��簡���� ���� �ִ���

    private bool nowFishing = false; // ���� ���� ������
    private float totaltime = 0.0f; // ���� �ð� ����
    private int fishCount = 0; // ���� ����� ��
    private string[] fish = new string[] { "����1", "����2", "����3", "����4", "����5" }; // ����� ����
    private int random = 0;

    private GameObject tree;
    private GameObject grid;
    private GameObject water;
    private GameObject farm;
    private GameObject plant;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    
    public PhotonView PV;

    void Start()
    {     
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        this.animator = GetComponent<Animator>();
        this.tree = GameObject.Find("tree");
        this.grid = GameObject.Find("Grid");
        this.water = grid.transform.Find("water").gameObject;
        this.farm = grid.transform.Find("farm").gameObject;
        this.plant = GameObject.Find("plant");
        //plant.SetActive(false);
    }

    void Update()
    {
        Move();
        Logging();
        Fishing();
        Farming();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision == tree.GetComponent<BoxCollider2D>()) // �ش� ������Ʈ�� �����϶���
            isThereTree = true;

        if (collision == water.GetComponent<BoxCollider2D>()) // �ش� ������Ʈ�� ���϶���
            isThereWater = true;

        if (collision == farm.GetComponent<BoxCollider2D>()) // �ش� ������Ʈ�� �����϶���
            isThereFarm = true;
    }

    void OnTriggerExit2D(Collider2D collision) // �������� �־����� ���� �Ұ�
    {
        if (collision == tree.GetComponent<BoxCollider2D>()) // �ش� ������Ʈ�� �����϶���
            isThereTree = false;

        if (collision == water.GetComponent<BoxCollider2D>()) // �ش� ������Ʈ�� ���϶���
            isThereWater = false;

        if (collision == farm.GetComponent<BoxCollider2D>()) // �ش� ������Ʈ�� �����϶���
            isThereFarm = false;
    }

    private void Move() // ĳ���� �̵�
    {
        if (nowFishing) // ���� �������̸� �̵� �Ұ�
            return;
        //if (PV.IsMine)
        // {
        xMove = 0;
        yMove = 0;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            xMove = speed * Time.deltaTime;
            sprite.flipX = false;
            animator.SetBool("isMoving", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            xMove = -speed * Time.deltaTime;
            sprite.flipX = true;
            animator.SetBool("isMoving", true);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            yMove = speed * Time.deltaTime;
            animator.SetBool("isMoving", true);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            yMove = -speed * Time.deltaTime;
            animator.SetBool("isMoving", true);
        }

        if (!(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))) // Ű�� ������ ���� ���¸� �̵� �ִϸ��̼� ����
            animator.SetBool("isMoving", false);

        this.transform.Translate(new Vector3(xMove, yMove, 0));
        // }
    }

    private void Logging() // ����
    {
        if (isThereTree) {
            if(tree.GetComponent<logging>().HP <= 0) // ������ �Ϸ�Ǹ�
            {
                animator.SetTrigger("idle");
                isThereTree = false; // �ൿ ����
                return;
            }
            if (Input.GetKeyDown(KeyCode.Z)) // ������ �Ҷ����� 1�� ����
            {
                animator.SetTrigger("isLogging");
                tree.GetComponent<logging>().HP--;
                print(tree.GetComponent<logging>().HP + " �� ���ҽ��ϴ�.");            
            }
        }
    }

    private void Fishing() // ����
    {
        if (isThereWater)
        {
            if (!nowFishing && Input.GetKeyDown(KeyCode.A)) // AŰ�� ������ ���� ����
            {
                print("���� ����");
                nowFishing = true;
                animator.SetBool("isFishing", true);

            }

            if (nowFishing && Input.GetKeyDown(KeyCode.S)) // SŰ�� ������ ���� ����
            {
                nowFishing = false;
                animator.SetBool("isFishing", false);
                print("���� �� " + fishCount + "���� ���ҽ��ϴ�.");
            }

            if (nowFishing)
            {
                if (totaltime >= Random.Range(10, 30)) // �� 10��~30�ʸ��� ����� ����
                {
                    totaltime = 0; // �ð� �ʱ�ȭ
                    random = Random.Range(0, 5);
                    animator.SetTrigger("getFish");
                    print(fish[random] + " ����⸦ ���ҽ��ϴ�!");
                    fishCount++;
                }

                totaltime += Time.deltaTime;
            }
        }
        
    }

    private void Farming()
    {
        if (isThereFarm)
        {
            if(Input.GetKeyDown(KeyCode.S)) // SŰ ������ ���� �ɱ�
                plant.SetActive(true);
        }
    }
}


