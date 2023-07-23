using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetChildScript_HJH : MonoBehaviour
{
    public bool On;
    // Start is called before the first frame update
    void Start()
    {
        if (On)
        {
            ChildAttachScript();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChildAttachScript()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.AddComponent<InvisiblsDestroy_HJH>();
        }
    }
}
