using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_HJH : MonoBehaviour
{
    BoxCollider2D box;
    SpriteRenderer sprite;
    Vector3 bgSize;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        bgSize = GetBGSize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Crash(GameObject player)
    {
        if(player.transform.position.y < transform.position.y || player.transform.position.x < transform.position.x - (bgSize.x/2))
        {
            
            //("3 : " + player.transform.position.x.ToString() + "\n4 : " + (transform.position.x - (transform.localScale.x / 2)));
            box.isTrigger = true;
            StopAllCoroutines();
            StartCoroutine(TriggerOff());
        }
        else
        {
            //Debug.Log("1 : " + player.transform.position.x.ToString() + "\n2 : " + (transform.position.x - (transform.localScale.x / 2)));
        }

    }

    IEnumerator TriggerOff()
    {
        yield return new WaitForSeconds(0.5f);
        box.isTrigger = false;
    }
    public Vector3 GetBGSize()
    {
        Vector2 bGSpriteSize = sprite.sprite.rect.size;
        Vector2 localbGSize = bGSpriteSize / sprite.sprite.pixelsPerUnit;
        Vector3 worldbGSize = localbGSize;
        worldbGSize.x *= gameObject.transform.lossyScale.x;
        worldbGSize.y *= gameObject.transform.lossyScale.y;
        return worldbGSize;
    }
}
