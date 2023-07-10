using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_HJH : MonoBehaviour
{
    BoxCollider2D box;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Crash(GameObject player)
    {
        if(player.transform.position.y < transform.position.y || player.transform.position.x < transform.position.x - (transform.localScale.x/2))
        {
            Debug.Log("3 : " + player.transform.position.x.ToString() + "\n4 : " + (transform.position.x - (transform.localScale.x / 2)));
            box.isTrigger = true;
            StopAllCoroutines();
            StartCoroutine(TriggerOff());
        }
        else
        {
            Debug.Log("1 : " + player.transform.position.x.ToString() + "\n2 : " + (transform.position.x - (transform.localScale.x / 2)));
        }

    }

    IEnumerator TriggerOff()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("yo");
        box.isTrigger = false;
    }
}
