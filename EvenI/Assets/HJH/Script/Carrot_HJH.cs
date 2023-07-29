using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot_HJH : Object_Manager_shj
{
    public override void Item_Active(GameObject player)
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager_HJH>().JuksunSoundPlay();
        GameObject.Find("InGame_UI").GetComponent<InGame_UI_shj>().Count++;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
