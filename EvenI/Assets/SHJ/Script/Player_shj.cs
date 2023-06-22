using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class Player_shj : MonoBehaviour
{
    #region Variable
    Rigidbody2D rigid;

    public LineRenderer predictLine;

    [Range(0.0f, 15.0f)]
    public float speed; //�ӵ�

    [Range(0.0f,15.0f)]
    public float jump_up_power; //���� ������

    [Range(0.0f, 15.0f)]
    public float jump_right_power; //���������� ������

    [Range(0.0f, 10.0f)]
    public float charge_speed; //���� ������ �������� �ӵ�
    [SerializeField]
    bool jumping = false; //���������� �ƴ��� Ȯ��
    float jump_charge = 0.0f; //������ ����
    public Image charge_img; //���� ������
    //int jump_cnt = 0; //����Ƚ�� 2�������� ���

    public Animator playerAnimator;
    public GameObject hp_List;
    int hp = 9;

    [Range(0.0f, 10.0f)]
    public float cameraSpeed;//ī�޶� ���ǵ�
    //public Map_shj map; //���� �ӵ� ������ ����


    #endregion

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, Vector2.down, 1f,LayerMask.GetMask("ground")); //�ٴ� �˻� �ؼ� ������ �� �ְ�
        if(hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            if (!jumping)
            {
                rigid.velocity = Vector2.right * speed; //���������� ���� ���� �ӵ� �����ϰ�
                predictLine.positionCount = 0; // �޸� ���� ���� �� �ȱ׷�����
            }
        }

        Camera.main.transform.position = new Vector3((transform.position+new Vector3(5.5f,0,0)).x,2,-10);


#if UNITY_EDITOR
        if (!jumping && Input.GetMouseButton(0))
        {
            charge_img.enabled = true; //uiȰ��ȭ
            jump_charge = jump_charge <= 1.0f ? jump_charge + Time.deltaTime * charge_speed : 1.0f; //��¡�ϸ� �������� �������ϴ�
            charge_img.fillAmount = jump_charge;
            Time.timeScale = 0.5f;
            PredictLine(transform.position, (Vector2.right * jump_right_power + Vector2.up * jump_up_power) * jump_charge);
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
                charge_img.enabled = true; //uiȰ��ȭ
                jump_charge = jump_charge <= 1.0f ? jump_charge + Time.deltaTime * charge_speed : 1.0f; //��¡�ϸ� �������� �������ϴ�
                charge_img.fillAmount = jump_charge;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                Jump();
        }
#endif
    }



    public void Jump()
    {
        playerAnimator.SetTrigger("Jump"); //���� �ִϸ��̼�

        jumping = true; //������
        charge_img.enabled = false; //ui��Ȱ��ȭ
        rigid.velocity = (Vector2.right * jump_right_power + Vector2.up * jump_up_power) * jump_charge;
        //rigid.AddForce(Vector2.up * jump_up_power * jump_charge, ForceMode2D.Impulse); //��¡�� ��ŭ ����
        jump_charge = 0.0f;

        //if(jump_cnt != 2) //2�� ���� ����
        //{
        //    jump_cnt++;
        //    rigid.velocity = Vector2.zero;
        //    rigid.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse);
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
            jumping = false;
            //jump_cnt = 0; //2������ �ʱ�ȭ
        else if (collision.gameObject.name == "Rock")
        {
            collision.gameObject.GetComponent<Rock_HJH>().RockTouch();
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            hp_List.transform.GetChild(hp).gameObject.SetActive(false);
            //collision.gameObject.SetActive(false);
            hp--;
        }
    }

    void PredictLine(Vector2 startPos, Vector2 vel)  //������ ����
    {

        int step = 120;
        float deltaTime = Time.fixedDeltaTime;
        Vector2 gravity = Physics.gravity;

        Vector2 position = startPos;
        Vector2 velocity = vel;

        predictLine.positionCount = 120;
        for (int i = 0; i < step; i++)
        {
            position += velocity * deltaTime + 0.5f * gravity * deltaTime * deltaTime;
            velocity += gravity * deltaTime;
            predictLine.SetPosition(i, position);
            Collider2D colls = Physics2D.OverlapCircle(position, 1.0f);
            if(colls != null && colls.transform.name != "Player")
            { 
                predictLine.positionCount = i; //�������� �ٸ� ��ü�� �浹�� ���̻� �׸��� �ʰ�
                break;
            }

        }
    }
}
