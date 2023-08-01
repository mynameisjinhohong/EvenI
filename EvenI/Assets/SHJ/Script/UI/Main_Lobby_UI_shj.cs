using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
using System;

public class Main_Lobby_UI_shj : UI_Setting_shj
{
    public AudioClip[] clips;
    public GameObject[] scenario_list;
    public Image[] playing_value;

    public Text nickname_text;
    public GameObject set_nickname;
    public GameObject continue_stage;
    public GameObject continue_stage_btn;
    public GameObject[] hidden_list;
    public Text charge_cnt_txt;
    public GameObject gameinfo;
    public GameObject info;
    public GameObject heart_charge;

    public Button panda_hos;
    public Slider BGM_value;
    public Slider Effect_value;

    public void Start()
    {
        init_set();
        BackGround_Set();
        Date_Check();

        //Advertisement.Initialize(gameID, true);

        if (GameManager_shj.Getinstance.Save_data.nickname.Length == 0 || GameManager_shj.Getinstance.Save_data.nickname == "")
            gameinfo.SetActive(true);

        if(GameManager_shj.Getinstance.Save_data.hp == GameManager_shj.Getinstance.Save_data.max_hp)
        {
            panda_hos.onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
            panda_hos.onClick.AddListener(() => Active_Info("판다가 아프지 않습니다!"));
        }

        charge_cnt_txt.text = "하트 무료 충전" + "\n" + "(" + GameManager_shj.Getinstance.Save_data.healcnt + "/5" + ")";
        hp_cnt.text = GameManager_shj.Getinstance.Save_data.hp.ToString();
        BGM_value.value = GameManager_shj.Getinstance.Save_data.bgm_vol;
        Effect_value.value = GameManager_shj.Getinstance.Save_data.eff_vol;

        for (int i = 0; i < GameManager_shj.Getinstance.Save_data.playing.Length; i++)
            playing_value[i].fillAmount = GameManager_shj.Getinstance.Save_data.playing[i];

        for (int i = 0; i < hidden_list.Length; i++)
        {
            if(!GameManager_shj.Getinstance.Save_data.hidden_open[i % 2])
            {
                hidden_list[i].GetComponentInChildren<Text>().text = "???";
                hidden_list[i].transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                hidden_list[i].GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
            }
            else
            {
                hidden_list[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                hidden_list[i].GetComponent<Button>().onClick.SetPersistentListenerState(1, UnityEngine.Events.UnityEventCallState.Off);
            }
        }

        for (int i = 0; i < GameManager_shj.Getinstance.Save_data.ending.Length; i++)
        {
            GameObject target = scenario_list[i];
            target = scenario_list[i].transform.GetChild(0).GetChild(0).gameObject;

            if (GameManager_shj.Getinstance.Save_data.ending[i])
            {
                target.transform.GetChild(0).gameObject.SetActive(true);
                scenario_list[i].GetComponent<Button>().onClick.SetPersistentListenerState(1, UnityEngine.Events.UnityEventCallState.Off);
            }
            else
            {
                target.transform.GetChild(1).gameObject.SetActive(true);
                scenario_list[i].GetComponent<Button>().onClick.SetPersistentListenerState(0, UnityEngine.Events.UnityEventCallState.Off);
                //scenario_list[i].GetComponent<Button>().onClick.AddListener(() =>Active_Info(""));
            }
        }
    }

    private void Update()
    {
        if (heart_charge.activeInHierarchy)
        {
            Date_Check();
            hp_cnt.text = GameManager_shj.Getinstance.Save_data.hp.ToString();
            if (GameManager_shj.Getinstance.Save_data.healcnt < 5 && Time_Check >= GameManager_shj.Getinstance.Save_data.nexthealtime)
            {
                heart_charge.transform.GetChild(1).gameObject.SetActive(true);
                if (heart_charge.transform.GetChild(2).gameObject.activeSelf) heart_charge.transform.GetChild(2).gameObject.SetActive(false);
            }
            else if(GameManager_shj.Getinstance.Save_data.healcnt == 5)
            {
                for (int i = 0; i < 3; i++)
                    heart_charge.transform.GetChild(i).gameObject.SetActive(false);

                heart_charge.transform.GetChild(3).gameObject.SetActive(true);
            }
            else
            {
                if(heart_charge.transform.GetChild(1).gameObject.activeSelf) heart_charge.transform.GetChild(1).gameObject.SetActive(false);
                heart_charge.transform.GetChild(2).gameObject.SetActive(true);

                int cnt = GameManager_shj.Getinstance.Save_data.nexthealtime - Time_Check;
                heart_charge.GetComponentInChildren<TextMeshProUGUI>().text = cnt / 60 + " : " + (cnt % 60).ToString("D2");
            }
        }
    }

    public void Active_Info(string txt)
    {
        info.SetActive(true);
        info.GetComponentInChildren<Text>().text = txt;
    }
    public int Time_Check { get { return int.Parse(DateTime.Now.ToString("HH")) * 3600 + int.Parse(DateTime.Now.ToString("mm")) * 60 + int.Parse(DateTime.Now.ToString("ss")); } }
    public void Active_Heal()
    {
        ShowAds(3);
        
        int nexthealtime = Time_Check + 300 < 86400 ? Time_Check + 300 : 86400;
        GameManager_shj.Getinstance.Save_data.nexthealtime = nexthealtime;
        GameManager_shj.Getinstance.Save_data.healcnt += 1;
        //GameManager_shj.Getinstance.Push_Alarm();
        charge_cnt_txt.text = "하트 무료 충전" + "\n" + "(" + GameManager_shj.Getinstance.Save_data.healcnt + "/5" + ")";
        Data_Save();
    }

    public void Date_Check()
    {
        if (GameManager_shj.Getinstance.Save_data.lastjoin != DateTime.Now.ToString("yyyy-MM-dd")) //날짜가 하루 지나면 힐카운트 초기화
        {
            GameManager_shj.Getinstance.Save_data.healcnt = 0;
            GameManager_shj.Getinstance.Save_data.lastjoin = DateTime.Now.ToString("yyyy-MM-dd");
            GameManager_shj.Getinstance.Save_data.nexthealtime = 0;
            Data_Save();
        }
    }

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

        else /*if(GameManager_shj.Getinstance.Save_data.last_play_scene_num >= 2)*/
        {
            continue_stage.SetActive(true);

            if (GameManager_shj.Getinstance.Save_data.last_play_scene_num <= 2)
                continue_stage_btn.SetActive(false);
        }
        //else
        //    Next_Scene();
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
