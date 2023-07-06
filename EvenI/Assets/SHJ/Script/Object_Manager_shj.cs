using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Item,
    Obstacle,
}

public class Object_Manager_shj : MonoBehaviour
{
    Animator animator;
    public Type type;

    bool touch = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void Item_Active(GameObject player) { }

    public void Active(GameObject player)
    {
        if(type == Type.Item) //아이템 일때
        {
            Item_Active(player);
            InActive();
        }
        else
        {
            //animator.SetTrigger("Touch");
            player.GetComponent<Player_shj>().hp--;
        }
    }

    public void InActive() { gameObject.SetActive(false); }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !touch)
        {
            Active(collision.gameObject);
            touch = true;
        }
    }
}
