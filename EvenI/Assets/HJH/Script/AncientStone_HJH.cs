using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientStone_HJH : Object_Manager_shj
{
    public override void Item_Active(GameObject player)
    {
        GameObject.Find("SoundManager").GetComponent<SoundManager_HJH>().JuksunSoundPlay();
        GameObject.Find("InGame_UI").GetComponent<InGame_UI_shj>().ancientStoneCount++;
    }
}
