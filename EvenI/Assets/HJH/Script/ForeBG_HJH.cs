using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForeBG_HJH : MonoBehaviour
{
    SpriteRenderer sprite;
    public float alpha = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        sprite = transform.parent.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && collision.GetComponent<Player_shj>().enabled)
        {
            Color a = sprite.color;
            a.a = alpha;
            sprite.color = a; 

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.GetComponent<Player_shj>().enabled)
        {
            Color a = sprite.color;
            a.a = 1;
            sprite.color = a;

        }
    }
}
