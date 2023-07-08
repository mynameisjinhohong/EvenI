using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Main_Lobby_UI_shj : UI_Setting_shj
{
    public void UI_On_Off()
    {
        GameObject G_obj = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject;

        if (!G_obj.activeSelf)
        {
            G_obj.SetActive(true);
            root_UI = G_obj;
        }
    }
    public void Lastest_Open_UI_Close() //마지막 열린 UI닫기
    {
        if (root_UI != null) root_UI.SetActive(false);
    }

}
