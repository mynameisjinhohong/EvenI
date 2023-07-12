using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum Player_State // 플레이어의 상태 달리는중인지, 구르는중인지
{
    Run,
    Rolling,
}

public class Player_shj : MonoBehaviour
{
    #region Variable
    Rigidbody2D rigid;
    AudioSource audio;
    SoundManager_HJH soundManager;
    Camera cam;

    public LineRenderer predictLine;
    public GameObject gameOverPanel;
    public GameObject gameClearPanel;
    [Range(0.0f, 10.0f)]
    public float camera_distance;

    [Range(0.0f, 1.0f)]
    public float timeSlowSpeed;
    public bool timeSlowOnOff;

    [Range(0.0f, 15.0f)]
    public float speed; //최대속도
    [Range(0.0f, 15.0f)]
    public float velocity; //가속도

    [Header("점프 관련")]
    [Range(0.0f, 100.0f)]
    public float jump_up_power; //위로 점프력

    //[Range(0.0f, 15.0f)]
    //public float jump_right_power; //오른쪽으로 점프력
    [Range(0.0f, 2.0f)]
    public float maxJumpPower; //점프 게이지 차오르는 속도

    [Range(0.0f, 2.0f)]
    public float minJumpPower; //점프 게이지 차오르는 속도

    [Range(0.0f, 10.0f)]
    public float charge_speed; //점프 게이지 차오르는 속도
    [Header("구르기 관련")]
    [Range(0.0f, 10.0f)]
    public float rolling_time; //구르는 시간
    [Range(0.0f, 15.0f)]
    public float rolling_Speed; //구르기 속도
    [Range(0.0f, 50.0f)]
    public float rolling_MoveSpeed;
    bool rollStart = false;


    [Range(0.0f, 5.0f)] //구를때 크기
    public float rolling_size;
    float default_size;

    [SerializeField]
    bool jumping = false; //점프중인지 아닌지 확인
    bool floorCheck = true; //점프 하고 잠깐동안 바닥 체크 안하게
    float jump_charge = 0.0f; //점프력 충전
    public Image charge_img; //점프 게이지
    //int jump_cnt = 0; //점프횟수 2단점프때 사용

    public Animator playerAnimator;
    [Header("체력 관련")]
    public GameObject hp_List;
    public int maxHP = 9;



    public int Hp = 9;

    public Transform[] rayPoint;
    public int hp
    {
        get
        {
            return Hp;
        }
        set
        {
            if (!invincible)
            {
                if (Hp > value)
                {
                    nuckBackBool = true;
                }
                Hp = value;
                if (Hp < 1)
                {
                    if (!gameOverPanel.activeInHierarchy)
                    {
                        gameOverPanel.SetActive(true);
                        Time.timeScale = 0f;
                    }
                    soundManager.LifeZeroSoundPlay();
                }
            }
        }
    }
    [Header("넉백과 무적시간 관련")]
    [Range(0.0f,10.0f)]
    public float invincibleTime = 1f;
    [Range(0.0f, 1000.0f)]
    public float nuckBackPower = 1f;
    public int blinkCount;
    [Range(0.0f, 1f)]
    public float blinkSpeed;
    bool invincible = false;

    [Header("카메라 속도")]
    [Range(0.0f, 10.0f)]
    public float cameraSpeed;//카메라 스피드
    //public Map_shj map; //점프 속도 조절을 위해


    public SpriteRenderer playerSprite;

    Player_State player_State;
    public Player_State state { get { return player_State; } set { player_State = value; } }

    [Header("임시로 만들어본 것들")]
    public bool isFloor = false;
    public float gravity = -9.81f;
    bool jumpBool = false;
    bool nuckBackBool = false;
    bool nuckBackDuring = false;
    public bool upCrushCheck = false; //위에 부딪혔을 때
    #endregion
    float test = 0.0f;
    public bool gameClear = false;
    private void Start()
    {
        cam = Camera.main;
        soundManager = GetComponentInChildren<SoundManager_HJH>();  
        audio = GetComponent<AudioSource>();
        playerAnimator.SetFloat("RollSpeed", rolling_Speed);
        hp = maxHP;
        rigid = GetComponent<Rigidbody2D>();
        player_State = Player_State.Run;
        default_size = transform.localScale.x; 
    }

