using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Main_Lobby_UI_shj : UI_Setting_shj
{
    public AudioClip[] clips;
    public GameObject[] scenario_list;
    public Image[] playing_value;
    public Text nickname_text;

    public GameObject set_nickname;
    public GameObject continue_stage;
    public GameObject scenario_info;

    public Slider BGM_value;
    public Slider Effect_value;

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
        //senario = CSVReader.Read("Scenario/opening/opening_scenario");
        //next_text();

        BGM_value.value = GameManager_shj.Getinstance.Save_data.bgm_vol;
        Effect_value.value = GameManager_shj.Getinstance.Save_data.eff_vol;

        for (int i = 0; i < GameManager_shj.Getinstance.Save_data.playing.Length; i++)
            playing_value[i].fillAmount = GameManager_shj.Getinstance.Save_data.playing[i];

        for (int i = 0; i < GameManager_shj.Getinstance.Save_data.ending.Length; i++)
        {
            GameObject target = scenario_list[i];
            target = scenario_list[i].transform.GetChild(0).GetChild(0).gameObject;

            if (GameManager_shj.Getinstance.Save_data.ending[i]) target.transform.GetChild(0).gameObject.SetActive(true);
            else
            {
                target.transform.GetChild(1).gameObject.SetActive(true);
                scenario_list[i].GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                scenario_list[i].GetComponent<Button>().onClick.AddListener(Active_Info);
            }
        }
    }

    public void Active_Info() { scenario_info.SetActive(true); }

    public void Set_NickName() //닉네임 설정
    {
        if (nickname_text.text.Length != 0)
        {
            GameManager_shj.Getinstance.Save_data.nickname = nickname_text.text;
            GameManager_shj.Getinstance.Save_data.ending[0] = true;
            Data_Save();
            gamestart = true;
            Load_Story("opening");
        }
    }

    public void Play_Check()
    {
        if (GameManager_shj.Getinstance.Save_data.nickname.Length == 0 || GameManager_shj.Getinstance.Save_data.nickname == "")
            set_nickname.SetActive(true);

        else if(GameManager_shj.Getinstance.Save_data.last_play_scene_num > 2)
            continue_stage.SetActive(true);
        else
            Next_Scene();
    }

    public void Set_BGM_vol(Slider bar) { GameManager_shj.Getinstance.Volume_Set("BGM",bar.value); }

    public void Set_Effect_vol(Slider bar) { GameManager_shj.Getinstance.Volume_Set("Effect", bar.value); }

    //public override void Return_Scene(int num)
    //{
    //    if (num == 0)
    //        base.Return_Scene(GameManager_shj.Getinstance.Save_data.last_play_scene_num);
    //    else
    //        base.Return_Scene(num);
    //}

    //부모클래스에서 변경예정
    public void UI_On_Off() //교체 예정
    {
        GameObject G_obj = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject;
        if (EventSystem.current.currentSelectedGameObject.name == "Story_Dictionary_Btn")
        {
            if (!G_obj.activeSelf)
            {
                audio.clip = clips[(int)AudioType.StoryDic];
                audio.Play();

                G_obj.SetActive(true);
                root_UI = G_obj;
            }
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Challenge_Btn")
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

    public void Lastest_Open_UI_Close() //마지막 열린 UI닫기 교체예정
    {
        audio.clip = clips[(int)AudioType.UIOff];
        audio.Play();
        if (root_UI != null) root_UI.SetActive(false);
    }
    //여기까지
}
