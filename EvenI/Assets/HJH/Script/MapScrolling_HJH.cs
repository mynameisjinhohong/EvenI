using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScrolling_HJH : MonoBehaviour
{
    [Range(0.0f, 10.0f)]
    public float speed;
    public GameObject player;
    public GameObject[] bgs;
    public float distance;
    int nowBg;
    Vector3 bgSize;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        bgSize = GetBGSize(bgs[0]);
        Debug.Log(bgSize);
        nowBg = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (player.transform.position.x  > (bgs[nowBg].transform.position + bgSize/2).x +distance)
        {
            bgs[nowBg].transform.position = new Vector3(bgs[nowBg].transform.position.x + (bgSize.x *4), bgs[nowBg].transform.position.y, 0);
            nowBg++;
            if(nowBg > 3)
            {
                nowBg = 0;
            }
        }
    }
    public Vector3 GetBGSize(GameObject bG)
    {
        Vector2 bGSpriteSize = bG.GetComponent<SpriteRenderer>().sprite.rect.size;
        Vector2 localbGSize = bGSpriteSize / bG.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        Vector3 worldbGSize = localbGSize;
        worldbGSize.x *= bG.transform.lossyScale.x;
        worldbGSize.y *= bG.transform.lossyScale.y;
        return worldbGSize;
    }
}
