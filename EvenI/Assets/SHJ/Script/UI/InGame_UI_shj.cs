using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;


public class InGame_UI_shj : UI_Setting_shj
{
    public TextMeshProUGUI count_text;
    public TextMeshProUGUI hp_cnt;
    public GameObject Hp;
    public Transform Hp_list;

    public GameObject count_down_txt;
    public GameObject select_panel;

    string gameID = "5343352";
    string adType = "Rewarded_Android";

    int count;

    int next_scene_cnt = 0;

    bool gamestart;
    float countdown;

    Vector3 hitpoint;

    List<int> scene_chk = new List<int>() { 3 };
    public int Count { get {  return count; } set { count = value; } }
    public bool Ending { get { return (SceneManager.GetActiveScene().buildIndex / 3 > 3 && SceneManager.GetActiveScene().buildIndex % 3 != 0); } };



    private void Awake()
    {
        //if(Hp_list.childCount == 0)
        //{
        //    for (int i = 0; i < GameManager_shj.Getinstance.Save_data.hp; i++)
        //    {
        //        GameObject obj = Instantiate(Hp);
        //        obj.transform.parent = Hp_list;
        //    }
        //}
        Advertisement.Initialize(gameID, true);
        count = GameManager_shj.Getinstance.Save_data.juksun;
        player.GetComponent<Player_shj>().hp = GameManager_shj.Getinstance.Save_data.hp;
    }

    private void Start()
    {
        player.GetComponent<Player_shj>().enabled = false;
        player.GetComponent<Animator>().enabled = false;
        gamestart = false;
        countdown = 3.9f;
        Time.timeScale = 1.0f;
        //카운트다운 3초 필요
        next_scene_cnt = SceneManager.GetActiveScene().buildIndex > 3 ? 4 : 1;
        if(Select_chk)
        {
            int num = SceneManager.GetActiveScene().buildIndex;
            select_panel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => Set_cnt(next_scene_cnt));
            select_panel.transform.GetChild(0).GetComponent<Image>().sprite = background_list[(num + 1) / 3 - 1];

            select_panel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => Set_cnt(next_scene_cnt + 3));
            select_panel.transform.GetChild(1).GetComponent<Image>().sprite = background_list[(num + 4) / 3 - 1];

        }
    }

    void Set_cnt(int value) { next_scene_cnt = value; }

    private void Update()
    {
        count_text.text = count.ToString();
        hp_cnt.text = player.GetComponent<Player_shj>().hp.ToString();
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
            count_down_txt.GetComponent<TextMeshProUGUI>().text = SceneManager.GetActiveScene().name +  "\nSTART!";
            count_down_txt.GetComponent<TextMeshProUGUI>().fontSize = 140;
            player.GetComponent<Player_shj>().enabled = true;
            player.GetComponent<Animator>().enabled = true;
            StartCoroutine(Delay_active(1.0f, count_down_txt));
        }
        else 
        {
            count_down_txt.SetActive(true);
            countdown -= Time.deltaTime;
            count_down_txt.GetComponent<TextMeshProUGUI>().text = countdown.ToString("F0");
            count_down_txt.GetComponent<TextMeshProUGUI>().fontSize = 200;

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


    public void ShowAds(GameObject ads)
    {
        if (Advertisement.IsReady())
        {
            ShowOptions options = new ShowOptions { resultCallback = ResultAds };
            Advertisement.Show(adType, options);
        }
        ads.SetActive(false);
    }

    void ResultAds(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Failed:
                break;
            case ShowResult.Skipped:
            case ShowResult.Finished:

               if(player.GetComponent<Player_shj>().hp + 1 <= 10)
                    player.GetComponent<Player_shj>().hp += 1;
                break;
        }
    }

    public void Restart_Check(GameObject ads)
    {
        GameObject player = GameObject.Find("Player");
        Vector3 player_pos = player.transform.localPosition;
        int hp = player.GetComponent<Player_shj>().hp;

        RaycastHit2D hit = Physics2D.Raycast(player_pos + new Vector3(- 15f, 20, 0), Vector3.down, 30.0f);

        if(player.GetComponent<Player_shj>().hp < 3) ShowAds(ads);
        else if (player_pos.y <= -6)
        {
            hp -= 2;
            ads.SetActive(false);
        }

        gamestart = false;
        player.GetComponent<Player_shj>().gameOverPanel.SetActive(false);
        player.transform.localPosition = hit.point;
        Camera.main.transform.position = (Vector3)hit.point + new Vector3(player.GetComponent<Player_shj>().camera_distance, 2, -10);
        player.GetComponent<Player_shj>().Start();
        player.GetComponent<Player_shj>().hp = hp;

        Start();
    }

    public bool Select_chk { get { return scene_chk.Contains(SceneManager.GetActiveScene().buildIndex); } }

    public void Change_Scene()
    {
        if (Select_chk) Return_Scene(SceneManager.GetActiveScene().buildIndex + next_scene_cnt);
        else if(SceneManager.GetActiveScene().buildIndex == 6)
            Return_Scene(10);
        else Next_Scene();

        Data_change(count, next_scene_cnt);
    }

    //public void Next_Scene_num(int num) { next_scene_cnt = num; }

    //public override void Next_Scene()
    //{
    //    StartCoroutine(GameManager_shj.Getinstance.Change_Scene(next_scene_cnt));
    //}
}
