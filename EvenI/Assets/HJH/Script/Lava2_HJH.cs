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
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        else
        {
            if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) < startDistance)
            {
                StartCoroutine(FireBall());
            }
        }

    }

    IEnumerator FireBall()
    {
        Vector2 moveVec = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
        transform.position += (Vector3)(moveVec * speed * Time.deltaTime);
        yield return null;
    }
}
