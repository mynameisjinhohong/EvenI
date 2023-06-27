using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editor_Guide_shj : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.inputString != "" && Input.inputString.Length == 1)
            Map_Editor_shj.Getinstance.Guide_Control(Input.inputString);
    }
}
