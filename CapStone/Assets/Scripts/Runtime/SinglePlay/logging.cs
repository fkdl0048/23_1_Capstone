using UnityEngine;


public class logging : MonoBehaviour
{

    public int HP = 3;
    private BoxCollider2D boxCollider;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP == 0)
        {
            animator.SetBool("isCut", true);
            //Destroy(this);
        }
    }

}
