using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class IceMonster_HJH : Object_Manager_shj
{
    Rigidbody2D rigid;
    float currentTime = 0;
    GameObject player;
    public float startDistance;
    public float runningTime;
    public float runSpeed;
    public float jumpPower;
    public float angle;
    bool startCo = false;
    bool Out = false;
    Vector2 move;
    bool jump = false;
    public float gravity;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (startCo)
        {
            if (jump)
            {
                rigid.AddForce(Vector2.down * gravity);
            }
            else
            {
                rigid.velocity = Vector2.left * runSpeed;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        else
        {
            if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) < startDistance)
            {
                if (!startCo)
                {
                    animator.SetTrigger("Touch");
                    StartCoroutine(MonsterMove());
                    startCo = true;
                }
            }
        }
        move = rigid.velocity;
    }

    IEnumerator MonsterMove()
    {
        yield return new WaitForSeconds(runningTime);
        Vector2 moveVec = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
        jump = true;
        rigid.AddForce(moveVec * jumpPower, ForceMode2D.Impulse);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !Out)
        {
            GameObject.Find("SoundManager").GetComponent<SoundManager_HJH>().ObjectBreakSoundPlay();
            Active(collision.gameObject);
            rigid.velocity = move;
            gameObject.layer = 3;
            Out = true;
        }
        if(collision.gameObject.layer == 8)
        {
            jump = false;
        }
}
    }
