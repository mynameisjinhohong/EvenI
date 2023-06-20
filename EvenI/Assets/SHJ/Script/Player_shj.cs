using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_shj : MonoBehaviour
{
    Rigidbody2D rigid;
    [Range(0.0f,10.0f)]
    public float jump_power;

    public GameObject Hp_list;
    int hp = 9;

    int jump_cnt = 0;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        if(jump_cnt != 2)
        {
            jump_cnt++;
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.up * jump_power, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
            jump_cnt = 0;
        else if(collision.gameObject.name == "Rock")
        {
            Hp_list.transform.GetChild(hp).gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            hp--;
        }
    }
}
