using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeScrolling_HJH : MonoBehaviour
{
    public GameObject player;
    Player_shj play;
    public GameObject[] maps;
    int idx = 1;
    BoxCollider2D[] cols = new BoxCollider2D[2];
    public Vector3 firstPos;
    public Vector3 secondPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        play = player.GetComponent<Player_shj>();
        cols[0] = maps[0].GetComponent<BoxCollider2D>();
        cols[1] = maps[1].GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        firstPos = (Vector2)cols[0].transform.position + cols[0].offset + (cols[0].size / 2);
        secondPos = cols[1].offset + (cols[1].size / 2) + (Vector2)cols[1].transform.position;
        if (idx == 1)
        {
            if (playerPos.x > secondPos.x - (cols[1].size.x/2))
            {
                maps[0].transform.position += new Vector3(secondPos.x, 0, 0);

                idx = 0;
            }
        }
        else
        {
            if (playerPos.x > (firstPos.x - (cols[0].size.x / 2)))
            {
                maps[1].transform.position += new Vector3(firstPos.x,0,0);
                Debug.Log(maps[1].transform.position);
                Debug.Log(secondPos);
                idx = 1;
            }
        }


    }
}
