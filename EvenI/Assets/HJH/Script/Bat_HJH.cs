using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_HJH : Object_Manager_shj
{
    GameObject player;
    Camera cam;
    [Range(0.0f, 10f)]
    public float speed;

    public float startDistance;
    bool startCo = false;
    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        Vector3 viewPos = cam.WorldToViewportPoint(transform.position);
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) < startDistance)
        {
            if (!startCo)
            {
                animator.SetTrigger("InCam");
                StartCoroutine(MoveBat());
                startCo = true;
            }
        }
    }

    IEnumerator MoveBat()
    {
        while(true)
        {
            Vector3 viewPos = cam.WorldToViewportPoint(transform.position);
            if (viewPos.x < -0.5f)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            yield return null;
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.gameObject.name == "Player")
    //    {
    //        collision.gameObject.GetComponent<Player_shj>().hp--;
    //        Destroy(gameObject);
    //    }
    //}
}
