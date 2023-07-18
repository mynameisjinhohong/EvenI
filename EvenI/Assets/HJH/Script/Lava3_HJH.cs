using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava3_HJH : Object_Manager_shj
{
    Camera cam;
    public GameObject fireBall;
    GameObject player;
    public float aniSpeed;
    public float startDistance;
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
            animator.SetFloat("AniSpeed", aniSpeed);
        }
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        else
        {
            if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) < startDistance)
            {
                animator.SetTrigger("InCam");
                fireBall.SetActive(true);
            }
        }
    }
    public override void Obstacle_Active(GameObject player)
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager_HJH>().ObjectBreakSoundPlay();
    }
}
