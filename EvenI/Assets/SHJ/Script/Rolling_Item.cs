using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling_Item : Object_Manager_shj
{
    public bool rolling = false;
    GameObject player;
    Camera cam;
    [Range(0.0f, 10f)]
    public float speed;
    [Range(0.0f, 10f)]
    public float maxMove;


    public float startDistance;
    bool startCo = false;

    void Awake()
    {
        cam = Camera.main;
        Vector3 viewPos = cam.WorldToViewportPoint(transform.position);
        player = GameObject.Find("Player");
    }


    private void Update()
    {
        if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) < startDistance)
        {
            if (!startCo)
            {
                StartCoroutine(MoveItem());
                startCo = true;
            }
        }
    }

    IEnumerator MoveItem()
    {
        float minY = transform.position.y;
        float maxY = transform.position.y + maxMove;
        bool up = true;
        while (true)
        {
            Vector3 viewPos = cam.WorldToViewportPoint(transform.position);
            if (viewPos.x < -0.5f)
            {
                Destroy(gameObject);
            }
            else
            {
                if (up)
                {
                    if(transform.position.y > maxY)
                    {
                        up = false;
                    }
                    transform.position += Vector3.up * speed * Time.deltaTime;
                }
                else
                {
                    if(transform.position.y < minY)
                    {
                        up = true;
                    }
                    transform.position += Vector3.down * speed * Time.deltaTime;
                }
            }
            yield return null;
        }
    }
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
