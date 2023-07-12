using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Main_Lobby_UI_shj : UI_Setting_shj
{
    AudioSource audio;
    public AudioClip[] clips;

    enum AudioType
    {
        UION,
        UIOff,
        StoryDic,
        PopUPClose,
        Lock,
    }
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        BackGround_Set();
    }

    public void UI_On_Off()
    {
        GameObject G_obj = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject;
        if(EventSystem.current.currentSelectedGameObject.name == "Story_Dictionary_Btn")
        {
            if (!G_obj.activeSelf)
            {
                audio.clip = clips[(int)AudioType.StoryDic];
                audio.Play();

                G_obj.SetActive(true);
                root_UI = G_obj;
            }
        }
        else if(EventSystem.current.currentSelectedGameObject.name == "Challenge_Btn")
        {
            audio.clip = clips[(int)AudioType.Lock];
            audio.Play();
        }
        else
        {
            if (!G_obj.activeSelf)
            {
                audio.clip = clips[(int)AudioType.UION];
                audio.Play();

                G_obj.SetActive(true);
                root_UI = G_obj;
            }
        }
    }
    public void Lastest_Open_UI_Close() //마지막 열린 UI닫기
    {
        audio.clip = clips[(int)AudioType.UIOff];
        audio.Play();
        if (root_UI != null) root_UI.SetActive(false);
    }

}
