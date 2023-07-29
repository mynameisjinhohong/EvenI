using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFloor_HJH : MonoBehaviour
{
    public float speedChangeAmount = 1.5f;
    bool touch = false;
    bool Out = false;
    float nomalSpeed = 0;
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !touch)
        {
            Player_shj player = collision.GetComponent<Player_shj>();
            nomalSpeed = player.nomalSpeed;
            player.speed = nomalSpeed*speedChangeAmount;
            player.state = Player_State.IceFloor;
            touch = true;
        }
    }
}
