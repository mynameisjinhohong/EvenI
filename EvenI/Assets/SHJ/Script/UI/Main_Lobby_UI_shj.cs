using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Main_Lobby_UI_shj : UI_Setting_shj
{
    AudioSource audio;
    public AudioClip[] clips;
    public GameObject[] scenario_list;



    public Sprite[] opening_img;
    public Sprite[] ending1_img;
    public Sprite[] ending2_img;
    public Sprite[] ending3_img;
    public Sprite[] ending4_img;
    public Sprite[] hidden1_img;
    public Sprite[] hidden2_img;

    public RectTransform text_pos;
    public Text nickname_text;
    public Text story_text;
    public Text btn_text;

    int click_cnt;

    public GameObject main;
    public GameObject story;
    public GameObject set_nickname;
    public GameObject continue_stage;
    public GameObject scenario_info;

    public Slider BGM_value;
    public Slider Effect_value;

    bool gamestart;


    //string[] scenario_name = { "opening", "ending1", "ending2", "ending3", "ending4", "hidden1", "hidden1" };


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
        gamestart = false;
        audio = GetComponent<AudioSource>();
        BackGround_Set();
        //senario = CSVReader.Read("Scenario/opening/opening_scenario");
        //next_text();
        click_cnt = -1;
        BGM_value.value = GameManager_shj.Getinstance.Save_data.bgm_vol;
        Effect_value.value = GameManager_shj.Getinstance.Save_data.eff_vol;

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

    public void next_text()
    {
        click_cnt++;

        if (senario.Count / 3 > click_cnt)
        {
            audio.Play();

            //Debug.Log(scene_image_num[0]);
            //if (scene_image_num[scenario_cnt] == click_cnt)
            //{
            //    story_bg.sprite = scene_image_list[image_num[scenario_cnt]];
            //    scenario_cnt++;
            //}
            //else
            //    story_bg.sprite = scene_image_list[0];

            for (int i = 0; i < 3; i++)
            {
                string text = senario[i + 3 * click_cnt]["text"].ToString();

                //수정필요
                if (i == 2 && senario[i + 3 * click_cnt]["image_num"].ToString() != "")
                {
                    story_bg.sprite = scenario_img[int.Parse(senario[i + 3 * click_cnt]["image_num"].ToString()) - 1];
                    Debug.Log(scenario_img[int.Parse(senario[i + 3 * click_cnt]["image_num"].ToString()) - 1].name);
                }

                if (i == 2 && senario[i + 3 * click_cnt]["font_size"].ToString() != "")
                    story_text.fontSize = int.Parse(senario[i + 3 * click_cnt]["font_size"].ToString());
                else story_text.fontSize = 30;

                if (i == 2 && senario[i + 3 * click_cnt]["pos_y"].ToString() != "")
                    text_pos.anchoredPosition = new Vector2(0, -10);
                else text_pos.anchoredPosition = new Vector2(0, 10);

                //닉네임 부분을 변경
                if (text.Contains("(닉네임)")) text = text.Replace("(닉네임)", GameManager_shj.Getinstance.Save_data.nickname);

                if (i == 0) story_text.text = text;
                else story_text.text += "\n" + text;
            }
            btn_text.text = "다음";
            if (click_cnt == senario.Count / 3 - 1) //스토리 끝났을때
            {
                if (gamestart) btn_text.text = "게임 시작!";
                else btn_text.text = "돌아 가기";
            }
        }
        else
        {
            if (gamestart) Next_Scene();
            else
            {
                main.SetActive(true);
                story.SetActive(false);
            }
        }
    }

    public void Skip_btn()
    {
        if (gamestart) Next_Scene();
        else
        {
            story.SetActive(false);
            main.SetActive(true);
        }
    }

    public void Set_BGM_vol(Slider bar) { GameManager_shj.Getinstance.Volume_Set("BGM",bar.value); }

    public void Set_Effect_vol(Slider bar) { GameManager_shj.Getinstance.Volume_Set("Effect", bar.value); }

    public void Load_Story(string scenario_name)
    {
        switch(scenario_name)
        {
            case "opening":scenario_img = opening_img;
                break;

            case "ending1":scenario_img = ending1_img;
                break;

            case "ending2": scenario_img = ending2_img;
                break;

            case "ending3":scenario_img = ending3_img;
                break;

            case "ending4":scenario_img = ending4_img;
                break;

            case "hidden1":scenario_img = hidden1_img;
                break;

            case "hidden2": scenario_img = hidden2_img;
                break;
        }

        main.SetActive(false);
        story.SetActive(true);
        click_cnt = -1;
        senario = CSVReader.Read("Scenario/"+ scenario_name + "/" + scenario_name + "_scenario");
        next_text();
    }
   
    //public override void Return_Scene(int num)
    //{
    //    if (num == 0)
    //        base.Return_Scene(GameManager_shj.Getinstance.Save_data.last_play_scene_num);
    //    else
    //        base.Return_Scene(num);
    //}

    public void btn_test() //버튼 테스트용
    {
        Debug.Log(1);
    }

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
