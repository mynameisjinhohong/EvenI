using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit_HJH : Object_Manager_shj
{
    public float jumpPower = 1f;
    public override void Item_Active(GameObject player)
    {
        ItmeInActive = false;
        if (Mathf.Abs(player.transform.position.x -gameObject.transform.position.x) > Mathf.Abs(player.transform.position.y - gameObject.transform.position.y))
        {
            GameObject.Find("SoundManager").GetComponent<SoundManager_HJH>().ObjectBreakSoundPlay();
            if (player.GetComponent<Player_shj>().state != Player_State.Rolling) player.GetComponent<Player_shj>().hp--;
        }
        else
        {
            if (animator != null) animator.SetTrigger("Solve");
            Player_shj play =  player.GetComponent<Player_shj>();
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            play.jumpBool = true;
            play.jump_charge = jumpPower;
        }
    }
}
