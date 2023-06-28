using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UI_Setting_shj : MonoBehaviour
{
    public UI_Setting_shj uI_Setting;


    public void UI_On_Off()
    {
        GameObject G_obj = EventSystem.current.currentSelectedGameObject;
        bool on_off = G_obj.activeSelf == true ? false : true; ;
        G_obj.SetActive(on_off);
    }

    public void Game_Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif 
    }
}
