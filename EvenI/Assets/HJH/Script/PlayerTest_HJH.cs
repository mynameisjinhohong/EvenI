using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTest_HJH : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField]
    bool jumping = false;
    bool floorCheck = true;
    float jump_charge = 0.0f;
    [Range(0.0f, 2.0f)]
    public float maxJumpPower;

    [Range(0.0f, 2.0f)]
    public float minJumpPower;

    bool invincible = false;
    public bool upCrushCheck = false; //위에 부딪혔을 때
    [Range(0.0f, 10.0f)]
    public float charge_speed;
    public Image charge_img;
    bool jumpBool = false;
    public bool isFloor = false;
    public float gravity = -9.81f;
    public float jump_up_power; //위로 점프력
    bool rollStart = false;
    bool nuckBackDuring = false;
    Player_State player_State;
    public Transform[] rayPoint;
    bool nuckBackBool = false;
    public Player_State state { get { return player_State; } set { player_State = value; } }
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    IEnumerator UpCrushOff()
    {
        yield return new WaitForSeconds(0.5f);
        upCrushCheck = false;
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(rayPoint[0].position, Vector2.up, transform.localScale.x / 2, LayerMask.GetMask("ground"));
        if (hit.collider != null)
        {
            hit.transform.gameObject.GetComponent<Floor_HJH>().Crash(gameObject);
            upCrushCheck = true;
            StartCoroutine(UpCrushOff());
        }
        hit = Physics2D.Raycast(rayPoint[1].position, Vector2.up, transform.localScale.x / 2, LayerMask.GetMask("ground"));
        if (hit.collider != null)
        {
            hit.transform.gameObject.GetComponent<Floor_HJH>().Crash(gameObject);
            upCrushCheck = true;
            StartCoroutine(UpCrushOff());
        }
        hit = Physics2D.Raycast(rayPoint[2].position, Vector2.right, transform.localScale.x / 2, LayerMask.GetMask("ground"));
        if (hit.collider != null)
        {
            hit.transform.gameObject.GetComponent<Floor_HJH>().Crash(gameObject);
        }
        hit = Physics2D.Raycast(rayPoint[3].position, Vector2.right, transform.localScale.x / 2, LayerMask.GetMask("ground"));
        if (hit.collider != null)
        {
            hit.transform.gameObject.GetComponent<Floor_HJH>().Crash(gameObject);
        }
        hit = Physics2D.Raycast(rayPoint[4].position, Vector2.right, transform.localScale.x / 2, LayerMask.GetMask("ground"));
        if (hit.collider != null)
        {
            hit.transform.gameObject.GetComponent<Floor_HJH>().Crash(gameObject);
        }
        Camera.main.transform.position = new Vector3((transform.position + new Vector3(9.5f, 0, 0)).x, 2, -10);
        if (!jumping && Input.GetMouseButton(0))
        {
            jump_charge = jump_charge <= maxJumpPower ? jump_charge + Time.deltaTime * charge_speed : maxJumpPower; 
            charge_img.enabled = true;
            charge_img.fillAmount = (jump_charge - minJumpPower) / ((jump_charge - minJumpPower) + (maxJumpPower - jump_charge));
            if (jump_charge < minJumpPower)
            {
                jump_charge = minJumpPower;
            }
            if (jump_charge > maxJumpPower)
            {
                jump_charge = maxJumpPower;
            }
        }
        else if (!jumping && Input.GetMouseButtonUp(0))
        {
            jumpBool = true;
        }
    }
    private void FixedUpdate()
    {

        RunRoll();
        Jump();
        NukBack();
    }
    IEnumerator Rolling() //구르기
    {
        //gameObject.GetComponent<BoxCollider2D>().isTrigger = true; //임시 주석
        //playerAnimator.SetTrigger("Roll"); //구르기 애니메이션 작동
        yield return new WaitForSeconds(1f); //일정시간동안 구르기진행
        player_State = Player_State.Run; //달리는 상태로 복귀
        //gameObject.GetComponent<BoxCollider2D>().isTrigger = false; //임시 주석
        //playerAnimator.SetTrigger("RollEnd");
        rollStart = false;
        //playerAnimator.SetBool("Rolling", false);
    }
    public void RunRoll()
    {
        if (player_State == Player_State.Rolling && !rollStart) //구를때
        {
            StartCoroutine(Rolling());
            rollStart = true;
        }

        if (!nuckBackDuring)
        {
            if (player_State == Player_State.Rolling)
            {
                transform.localScale = new Vector3(1, 1, 0.0f);
                rigid.velocity = Vector2.right * 10 + new Vector2(0, rigid.velocity.y);
            }
            else
            {
                transform.localScale = new Vector3(0.8f, 0.8f, 0.0f);
                rigid.velocity = Vector2.right * 8 + new Vector2(0, rigid.velocity.y);
            }
        }
        if (floorCheck)
        {
            if (!isFloor)
            {
                rigid.AddForce(Vector2.down * gravity);
            }
            if (!upCrushCheck)
            {
                RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, Vector2.down, transform.localScale.y, LayerMask.GetMask("ground"));
                if (hit.collider != null)
                {
                    isFloor = true;
                    jumping = false;
                }
            }
        }

    }
    public void Jump()
    {
        if (!jumpBool)
        {
            return;
        }
        jumpBool = false;
        floorCheck = false;
        isFloor = false;
        StartCoroutine(FloorCheck());
        jumping = true; //점프중
        rigid.AddForce((Vector2.up * jump_up_power) * jump_charge, ForceMode2D.Impulse);
        charge_img.enabled = false; //ui비활성화
        jump_charge = 0.0f;
        charge_img.fillAmount = 0.0f; //추가됨
    }
    IEnumerator FloorCheck()
    {
        yield return new WaitForSeconds(0.1f);
        floorCheck = true;
    }
    public void NukBack()
    {
        if (!nuckBackBool)
        {
            return;
        }
        nuckBackBool = false;
        invincible = true;
        rigid.velocity = Vector2.zero;
        rigid.AddForce((Vector2.left), ForceMode2D.Force);
        nuckBackDuring = true;
        StartCoroutine(NuckBackAddForce());
        StartCoroutine(Invincible());
        StartCoroutine(Blink());
    }
    IEnumerator Invincible() //무적상태 일정 시간 후 꺼주는 코루틴
    {
        yield return new WaitForSeconds(1f);
        invincible = false;
    }
    IEnumerator Blink() //주인공 깜박이게
    {
        int count = 0;
        gameObject.SetActive(true);

        while (count < 3)
        {
            yield return new WaitForSeconds(1f);
            count += 2;
        }
    }
    IEnumerator NuckBackAddForce()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            rigid.AddForce(Vector2.right);
            if (rigid.velocity.x > 8)
            {
                rigid.velocity = Vector2.right * 8;
                nuckBackDuring = false;
                break;
            }
        }
    }
}
