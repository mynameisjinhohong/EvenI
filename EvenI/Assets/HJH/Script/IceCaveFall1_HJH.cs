using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCaveFall1_HJH : Object_Manager_shj
{
    GameObject player;
    public float startDistance;
    public float speed;
    bool startCo = false;
    float currentTime = 0;
    // Start is called before the first frame update
    void Awake()
    {
        
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
                if(!startCo)
                {
                    startCo = true;
                    StartCoroutine(IceFall());
                }
            }
        }
    }
    IEnumerator IceFall()
    {
       while(true)
        {
            currentTime += Time.deltaTime;
            transform.position += Vector3.down * Time.deltaTime * speed;
            if(currentTime > 5f)
            {
                gameObject.SetActive(false);
            }
            yield return null;
        }
    }

}
