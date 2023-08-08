using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story_Event_shj : MonoBehaviour
{
    public GameObject story;
    public GameObject black;

    public void Story_Open() { story.SetActive(true); }
    public void Set_Off() { black.SetActive(false); }
}
