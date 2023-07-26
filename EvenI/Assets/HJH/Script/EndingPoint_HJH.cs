using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPoint_HJH : MonoBehaviour
{
    Camera cam;
    Vector3 firstPos;
    public Player_shj player;
    // Start is called before the first frame update
    void Start()
    {
        firstPos = transform.position;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = firstPos;
        Vector3 viewPos = cam.WorldToViewportPoint(transform.position);
        if (viewPos.x >= 0 && viewPos.x <= 0.9)
        {
            player.gameClear = true;
        }
    }
}
