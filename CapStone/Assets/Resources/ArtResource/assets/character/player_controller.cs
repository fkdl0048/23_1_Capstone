using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class player_controller : MonoBehaviour
{   
    [SerializeField]
    private float speed = 5f; // 이동 속도
    private float xMove, yMove;
    private bool isThereTree = false; // 근처에 벌목할 수 있는 나무가 있는지
    private bool isThereWater = false; // 근처에 낚시가능한 물이 있는지
    private bool isThereFarm = false; // 근처에 농사가능한 땅이 있는지

    private bool nowFishing = false; // 현재 낚시 중인지
    private float totaltime = 0.0f; // 낚시 시간 측정
    private int fishCount = 0; // 잡은 물고기 수
    private string[] fish = new string[] { "생선1", "생선2", "생선3", "생선4", "생선5" }; // 물고기 종류
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
        if (collision == tree.GetComponent<BoxCollider2D>()) // 해당 오브젝트가 나무일때만
            isThereTree = true;

        if (collision == water.GetComponent<BoxCollider2D>()) // 해당 오브젝트가 물일때만
            isThereWater = true;

        if (collision == farm.GetComponent<BoxCollider2D>()) // 해당 오브젝트가 농장일때만
            isThereFarm = true;
    }

    void OnTriggerExit2D(Collider2D collision) // 나무에서 멀어지면 벌목 불가
    {
        if (collision == tree.GetComponent<BoxCollider2D>()) // 해당 오브젝트가 나무일때만
            isThereTree = false;

        if (collision == water.GetComponent<BoxCollider2D>()) // 해당 오브젝트가 물일때만
            isThereWater = false;

        if (collision == farm.GetComponent<BoxCollider2D>()) // 해당 오브젝트가 농장일때만
            isThereFarm = false;
    }

    private void Move() // 캐릭터 이동
    {
        if (nowFishing) // 현재 낚시중이면 이동 불가
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

        if (!(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))) // 키를 누르지 않은 상태면 이동 애니메이션 중지
            animator.SetBool("isMoving", false);

        this.transform.Translate(new Vector3(xMove, yMove, 0));
        // }
    }

    private void Logging() // 벌목
    {
        if (isThereTree) {
            if(tree.GetComponent<logging>().HP <= 0) // 벌목이 완료되면
            {
                animator.SetTrigger("idle");
                isThereTree = false; // 행동 중지
                return;
            }
            if (Input.GetKeyDown(KeyCode.Z)) // 도끼질 할때마다 1씩 감소
            {
                animator.SetTrigger("isLogging");
                tree.GetComponent<logging>().HP--;
                print(tree.GetComponent<logging>().HP + " 번 남았습니다.");            
            }
        }
    }

    private void Fishing() // 낚시
    {
        if (isThereWater)
        {
            if (!nowFishing && Input.GetKeyDown(KeyCode.A)) // A키를 누르면 낚시 시작
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
            if(Input.GetKeyDown(KeyCode.S)) // S키 눌러서 씨앗 심기
                plant.SetActive(true);
        }
    }
}


