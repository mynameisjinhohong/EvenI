using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum Player_State // �÷��̾��� ���� �޸���������, ������������
{
    Run,
    Rolling,
}

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
    public float speed; //�ӵ�

    [Header("���� ����")]
    [Range(0.0f, 15.0f)]
    public float jump_up_power; //���� ������

    //[Range(0.0f, 15.0f)]
    //public float jump_right_power; //���������� ������
    [Range(0.0f, 2.0f)]
    public float maxJumpPower; //���� ������ �������� �ӵ�

    [Range(0.0f, 2.0f)]
    public float minJumpPower; //���� ������ �������� �ӵ�

    [Range(0.0f, 10.0f)]
    public float charge_speed; //���� ������ �������� �ӵ�
    [Header("������ ����")]
    [Range(0.0f, 10.0f)]
    public float rolling_time; //������ �ð�
    [Range(0.0f, 10.0f)]
    public float rolling_Speed; //������ �ӵ�
    [Range(0.0f, 10.0f)]
    public float rolling_MoveSpeed;
    bool rollStart = false;

    [SerializeField]
    bool jumping = false; //���������� �ƴ��� Ȯ��
    bool floorCheck = true; //���� �ϰ� ��񵿾� �ٴ� üũ ���ϰ�
    float jump_charge = 0.0f; //������ ����
    public Image charge_img; //���� ������
    //int jump_cnt = 0; //����Ƚ�� 2�������� ���

    public Animator playerAnimator;
    [Header("ü�� ����")]
    public GameObject hp_List;
    public int maxHP = 9;


    int Hp = 9;

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
                    NukBack();
                }
                Hp = value;
            }
        }
    }
    [Header("�˹�� �����ð� ����")]
    [Range(0.0f,10.0f)]
    public float invincibleTime = 1f;
    [Range(0.0f, 1000.0f)]
    public float nuckBackPower = 1f;
    public int blinkCount;
    [Range(0.0f, 1f)]
    public float blinkSpeed;
    bool invincible = false;

    [Header("ī�޶� �ӵ�")]
    [Range(0.0f, 10.0f)]
    public float cameraSpeed;//ī�޶� ���ǵ�
    //public Map_shj map; //���� �ӵ� ������ ����


    public SpriteRenderer playerSprite;

    Player_State player_State;
    public Player_State state { set { player_State = value; } }

    #endregion
    //bool test = false;
    float test = 0.0f;
    private void Start()
    {
        playerAnimator.SetFloat("RollSpeed", rolling_Speed);
        hp = maxHP;
        rigid = GetComponent<Rigidbody2D>();
        player_State = Player_State.Run;
    }

    private void Update()
    {
        if (player_State == Player_State.Rolling && !rollStart)
        {
            StartCoroutine(Rolling());
            rollStart = true;
        }
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
        if (floorCheck)
        {
            test = 0.0f; ;
            RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, Vector2.down, transform.localScale.y, LayerMask.GetMask("ground")); //�ٴ� �˻� �ؼ� ������ �� �ְ�
            Debug.DrawRay(gameObject.transform.position, Vector2.down * hit.distance, Color.red);
            if (hit.collider != null)
            {
                jumping = false;
                if (!jumping && !invincible)
                {
                    if(player_State == Player_State.Rolling)
                    {
                        //rigid.velocity = Vector2.right * speed + Vector2.right * rolling_MoveSpeed;
                        transform.position += (Vector3)Vector2.right * rolling_MoveSpeed * Time.deltaTime;
                    }
                    else
                    {
                        //rigid.velocity = Vector2.right * speed; //���������� ���� ���� �ӵ� �����ϰ�
                        transform.position += (Vector3)Vector2.right * speed * Time.deltaTime;
                    }
                    if (!Input.GetMouseButton(0))
                    {
                        predictLine.positionCount = 0; // �޸� ���� ���� �� �ȱ׷�����
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

        Camera.main.transform.position = new Vector3((transform.position + new Vector3(camera_distance, 0, 0)).x, 2, -10);//�÷��̾����� ���缭 ī�޶� ��ġ
#if UNITY_EDITOR
        if (!jumping && Input.GetMouseButton(0))
        {
            jump_charge = jump_charge <= maxJumpPower ? jump_charge + Time.deltaTime * charge_speed : maxJumpPower; //��¡�ϸ� �������� �������ϴ�
            charge_img.enabled = true; //uiȰ��ȭ
            charge_img.fillAmount += Time.deltaTime/*jump_charge*/; //�����Ǿ���
            if (timeSlowOnOff)
            {
                Time.timeScale = timeSlowSpeed;
            }
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
            //test = true;\
            Jump();
            Time.timeScale = 1f;
        }


        if(jumping && test < 1.0f)
        {
            float angle = 75 * Mathf.Deg2Rad;
            Vector3 direction = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
            transform.position += (direction - transform.position) * 10 * Time.deltaTime;
            test += Time.deltaTime;
        }

#elif UNITY_ANDROID
        if (Input.touchCount > 0 && !jumping)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                charge_img.enabled = true; //uiȰ��ȭ
                jump_charge = jump_charge <= 1.0f ? jump_charge + Time.deltaTime * charge_speed : 1.0f; //��¡�ϸ� �������� �������ϴ�
                charge_img.fillAmount = jump_charge;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                Jump();
        }
#endif
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        float angle = 75 * Mathf.Deg2Rad;
        Vector3 direction = transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * 1.0f;
        Gizmos.DrawLine(transform.position, direction);
    }
    //private void FixedUpdate()
    //{
    //    if(test)
    //    {
    //        Jump();
    //        test = false;
    //    }mathf
    //}

    public void Jump() //����
    {
        floorCheck = false;
        StartCoroutine(FloorCheck());
        playerAnimator.SetTrigger("Jump"); //���� �ִϸ��̼�
        jumping = true; //������

        charge_img.enabled = false; //ui��Ȱ��ȭ
        jump_charge = 0.0f;
        charge_img.fillAmount = 0.0f; //�߰���

        #region �����ڵ�
        //Debug.Log((Vector2.up * jump_up_power) * 50 * jump_charge);
        //rigid.AddForce((Vector2.up * jump_up_power) * 0.75f * jump_charge,ForceMode2D.Impulse);
        //rigid.velocity = (Vector2.right * jump_right_power + Vector2.up * jump_up_power) * jump_charge;
        //rigid.AddForce(Vector2.up * jump_up_power * jump_charge, ForceMode2D.Impulse); //��¡�� ��ŭ ����
        //if(jump_cnt != 2) //2�� ���� ����
        //{
        //    jump_cnt++;
        //    rigid.velocity = Vector2.zero;
        //    rigid.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse);
        //}
        #endregion
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

    void PredictLine(Vector2 startPos, Vector2 vel)  //������ ����
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
                predictLine.positionCount = i; //�������� �ٸ� ��ü�� �浹�� ���̻� �׸��� �ʰ�
                break;
            }

        }
    }
    public void NukBack()
    {
        invincible = true;
        rigid.AddForce(Vector2.left * nuckBackPower); // �˹� �Լ�
        StartCoroutine(Invincible());
        StartCoroutine(Blink());    
    }

    IEnumerator Invincible() //�������� ���� �ð� �� ���ִ� �ڷ�ƾ
    {
        yield return new WaitForSeconds(invincibleTime);
        invincible = false;
    }
    IEnumerator Blink() //���ΰ� �����̰�
    {
        Debug.Log("??");
        int count = 0;
        gameObject.SetActive(true);

        while (count < blinkCount)
        {
            playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b,0);
            yield return new WaitForSeconds(blinkSpeed);
            playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            yield return new WaitForSeconds(blinkSpeed);
            count++;
        }
    }
    IEnumerator Rolling() //������
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        playerAnimator.SetTrigger("Roll"); //������ �ִϸ��̼� �۵�
        yield return new WaitForSeconds(rolling_time); //�����ð����� ����������
        player_State = Player_State.Run; //�޸��� ���·� ����
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        playerAnimator.SetTrigger("RollEnd");
        rollStart = false;
        //playerAnimator.SetBool("Rolling", false);
    }
}
