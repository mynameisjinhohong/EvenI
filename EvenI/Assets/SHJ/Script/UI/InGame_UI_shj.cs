using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Advertisements;

public class InGame_UI_shj : UI_Setting_shj
{
    public TextMeshProUGUI count_text;
    public GameObject Hp;
    public Transform Hp_list;

    public GameObject ads;

    public GameObject count_down_txt;

    string gameID = "5343352";
    string adType = "Rewarded_Android";

    int count;

    int recovery;
    int next_scene_cnt = 0;

    bool gamestart;
    float countdown;

    public int Count { get {  return count; } set { count = value; } }

    private void Awake()
    {
        if(Hp_list.childCount == 0)
        {
            for (int i = 0; i < GameManager_shj.Getinstance.Save_data.hp; i++)
            {
                GameObject obj = Instantiate(Hp);
                obj.transform.parent = Hp_list;
            }
        }
        Advertisement.Initialize(gameID, true);
    }

    private void Start()
    {
        count = GameManager_shj.Getinstance.Save_data.juksun;
        player.GetComponent<Player_shj>().enabled = false;
        player.GetComponent<Animator>().enabled = false;
        gamestart = false;
        countdown = 3.9f;
        Time.timeScale = 1.0f;
        //카운트다운 3초 필요
    }

    private void Update()
    {
        count_text.text = count.ToString();

        if (!gamestart)
        {
            Count_down();
        }
    }

    void Count_down()
    {
        if (countdown < 1.0f)
        {
            gamestart = true;
            count_down_txt.GetComponent<TextMeshProUGUI>().text = "START!";
            player.GetComponent<Player_shj>().enabled = true;
            player.GetComponent<Animator>().enabled = true;
            StartCoroutine(Delay_active(1.0f, count_down_txt));
        }
        else 
        {
            count_down_txt.SetActive(true);
            countdown -= Time.deltaTime;
            count_down_txt.GetComponent<TextMeshProUGUI>().text = countdown.ToString("F0");

        }
    }

    public void Game_Stop() //게임 정지 버튼
    {
        Time.timeScale = 0.0f; //게임 일시정지
        //UI_On_Off();
    }

    public void Game_Continue()
    {
        //Lastest_Open_UI_Close();
        Time.timeScale = 1.0f;
    }


    public void ShowAds(int cnt)
    {
        if (Advertisement.IsReady())
        {
            recovery = cnt;
            ShowOptions options = new ShowOptions { resultCallback = ResultAds };
            Advertisement.Show(adType, options);
        }
    }

    void ResultAds(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Failed:
                break;
            case ShowResult.Skipped:
            case ShowResult.Finished:

                ads.SetActive(false);
               if(player.GetComponent<Player_shj>().Hp + recovery <= 10)
                    player.GetComponent<Player_shj>().Hp += recovery;

                break;
        }
    }

    public override void Data_change(int cnt)
    {
        base.Data_change(count);
    }

    //public void Next_Scene_num(int num) { next_scene_cnt = num; }


    //public override void Next_Scene()
    //{
    //    StartCoroutine(GameManager_shj.Getinstance.Change_Scene(next_scene_cnt));
    //}

}
