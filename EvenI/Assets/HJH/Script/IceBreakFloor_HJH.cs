using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBreakFloor_HJH : Object_Manager_shj
{
    public float aniSpeed;
    bool touch = false;
    public override void Item_Active(GameObject player)
    {
        ItmeInActive = false;
        animator.SetFloat("Speed", aniSpeed);
        if (animator != null)
        {
            animator.SetTrigger("Touch");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !touch)
        {
            Active(collision.gameObject);
            touch = true;
        }
    }
}
