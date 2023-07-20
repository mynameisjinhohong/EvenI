using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBreakFloor_HJH : Object_Manager_shj
{
    public override void Item_Active(GameObject player)
    {
        ItmeInActive = false;
        if (animator != null)
        {
            animator.SetTrigger("Touch");
        }
        Debug.Log("gogo");
    }
}
