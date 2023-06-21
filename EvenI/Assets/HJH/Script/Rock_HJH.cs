using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock_HJH : MonoBehaviour
{
    public Animator rockAni;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RockTouch()
    {
        rockAni.SetTrigger("Touch");
        
    }

    public void RockOff()
    {
        gameObject.SetActive(false);
    }
}
