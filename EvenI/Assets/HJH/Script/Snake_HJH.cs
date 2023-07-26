using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Snake_HJH : Object_Manager_shj
{
    GameObject player;
    public float startDistance;
    bool startCo = false;
    public float moveSpeed;
    public float moveTime;
    public float waitTime;
    float aniSpeed; 
    float currentTime;
    bool move = false;
    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        else
        {
            if (Mathf.Abs(player.transform.position.x - gameObject.transform.position.x) < startDistance)
            {
                if (!startCo)
                {
                    animator.SetTrigger("InCam");
                    StartCoroutine(Snake());
                    startCo = true;
                }
            }
        }
    }

    IEnumerator Snake()
    {
        move = true;
        aniSpeed = 1;
        animator.SetFloat("AniSpeed", aniSpeed);
        while (true)
        {
            currentTime += Time.deltaTime;
            if(move)
            {
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                if (currentTime > moveTime)
                {
                    currentTime = 0;
                    move = false;
                    aniSpeed = 0;
                    animator.SetFloat("AniSpeed", aniSpeed);
                }
            }
            else
            {
                if(currentTime > waitTime)
                {
                    currentTime = 0;
                    move = true;
                    aniSpeed = 1;
                    animator.SetFloat("AniSpeed", aniSpeed);
                }
            }
            yield return null;
        }
    }
    public override void Obstacle_Active(GameObject player)
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager_HJH>().ObjectBreakSoundPlay();
    }
}
