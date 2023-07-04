using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling_Item : Object_Manager_shj
{
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.name == "Player")
    //    {
    //        collision.gameObject.GetComponent<Player_shj>().state = Player_State.Rolling;
    //        gameObject.SetActive(false);
    //    }
    //}
    public override void Item_Active(GameObject player)
    {
        player.GetComponent<Player_shj>().state = Player_State.Rolling;
    }
}