    private void Update()
    {
        //이전 코드 주석으로 놔둠
        #region test
        //if (player_State == Player_State.Rolling)
        //{
        //    if (transform.position.y < 0)
        //    {
        //        rigid.velocity = new Vector2(rigid.velocity.x, 0);
        //    }
        //}
        //if (jumping)
        //{
        //    rigid.AddForce(Vector2.down);
        //}
        //if (floorCheck)
        //{
        //    test = 0.0f; ;
        //    RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, Vector2.down, transform.localScale.y, LayerMask.GetMask("ground")); //바닥 검사 해서 떨어질 수 있게
        //    Debug.DrawRay(gameObject.transform.position, Vector2.down * hit.distance, Color.red);
        //    if (hit.collider != null)
        //    {
        //        isFloor = true;
        //        jumping = false;
        //        if (!jumping && !invincible)
        //        {
        //            if(player_State == Player_State.Rolling)
        //            {
        //                moveVec += (Vector3)Vector2.right * rolling_MoveSpeed;
        //                //rigid.velocity = Vector2.right * speed + Vector2.right * rolling_MoveSpeed;
        //                //transform.position += (Vector3)Vector2.right * rolling_MoveSpeed * Time.deltaTime;
        //            }
        //            else
        //            {
        //                moveVec += (Vector3)Vector2.right * speed;
        //                //rigid.velocity = Vector2.right * speed; //점프중이지 않을 때는 속도 일정하게
        //                //transform.position += (Vector3)Vector2.right * speed * Time.deltaTime;
        //            }
        //            if (!Input.GetMouseButton(0))
        //            {
        //                predictLine.positionCount = 0; // 달릴 때는 예측 선 안그려지게
        //            }
        //        }
        //    }

        //}
        if(transform.position.y < -6)
        {
            hp = 0;
        }
        RaycastHit2D hit = Physics2D.Raycast(rayPoint[0].position, Vector2.up,transform.localScale.x/2, LayerMask.GetMask("ground"));
        if(hit.collider !=  null)
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
        if (hp_List.transform.childCount > 0)
        {
            if (Hp != 0)
            {
                for (int i = 0; i < maxHP; i++)
                {
                    if (i < hp)
                    {
                        hp_List.transform.GetChild(i).gameObject.SetActive(true);
                    }
                    else
                    {
                        hp_List.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }
            else if (Hp == 0)
            {
                for (int i = 0; i < hp_List.transform.childCount; i++)
                {
                    hp_List.transform.GetChild(i).gameObject.SetActive(false);
                }
            }

        }
        if (hp < 1)
        {
            
        }
        #endregion
        if (!gameClear)
        {
            Camera.main.transform.position = new Vector3((transform.position + new Vector3(camera_distance, 0, 0)).x, 2, -10);//플레이어한테 맞춰서 카메라 배치

        }
        else
        {
            Vector3 viewPos = cam.WorldToViewportPoint(transform.position);
            if (viewPos.x > 1 &&viewPos.z > 0)
            {
                gameClearPanel.SetActive(true);
                gameOverPanel.SetActive(false);
            }
        }
#if UNITY_EDITOR
        if (!jumping && Input.GetMouseButton(0))
        {
            jump_charge = jump_charge <= maxJumpPower ? jump_charge + Time.deltaTime * charge_speed : maxJumpPower; //차징하면 게이지가 차오릅니다
            charge_img.enabled = true; //ui활성화
            //Debug.Log(jump_charge/(maxJumpPower -minJumpPower) - 1);
            charge_img.fillAmount = (jump_charge-minJumpPower)/((jump_charge-minJumpPower) + (maxJumpPower-jump_charge))/*Time.deltaTime*//*jump_charge*/; //수정되었음
            //if (timeSlowOnOff)
            //{
            //    Time.timeScale = timeSlowSpeed;
            //}
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
            //test = true;
            jumpBool = true;
            //Time.timeScale = 1f;
        }


        //if(jumping && test < 1.0f)
        //{
        //    float angle = 75 * Mathf.Deg2Rad;
        //    Vector3 direction = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
        //    transform.position += (direction - transform.position) * 10 * Time.deltaTime;
        //    test += Time.deltaTime;
        //}

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
    private void FixedUpdate()
    {
        RunRoll();        
        Jump();
        NukBack();

    }
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    IEnumerator UpCrushOff()
    {
        yield return new WaitForSeconds(0.5f);
        upCrushCheck = false;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        RaycastHit2D hit = Physics2D.Raycast(rayPoint[0].position, Vector2.up, transform.localScale.x/2, LayerMask.GetMask("ground"));
        if (hit.collider != null)
        {
            Gizmos.DrawRay(rayPoint[0].position, Vector2.up*hit.distance);
        }
        else
        {
            Gizmos.DrawRay(rayPoint[0].position, Vector2.up * transform.localScale.x / 2);
        }
        hit = Physics2D.Raycast(rayPoint[1].position, Vector2.up, transform.localScale.x / 2, LayerMask.GetMask("ground"));
        if (hit.collider != null)
        {
            Gizmos.DrawRay(rayPoint[1].position, Vector2.up * hit.distance);
        }
        else
        {
            Gizmos.DrawRay(rayPoint[1].position, Vector2.up * transform.localScale.x / 2);
        }
        hit = Physics2D.Raycast(rayPoint[2].position, Vector2.right, transform.localScale.x / 2, LayerMask.GetMask("ground"));
        if (hit.collider != null)
        {
            Gizmos.DrawRay(rayPoint[2].position, Vector2.right * hit.distance);
        }
        else
        {
            Gizmos.DrawRay(rayPoint[2].position, Vector2.right * transform.localScale.x / 2);
        }
        hit = Physics2D.Raycast(rayPoint[3].position, Vector2.right, transform.localScale.x / 2, LayerMask.GetMask("ground"));
        if (hit.collider != null)
        {
            Gizmos.DrawRay(rayPoint[3].position, Vector2.right * hit.distance);
        }
        else
        {
            Gizmos.DrawRay(rayPoint[3].position, Vector2.right * transform.localScale.x / 2);
        }
        hit = Physics2D.Raycast(rayPoint[4].position, Vector2.right, transform.localScale.x / 2, LayerMask.GetMask("ground"));
        if (hit.collider != null)
        {
            Gizmos.DrawRay(rayPoint[4].position, Vector2.right * hit.distance);
        }
        else
        {
            Gizmos.DrawRay(rayPoint[4].position, Vector2.right * transform.localScale.x / 2);
        }
    }
    //private void FixedUpdate()
    //{
    //    if(test)
    //    {
    //        Jump();
    //        test = false;
    //    }mathf
    //}
    public void RunRoll()
    {
        //rigid.AddForce(Vector2.right * velocity);
        if (player_State == Player_State.Rolling && !rollStart) //구를때
        {
            StartCoroutine(Rolling());
            rollStart = true;
        }

        if (!nuckBackDuring)
        {
            if (player_State == Player_State.Rolling)
            {
                transform.localScale = new Vector3(rolling_size, rolling_size, 0.0f);
                rigid.velocity = Vector2.right * rolling_MoveSpeed + new Vector2(0, rigid.velocity.y);
                //vecSpeed.x = rolling_MoveSpeed;
            }
            else
            {
                transform.localScale = new Vector3(default_size, default_size, 0.0f);
                rigid.velocity = Vector2.right * speed + new Vector2(0, rigid.velocity.y);
                //vecSpeed.x = speed;
            }
        }
        if (floorCheck)
        {
            if (!isFloor)
            {
                rigid.AddForce(Vector2.down * gravity);
                //velocity.y = gravity;
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

    public void Jump() //점프
    {
        if (!jumpBool)
        {
            return;
        }
        audio.Play();
        jumpBool = false;
        floorCheck = false;
        isFloor = false;
        StartCoroutine(FloorCheck());
        playerAnimator.SetTrigger("Jump"); //점프 애니메이션
        jumping = true; //점프중
        StartCoroutine(HeightTest());
        //Debug.Log(jump_up_power * jump_charge);
        rigid.AddForce((Vector2.up * jump_up_power) * jump_charge,ForceMode2D.Impulse);
        //velocity.y = jump_up_power *jump_charge;
        charge_img.enabled = false; //ui비활성화
        jump_charge = 0.0f;
        charge_img.fillAmount = 0.0f; //추가됨

        #region 이전코드
        //Debug.Log((Vector2.up * jump_up_power) * 50 * jump_charge);
        //rigid.AddForce((Vector2.up * jump_up_power) * 0.75f * jump_charge,ForceMode2D.Impulse);
        //rigid.velocity = (Vector2.right * jump_right_power + Vector2.up * jump_up_power) * jump_charge;
        //rigid.AddForce(Vector2.up * jump_up_power * jump_charge, ForceMode2D.Impulse); //차징한 만큼 점프
        //if(jump_cnt != 2) //2단 점프 구현
        //{
        //    jump_cnt++;
        //    rigid.velocity = Vector2.zero;
        //    rigid.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse);
        //}
        #endregion
    }
    IEnumerator HeightTest()
    {
        Vector3 trans= transform.position;
        Vector3 trans2 = trans;
        while (true)
        {
            trans = transform.position;
            yield return null;
            if(trans2.y > trans.y)
            {
                Debug.Log(trans2.y);
                break;
            }
            else
            {
                trans2 = trans; 
            }
        }
    }

    IEnumerator FloorCheck()
    {
        yield return new WaitForSeconds(0.1f);
        floorCheck = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.layer == 8)
        //{

        //}//jumping = false;
        //else if (collision.gameObject.layer == 9)
        //{
        //    Rock_HJH rock;
        //    if (collision.gameObject.TryGetComponent<Rock_HJH>(out rock))
        //    {
        //        rock.RockTouch();
        //    }
        //    collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        //    //collision.gameObject.SetActive(false);
        //    if(player_State == Player_State.Run)
        //        hp--;
        //}
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
        if (!nuckBackBool)
        {
            return;
        }
        nuckBackBool = false;
        invincible = true;
        rigid.velocity = Vector2.zero;
        rigid.AddForce((Vector2.left * nuckBackPower), ForceMode2D.Force);
        nuckBackDuring = true;
        StartCoroutine(NuckBackAddForce());
        StartCoroutine(Invincible());
        StartCoroutine(Blink());    
    }

    IEnumerator NuckBackAddForce()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            rigid.AddForce(Vector2.right * velocity);
            if (rigid.velocity.x > speed)
            {
                rigid.velocity = Vector2.right * speed;
                nuckBackDuring = false;
                break;
            }
        }
    }
    IEnumerator Invincible() //무적상태 일정 시간 후 꺼주는 코루틴
    {
        yield return new WaitForSeconds(invincibleTime);
        invincible = false;
    }
    IEnumerator Blink() //주인공 깜박이게
    {
        int count = 0;
        gameObject.SetActive(true);

        while (count < blinkCount)
        {
            playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b,0);
            yield return new WaitForSeconds(blinkSpeed);
            playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            yield return new WaitForSeconds(blinkSpeed);
            count+=2;
        }
    }
    IEnumerator Rolling() //구르기
    {
        //gameObject.GetComponent<BoxCollider2D>().isTrigger = true; //임시 주석
        playerAnimator.SetTrigger("Roll"); //구르기 애니메이션 작동
        yield return new WaitForSeconds(rolling_time); //일정시간동안 구르기진행
        player_State = Player_State.Run; //달리는 상태로 복귀
        //gameObject.GetComponent<BoxCollider2D>().isTrigger = false; //임시 주석
        playerAnimator.SetTrigger("RollEnd");
        rollStart = false;
        //playerAnimator.SetBool("Rolling", false);
    }
}
