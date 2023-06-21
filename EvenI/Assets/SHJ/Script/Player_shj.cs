using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player_shj : MonoBehaviour
{
    #region Variable
    Rigidbody2D rigid;

    [Range(0.0f,10.0f)]
    public float jump_power; //������

    [Range(0.0f, 10.0f)]
    public float charge_speed; //���� ������ �������� �ӵ�
    bool jumping = false; //���������� �ƴ��� Ȯ��
    float jump_charge = 0.0f; //������ ����
    public Image charge_img; //���� ������
    //int jump_cnt = 0; //����Ƚ�� 2�������� ���

    public Animator playerAnimator;
    public GameObject hp_List;
    int hp = 9;

    #endregion

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (!jumping && Input.GetMouseButton(0))
        {
            charge_img.enabled = true; //uiȰ��ȭ
            jump_charge = jump_charge <= 1.0f ? jump_charge + Time.deltaTime * charge_speed : 1.0f; //��¡�ϸ� �������� �������ϴ�
            charge_img.fillAmount = jump_charge;
        }
        else if (!jumping && Input.GetMouseButtonUp(0))
            Jump();

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

        rigid.AddForce(Vector2.up * jump_power * jump_charge, ForceMode2D.Impulse); //��¡�� ��ŭ ����
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
}
