using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player_shj : MonoBehaviour
{
    #region Variable
    Rigidbody2D rigid;

    [Range(0.0f,10.0f)]
    public float jump_power; //점프력

    [Range(0.0f, 10.0f)]
    public float charge_speed; //점프 게이지 차오르는 속도
    bool jumping = false; //점프중인지 아닌지 확인
    float jump_charge = 0.0f; //점프력 충전
    public Image charge_img; //점프 게이지
    //int jump_cnt = 0; //점프횟수 2단점프때 사용

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
            charge_img.enabled = true; //ui활성화
            jump_charge = jump_charge <= 1.0f ? jump_charge + Time.deltaTime * charge_speed : 1.0f; //차징하면 게이지가 차오릅니다
            charge_img.fillAmount = jump_charge;
        }
        else if (!jumping && Input.GetMouseButtonUp(0))
            Jump();

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
        playerAnimator.SetTrigger("Jump"); //점프 애니메이션

        jumping = true; //점프중
        charge_img.enabled = false; //ui비활성화

        rigid.AddForce(Vector2.up * jump_power * jump_charge, ForceMode2D.Impulse); //차징한 만큼 점프
        jump_charge = 0.0f;

        //if(jump_cnt != 2) //2단 점프 구현
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
            //jump_cnt = 0; //2단점프 초기화
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
