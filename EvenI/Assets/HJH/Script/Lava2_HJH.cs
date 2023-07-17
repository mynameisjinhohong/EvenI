using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava2_HJH : Object_Manager_shj
{
    Camera cam;
    GameObject player;
    public float startDistance;
    public float angle;
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
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            animator.SetTrigger("InCam");
        }
        else if(viewPos.x < -0.5)
        {
            Destroy(gameObject);
        }
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        else
        {
            if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) < startDistance)
            {
                if(!startCo)
                {

                    StartCoroutine(FireBall());
                    startCo = true;
                }
            }
        }

    }

    IEnumerator FireBall()
    {
        while (true)
        {
            Vector2 moveVec = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
            float ang = Mathf.Atan2(moveVec.y, moveVec.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(ang + 135, Vector3.forward);
            transform.position += (Vector3)(moveVec * speed * Time.deltaTime);
            yield return null;
        }

    }
}
