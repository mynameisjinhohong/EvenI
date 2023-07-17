using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_HJH : Object_Manager_shj
{
    Camera cam;
    [Range(0.0f, 10f)]
    public float speed;

    bool startCo = false;
    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(transform.position);
        if(viewPos.x >=0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            if (!startCo)
            {
                StartCoroutine(MoveBat());
                startCo = true;
            }
        }
    }

    IEnumerator MoveBat()
    {
        Debug.Log("Go");
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
