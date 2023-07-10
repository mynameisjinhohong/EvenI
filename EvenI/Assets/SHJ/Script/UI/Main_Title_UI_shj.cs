using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Main_Title_UI_shj : UI_Setting_shj
{
    public Text nickname_text;
    public Text story_text;
    public Text btn_text;

    int click_cnt = -1;

    AudioSource audio;
    //세이브 파일 만들어지면 제작해야할것같음
    //처음 시작하게되면 닉네임 설정,오프닝 스토리 컷등장 <- 저장 데이터에서 읽어와야함
    //컷씬이 완료되면 다음씬으로 넘어감 .컷씬을 안보는경우 화면 클릭시 다음씬으로 변경

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        senario = CSVReader.Read("Scenario/opening_scenario");
        next_text();
    }

    //public void Next_Scene() //현재씬에서 다음 씬으로 넘어감
    //{
    //    audio.Play();
    //    Invoke("NextScene", 0.1f);
    //}

    //void NextScene()
    //{
    //    GameManager_shj.Getinstance.Change_Next_Scene(true);
    //}

    public void Set_NickName() //닉네임 설정
    {
        GameManager_shj.Getinstance.nickname = nickname_text.text;
    }

    public void next_text()
    {
        click_cnt++;

        if (senario.Count / 3 > click_cnt)
        {
            audio.Play();

            for (int i = 0; i < 3; i++)
            {
                string text = senario[i + 3 * click_cnt]["text"].ToString();

                //닉네임 부분을 변경
                if (text.Contains("(닉네임)")) text = text.Replace("(닉네임)", GameManager_shj.Getinstance.nickname);

                if (i == 0) story_text.text = text; 
                else story_text.text += "\n" + text;
            }

            if (click_cnt == senario.Count / 3 - 1) btn_text.text = "로비로 이동";
        }
        else
            Next_Scene();
    }
}
