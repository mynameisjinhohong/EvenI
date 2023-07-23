using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSetChildScript_HJH : MonoBehaviour
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
            SetChildScript_HJH set= transform.GetChild(i).gameObject.AddComponent<SetChildScript_HJH>();
            set.On = true;
        }
    }
}
