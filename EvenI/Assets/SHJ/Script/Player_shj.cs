using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Player_shj : MonoBehaviour
{
    #region Variable
    Rigidbody2D rigid;

    public LineRenderer predictLine;

    [Range(0.0f, 10.0f)]
    public float camera_distance;

    [Range(0.0f, 1.0f)]
    public float timeSlowSpeed;
    public bool timeSlowOnOff;

    [Range(0.0f, 15.0f)]
    public float speed; //속도

    [Range(0.0f, 15.0f)]
    public float jump_up_power; //위로 점프력

    //[Range(0.0f, 15.0f)]
    //public float jump_right_power; //오른쪽으로 점프력

    [Range(0.0f, 2.0f)]
    public float maxJumpPower; //점프 게이지 차오르는 속도

    [Range(0.0f, 2.0f)]
    public float minJumpPower; //점프 게이지 차오르는 속도

    [Range(0.0f, 10.0f)]
    public float charge_speed; //점프 게이지 차오르는 속도

    [Range(0.0f, 10.0f)]
    public float shortJump_up_power;

    [SerializeField]
    bool jumping = false; //점프중인지 아닌지 확인
    bool floorCheck = true; //점프 하고 잠깐동안 바닥 체크 안하게
    float jump_charge = 0.0f; //점프력 충전
    public Image charge_img; //점프 게이지
    //int jump_cnt = 0; //점프횟수 2단점프때 사용

    public Animator playerAnimator;
    public GameObject hp_List;
    public int maxHP = 9;
    public int hp = 9;

    [Range(0.0f, 10.0f)]
    public float cameraSpeed;//카메라 스피드
    //public Map_shj map; //점프 속도 조절을 위해


    #endregion

    private void Start()
    {
        hp = maxHP;
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(jumping)
        {
            rigid.AddForce(Vector2.down);
        }
        if (floorCheck)
        {
            RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, Vector2.down, transform.localScale.y, LayerMask.GetMask("ground")); //바닥 검사 해서 떨어질 수 있게
            Debug.DrawRay(gameObject.transform.position, Vector2.down * hit.distance, Color.red);
            if (hit.collider != null)
            {
                jumping = false;
                if (!jumping)
                {
                    rigid.velocity = Vector2.right * speed; //점프중이지 않을 때는 속도 일정하게
                    if (!Input.GetMouseButton(0))
                    {
                        predictLine.positionCount = 0; // 달릴 때는 예측 선 안그려지게
                    }
                }
            }

        }
        if (hp > 0)
        {
            //for (int i = 1; i <= maxHP; i++)
            //{
            //    if (i <= hp)
            //    {
            //        hp_List.transform.GetChild(hp).gameObject.SetActive(true);
            //    }
            //    else
            //    {
            //        hp_List.transform.GetChild(hp).gameObject.SetActive(false);
            //    }
            //}
        }

        Camera.main.transform.position = new Vector3((transform.position + new Vector3(camera_distance, 0, 0)).x, 2, -10);//플레이어한테 맞춰서 카메라 배치
#if UNITY_EDITOR
        if (!jumping && Input.GetMouseButton(0))
        {
            jump_charge = jump_charge <= maxJumpPower ? jump_charge + Time.deltaTime * charge_speed : maxJumpPower; //차징하면 게이지가 차오릅니다
            charge_img.enabled = true; //ui활성화
            charge_img.fillAmount += Time.deltaTime/*jump_charge*/; //수정되었음
            if (timeSlowOnOff)
            {
                Time.timeScale = timeSlowSpeed;
            }
            if (jump_charge < minJumpPower)
            {
                jump_charge = minJumpPower;
            }

        }
        else if (!jumping && Input.GetMouseButtonUp(0))
        {
            Jump();
            Time.timeScale = 1f;
        }


#elif UNITY_ANDROID
        if (Input.touchCount > 0 && !jumping)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                charge_img.enabled = true; //ui활성화
                jump_charge = jump_charge <= 1.0f ? jump_charge + Time.deltaTime * charge_speed : 1.0f; //차징하면 게이지가 차오릅니다
                charge_img.fillAmount = jump_charge;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                Jump();
        }
#endif

    }
    public void Jump()
    {
        floorCheck = false;
        StartCoroutine(FloorCheck());
        playerAnimator.SetTrigger("Jump"); //점프 애니메이션
        jumping = true; //점프중
        charge_img.enabled = false; //ui비활성화
        rigid.AddForce((Vector2.up * jump_up_power) * 50 * jump_charge,ForceMode2D.Force);
        //rigid.velocity = (Vector2.right * jump_right_power + Vector2.up * jump_up_power) * jump_charge;
        //rigid.AddForce(Vector2.up * jump_up_power * jump_charge, ForceMode2D.Impulse); //차징한 만큼 점프
        jump_charge = 0.0f;
        charge_img.fillAmount = 0.0f; //추가됨
        //if(jump_cnt != 2) //2단 점프 구현
        //{
        //    jump_cnt++;
        //    rigid.velocity = Vector2.zero;
        //    rigid.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse);
        //}
    }

    IEnumerator FloorCheck()
    {
        yield return new WaitForSeconds(0.1f);
        floorCheck = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {

        }//jumping = false;
        else if (collision.gameObject.layer == 9)
        {
            Rock_HJH rock;
            if (collision.gameObject.TryGetComponent<Rock_HJH>(out rock))
            {
                rock.RockTouch();
            }
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            //collision.gameObject.SetActive(false);
            hp--;
        }
    }

    void PredictLine(Vector2 startPos, Vector2 vel)  //포물선 예측
    {
        int step = 120;
        float deltaTime = Time.fixedDeltaTime;
        Vector2 gravity = (Vector2)Physics.gravity+Vector2.down;
        Vector2 position = startPos;
        Vector2 velocity = vel;
        predictLine.positionCount = 120;
        for (int i = 0; i < step; i++)
        {
            position += velocity * deltaTime + gravity * deltaTime * deltaTime;
            velocity += gravity * deltaTime;
            predictLine.SetPosition(i, position);
            Collider2D colls = Physics2D.OverlapCircle(position, 0.5f);
            if (colls != null && colls.transform.name != "Player")
            {
                predictLine.positionCount = i; //포물선이 다른 물체와 충돌시 더이상 그리지 않게
                break;
            }

        }
    }


    public void NukBack()
    {

    }
}
