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

    public void Crash()
    {
        box.isTrigger = true;
        StopAllCoroutines();
        StartCoroutine(TriggerOff());
    }

    IEnumerator TriggerOff()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("yo");
        box.isTrigger = false;
    }
}
