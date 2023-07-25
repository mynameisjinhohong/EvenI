using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFloor_HJH : MonoBehaviour
{
    public float speedChangeAmount = 1.5f;
    bool touch = false;
    bool Out = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !touch)
        {
            collision.GetComponent<Player_shj>().speed *= 1.5f;
            touch = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !Out)
        {
            collision.GetComponent<Player_shj>().speed /= 1.5f;
            Out = true;
        }
    }
}
