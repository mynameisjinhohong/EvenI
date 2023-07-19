using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_Lobby_UI_shj : UI_Setting_shj
{
    AudioSource audio;
    public AudioClip[] clips;

    public RectTransform text_pos;
    public Text nickname_text;
    public Text story_text;
    public Text btn_text;

    int click_cnt = -1;

    public GameObject main;
    public GameObject story;
    public GameObject set_nickname;
    public GameObject continue_stage;

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
        senario = CSVReader.Read("Scenario/opening/opening_scenario");
        next_text();
    }

    public void Set_NickName() //닉네임 설정
    {
        if (nickname_text.text.Length != 0)
        {
            GameManager_shj.Getinstance.Save_data.nickname = nickname_text.text;
            GameManager_shj.Getinstance.Data_Save();
            main.SetActive(false);
            story.SetActive(true);
        }
    }

    public void Play_Check()
    {
        if (GameManager_shj.Getinstance.Save_data.nickname.Length == 0)
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

                if (i == 2 && senario[i + 3 * click_cnt]["image_num"].ToString() != "")
                    story_bg.sprite = bg_image_list[int.Parse(senario[i + 3 * click_cnt]["image_num"].ToString())];
                else if (i == 2 && senario[i + 3 * click_cnt]["image_num"].ToString() == "")
                    story_bg.sprite = bg_image_list[0];

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

            if (click_cnt == senario.Count / 3 - 1) btn_text.text = "게임 시작!";
        }
        else
            Next_Scene();
    }

    public void UI_On_Off() //교체 예정
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
    public void Lastest_Open_UI_Close() //마지막 열린 UI닫기 교체예정
    {
        audio.clip = clips[(int)AudioType.UIOff];
        audio.Play();
        if (root_UI != null) root_UI.SetActive(false);
    }
    public override void Return_Scene(int num)
    {
        if (num == 0)
            base.Return_Scene(GameManager_shj.Getinstance.Save_data.last_play_scene_num);
        else
            base.Return_Scene(num);
    }
}
