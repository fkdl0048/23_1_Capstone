using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class player_controller : MonoBehaviourPunCallbacks
{   
    [SerializeField]
    private float speed = 5f; // �̵� �ӵ�
    [SerializeField]
    private GameObject[] tree;
    [SerializeField]
    private GameObject[] water;
    [SerializeField]
    private GameObject[] farm;
    [SerializeField]
    private GameObject plantGenerator;

    private float xMove, yMove;
    private bool isThereTree = false; // ��ó�� ������ �� �ִ� ������ �ִ���
    private bool isThereWater = false; // ��ó�� ���ð����� ���� �ִ���
    private bool isThereFarm = false; // ��ó�� ��簡���� ���� �ִ���
    private bool isTherePlant = false; // ��ó�� �Ĺ��� �ִ���

    private bool nowFishing = false; // ���� ���� ������
    private float totaltime = 0.0f; // ���� �ð� ����
    private int fishCount = 0; // ���� ������ ��
    private string[] fish = new string[] { "����1", "����2", "����3", "����4", "����5" }; // ������ ����
    private int random = 0;
    private int ptCount = 0; // �Ĺ� ����
    private int nowPlant = -1; // ���� �����ϴ� �Ĺ�
    private int i;

    private int nowTree; // ���� ��ȣ�ۿ��ϴ� ����

    private GameObject[] plants = new GameObject[3];
    
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;

    //�̹μ� �߰�
    public PhotonView m_PV;
    public int m_playerPosIndex = -1;
    private GameObject m_oZone;
    private GameObject m_xZone;

    public int m_quizState = 0;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        sprite = this.GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();
        //this.tree = GameObject.Find("tree");
        //this.grid = GameObject.Find("Grid");
        //this.water = grid.transform.Find("water").gameObject;
        //this.farm = grid.transform.Find("farm").gameObject;
        //this.plantGenerator = GameObject.Find("plantGenerator");

        m_PV = this.GetComponent<PhotonView>(); 

        //ptCount = 0;
        
    }

    void Update()
    {
        if (m_PV.IsMine)
        {
            Move();
            Logging();
            Fishing();
            Farming();
        }
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        for(i = 0; i < tree.Length; i++) {
            if (collision == tree[i].GetComponent<BoxCollider2D>())
            { // �ش� ������Ʈ�� �����϶���
                isThereTree = true;
                tree[i].transform.Find("Canvas-tree").gameObject.SetActive(true);
                nowTree = i;
                break;
            }
        }

        for (i = 0; i < water.Length; i++)
        {
            if (collision == water[i].GetComponent<BoxCollider2D>()) // �ش� ������Ʈ�� ���϶���
                isThereWater = true;
        }

        for (i = 0; i < farm.Length; i++)
        {
            if (collision == farm[i].GetComponent<BoxCollider2D>()) // �ش� ������Ʈ�� �����϶���
                isThereFarm = true;
        //if (collision == tree.GetComponent<BoxCollider2D>()) // �ش� ������Ʈ�� �����϶���
        //    isThereTree = true;

        //if (collision == water.GetComponent<BoxCollider2D>()) // �ش� ������Ʈ�� ���϶���
        //    isThereWater = true;

        //if (collision == farm.GetComponent<BoxCollider2D>()) // �ش� ������Ʈ�� �����϶���
        //    isThereFarm = true;

        if (collision.gameObject.name == "OZone")
        {
            m_quizState = 1;
        }
        else if (collision.gameObject.name == "XZone")
        {
            m_quizState = 2;

        }

        for (i = 0; i < ptCount; i++) // �ش� ������Ʈ�� �Ĺ��϶���
        {
            if (plants[i] != null && collision == plants[i].GetComponent<BoxCollider2D>())
            {
                isTherePlant = true;
                nowPlant = i;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision) // ������Ʈ���� �־����� ��ȣ�ۿ� �Ұ�
    {
        for (i = 0; i < tree.Length; i++)
        {
            if (collision == tree[i].GetComponent<BoxCollider2D>())
            { // �ش� ������Ʈ�� �����϶���
                isThereTree = false;
                tree[i].transform.Find("Canvas-tree").gameObject.SetActive(false);
            }
        }

        for (i = 0; i < water.Length; i++)
        {
            if (collision == water[i].GetComponent<BoxCollider2D>()) // �ش� ������Ʈ�� ���϶���
                isThereWater = false;
        }

        for (i = 0; i < farm.Length; i++)
        {
            if (collision == farm[i].GetComponent<BoxCollider2D>()) // �ش� ������Ʈ�� �����϶���
                isThereFarm = false;
        //if (collision == tree.GetComponent<BoxCollider2D>()) // �ش� ������Ʈ�� �����϶���
        //    isThereTree = false;

        //if (collision == water.GetComponent<BoxCollider2D>()) // �ش� ������Ʈ�� ���϶���
        //    isThereWater = false;

        //if (collision == farm.GetComponent<BoxCollider2D>()) // �ش� ������Ʈ�� �����϶���
        //    isThereFarm = false;

        if (collision.gameObject.name == "OZone" || collision.gameObject.name == "XZone")
        {
            m_quizState = 0;
        }

        for (int i = 0; i < ptCount; i++) // �ش� ������Ʈ�� �Ĺ��϶���
        {
            if (plants[i] != null && collision == plants[i].GetComponent<BoxCollider2D>())
                isTherePlant = false;
        }
    }

    private void Move() // ĳ���� �̵�
    {
        if (nowFishing) // ���� �������̸� �̵� �Ұ�
            return;

        xMove = 0;
        yMove = 0;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            xMove = speed * Time.deltaTime;
            m_PV.RPC("FlipXRPC",RpcTarget.AllBuffered,false);
            animator.SetBool("isMoving", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            xMove = -speed * Time.deltaTime;
            m_PV.RPC("FlipXRPC", RpcTarget.AllBuffered, true);
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
      
    }

    private void Logging() // ����
    {
        if (isThereTree) {
            if(tree[nowTree].GetComponent<logging>().HP <= 0) // ������ �Ϸ�Ǹ�
            {
                isThereTree = false; // �ൿ ����
                tree[nowTree].transform.Find("Canvas-tree").gameObject.SetActive(false); // ü�¹� �Ⱥ��̰�
                return;
            }
            if (Input.GetKeyDown(KeyCode.Z)) // ������ �Ҷ����� 1�� ����
            {
                animator.SetTrigger("isLogging");
                tree[nowTree].GetComponent<logging>().HP--;
                tree[nowTree].GetComponent<treeHP>().DecreaseHP();
                print(tree[nowTree].GetComponent<logging>().HP + " �� ���ҽ��ϴ�.");            
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
                if (totaltime >= Random.Range(10, 30)) // �� 10��~30�ʸ��� ������ ����
                {
                    totaltime = 0; // �ð� �ʱ�ȭ
                    random = Random.Range(0, 5);
                    animator.SetTrigger("getFish");
                    print(fish[random] + " �����⸦ ���ҽ��ϴ�!");
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
            if (Input.GetKeyDown(KeyCode.S))
            { // SŰ ������ ���� �ɱ�
                if (ptCount > 2)
                    return;
                plantGenerator.GetComponent<plantGenerator>().Planting();
                plants[ptCount] = GameObject.Find("plant(Clone)"+ptCount);
                ptCount++;
            }

            if (Input.GetKeyDown(KeyCode.D)) // DŰ ������ �� �ֱ�
            { 
                animator.SetTrigger("isWatering");
                if (isTherePlant)  // �翡 �Ĺ��� �ִٸ� ��ȣ�ۿ�
                    Watering();
            }

            if (Input.GetKeyDown(KeyCode.X)) // XŰ�� ��Ȯ�ϱ�
            {
                animator.SetTrigger("handUp");
                if (isTherePlant)  // �翡 �Ĺ��� �ִٸ� ��ȣ�ۿ�
                    Harvest();
            }
        }
    }

    private void Watering()
    {
        print("�Ĺ��� ���� �ݴϴ�.");
            if (plants[nowPlant].GetComponent<growPlant>().droop == true) // �ѹ� �õ� �Ŀ� ������
            {
                plants[nowPlant].GetComponent<growPlant>().droop = false;
                plants[nowPlant].GetComponent<Animator>().SetBool("droop", false);
                plants[nowPlant].GetComponent<growPlant>().growReady = true;
                if (plants[nowPlant].GetComponent<Animator>().GetBool("growReady"))
                { // ����
                    plants[nowPlant].GetComponent<Animator>().SetInteger("level", ++plants[nowPlant].GetComponent<growPlant>().level);
                    plants[nowPlant].GetComponent<Animator>().SetBool("growReady", false);
                }
                plants[nowPlant].GetComponent<growPlant>().totaltime = 0;
            }
        
    }

    private void Harvest()
    {
            if (plants[nowPlant].GetComponent<growPlant>().level >= 4 && plants[nowPlant].GetComponent<growPlant>().droop == false) // ���� �Ϸ��� �Ĺ��� ��Ȯ ����
            {
                animator.SetTrigger("handUp");
                plants[nowPlant].GetComponent<Animator>().SetBool("harvest", true);
            }
    }

    [PunRPC]
    void FlipXRPC(bool flip)
    {
        sprite.flipX = flip;
    }
}



