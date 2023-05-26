using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;

public class player_controller : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private float speed = 5f; // 이동 속도
    [SerializeField]
    private GameObject tree;
    [SerializeField]
    private GameObject water;
    [SerializeField]
    private GameObject farm;
   
    private float xMove, yMove;
    private bool isThereTree = false; // 근처에 벌목할 수 있는 나무가 있는지
    private bool isThereWater = false; // 근처에 낚시가능한 물이 있는지
    private bool isThereFarm = false; // 근처에 농사가능한 땅이 있는지
    private bool isTherePlant = false; // 근처에 식물이 있는지

    private bool nowFishing = false; // 현재 낚시 중인지
    private float totaltime = 0.0f; // 낚시 시간 측정
    private int fishCount = 0; // 잡은 물고기 수
    private string[] fish = new string[] { "생선1", "생선2", "생선3", "생선4", "생선5" }; // 물고기 종류
    private int random = 0;
    private int nowPlant = -1; // 현재 관리하는 식물
    private GameObject[] plants;



    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;

    //이민석 추가
    public PhotonView m_PV;
    public int m_playerPosIndex = -1;
    [SerializeField] private GameObject m_oZone;
    [SerializeField] private GameObject m_xZone;
    [SerializeField] private GameObject m_oxSign;
    public int m_quizState = 0;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        sprite = this.GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();
        m_PV = this.GetComponent<PhotonView>();
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
        if (collision.GetComponent<Collider2D>().gameObject.CompareTag("tree")) // 해당 오브젝트가 나무일때만
        {
            tree = collision.GetComponent<Collider2D>().gameObject;
            tree.transform.Find("Canvas-tree").gameObject.SetActive(true);
            isThereTree = true;
        }
   
        if (collision.GetComponent<Collider2D>().gameObject.CompareTag("water")) // 해당 오브젝트가 물일때만
                isThereWater = true;

        if (collision.GetComponent<Collider2D>().gameObject.CompareTag("farm")) // 해당 오브젝트가 농장일때만
        {
            farm = collision.GetComponent<Collider2D>().gameObject;
            this.GetComponent<plantGenerator>().farm = this.farm;
            this.GetComponent<plantGenerator>().farmSet = true;
            isThereFarm = true;
        }


        if (collision.gameObject.name == m_oZone.name)
        {
            m_quizState = 1;
        }
        else if (collision.gameObject.name == m_xZone.name)
        {
            m_quizState = 2;

        }

        if (isThereFarm)
        {
            for (int i = 0; i < farm.GetComponent<farming>().plants.Length; i++) // 해당 오브젝트가 식물일때만
            {
                plants = farm.GetComponent<farming>().plants;
                if (plants[i] != null && collision == plants[i].GetComponent<BoxCollider2D>())
                {
                    isTherePlant = true;
                    nowPlant = i;
                }
            }
        }

        if (collision.gameObject.name == m_oxSign.name)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                collision.GetComponent<OXQuiz>().StartQuiz();
                m_quizState = 3;
            }
        }

    }

    void OnTriggerExit2D(Collider2D collision) // 오브젝트에서 멀어지면 상호작용 불가
    {

        if (collision.GetComponent<Collider2D>().gameObject.CompareTag("tree")) // 해당 오브젝트가 나무일때만
        {
            tree.transform.Find("Canvas-tree").gameObject.SetActive(false);
            isThereTree = false;
        }

        if (collision.GetComponent<Collider2D>().gameObject.CompareTag("water")) // 해당 오브젝트가 물일때만
            isThereWater = false;


        if (collision.GetComponent<Collider2D>().gameObject.CompareTag("farm")) // 해당 오브젝트가 농장일때만
        {
            isThereFarm = false;
        }

        if (isThereFarm)
        {
            for (int i = 0; i < farm.GetComponent<farming>().plants.Length; i++) // 해당 오브젝트가 식물일때만
            {
                plants = farm.GetComponent<farming>().plants;
                if (plants[i] != null && collision == plants[i].GetComponent<BoxCollider2D>())
                    isTherePlant = false;
            }
        }

        if (collision.gameObject.name == m_oZone.name  || collision.gameObject.name == m_xZone.name)
        {
            m_quizState = 0;
        }

    }

    private void Move() // 캐릭터 이동
    {
        if (nowFishing) // 현재 낚시중이면 이동 불가
            return;

        xMove = 0;
        yMove = 0;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            xMove = speed * Time.deltaTime;
            m_PV.RPC("FlipXRPC", RpcTarget.AllBuffered, false);
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

        if (!(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))) // 키를 누르지 않은 상태면 이동 애니메이션 중지
            animator.SetBool("isMoving", false);

        this.transform.Translate(new Vector3(xMove, yMove, 0));

    }

    private void Logging() // 벌목
    {
        if (isThereTree)
        {
            if (tree.GetComponent<logging>().HP <= 0) // 벌목이 완료되면
            {
                isThereTree = false; // 행동 중지
                tree.transform.Find("Canvas-tree").gameObject.SetActive(false); // 체력바 안보이게
                return;
            }
            if (Input.GetKeyDown(KeyCode.Z)) // 도끼질 할때마다 1씩 감소
            {
                if(tree.transform.position.x < this.transform.position.x) // 나무가 플레이어보다 왼쪽에 있으면
                {
                    m_PV.RPC("FlipXRPC", RpcTarget.AllBuffered, true);
                }
                else if(tree.transform.position.x > this.transform.position.x)
                {
                    m_PV.RPC("FlipXRPC", RpcTarget.AllBuffered, false);
                }
                animator.SetTrigger("isLogging");
                m_PV.RPC("treeRPC",RpcTarget.AllBuffered);
                print(tree.GetComponent<logging>().HP + " 번 남았습니다.");
            }
        }
    }

    private void Fishing() // 낚시
    {
        if (isThereWater)
        {
            if (!nowFishing && Input.GetKeyDown(KeyCode.Z)) // A키를 누르면 낚시 시작
            {
                print("낚시 시작");
                nowFishing = true;
                animator.SetBool("isFishing", true);

            }

            if (nowFishing && Input.GetKeyDown(KeyCode.S)) // S키를 누르면 낚시 종료
            {
                nowFishing = false;
                animator.SetBool("isFishing", false);
                print("낚시 끝 " + fishCount + "마리 낚았습니다.");
            }

            if (nowFishing)
            {
                if (totaltime >= Random.Range(10, 30)) // 약 10초~30초마다 물고기 낚음
                {
                    totaltime = 0; // 시간 초기화
                    random = Random.Range(0, 5);
                    animator.SetTrigger("getFish");
                    print(fish[random] + " 물고기를 낚았습니다!");
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
            plants = farm.GetComponent<farming>().plants;
            if (Input.GetKeyDown(KeyCode.Z))
            { // S키 눌러서 씨앗 심기
                if (farm.GetComponent<farming>().cnt > 2)
                    return;
                print("씨앗을 심었습니다.");
                m_PV.RPC("plantingRPC", RpcTarget.AllBuffered);
            }

            if (Input.GetKeyDown(KeyCode.A)) // D키 눌러서 물 주기
            {
                animator.SetTrigger("isWatering");
                if (isTherePlant)  // 밭에 식물이 있다면 상호작용
                    Watering();
            }

            if (Input.GetKeyDown(KeyCode.S)) // X키로 수확하기
            {
                animator.SetTrigger("handUp");
                if (isTherePlant)  // 밭에 식물이 있다면 상호작용
                    Harvest();
            }
        }
    }

    private void Watering()
    {
        plants = farm.GetComponent<farming>().plants;
        m_PV.RPC("wateringRPC", RpcTarget.AllBuffered, nowPlant);
    }

    private void Harvest()
    {
        plants = farm.GetComponent<farming>().plants;
        m_PV.RPC("harvestRPC", RpcTarget.AllBuffered, nowPlant);
    }

    [PunRPC]
    void FlipXRPC(bool flip)
    {
        if(sprite != null)
            sprite.flipX = flip;
    }

    [PunRPC]
    void treeRPC()
    {
        tree.GetComponent<logging>().HP--;
        tree.GetComponent<treeHP>().DecreaseHP();
    }

    [PunRPC]
    void plantingRPC()
    {
        this.GetComponent<plantGenerator>().Planting();
    }

    [PunRPC]
    void wateringRPC(int plantIdx)
    {
        print("식물에 물을 줍니다.");
        farm.GetComponent<farming>().nowPlant = plantIdx;
        farm.GetComponent<farming>().watering();
    }

    [PunRPC]
    void harvestRPC(int plantIdx)
    { 
        farm.GetComponent<farming>().nowPlant = plantIdx;
        farm.GetComponent<farming>().harvest();
        if (plants[nowPlant].GetComponent<growPlant>().level >= 4 && plants[nowPlant].GetComponent<growPlant>().droop == false)
            farm.GetComponent<farming>().harvest_cnt++;
    }
}