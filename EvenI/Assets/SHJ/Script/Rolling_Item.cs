using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling_Item : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<Player_shj>().state = Player_State.Rolling;
            gameObject.SetActive(false);
        }
    }
}
