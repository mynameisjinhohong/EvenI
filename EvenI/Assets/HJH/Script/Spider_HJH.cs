using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spider_HJH : Object_Manager_shj
{
    GameObject spiderParent;
    GameObject player;
    public float startDistance;
    public float downSpeed;
    public float downTime;
    float currentTime;
    bool startCo = false;
    // Start is called before the first frame update
    void Awake()
    {
        spiderParent = transform.parent.gameObject;
    }

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
                    StartCoroutine(Spider());
                    startCo = true;
                }
            }
        }
    }

    IEnumerator Spider()
    {
        while (true)
        {
            currentTime += Time.deltaTime;
            spiderParent.transform.position += Vector3.down * downSpeed * Time.deltaTime;
            if(currentTime > downTime)
            {
                break;
            }
            yield return null;
        }
    }
}
