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
    public Player_shj player;

    public GameObject ads;

    string gameID = "5343352";
    string adType = "Rewarded_Android";

    int count;

    int recovery;
    int next_scene_cnt = 0;

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
        //ī��Ʈ�ٿ� 3�� �ʿ�
        //Time.timeScale = 0.0f ���� �Ͻ�������
    }

    private void Update()
    {
        count_text.text = count.ToString();
    }

    public void Game_Stop() //���� ���� ��ư
    {
        Time.timeScale = 0.0f; //���� �Ͻ�����
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
               if(player.Hp + recovery <= 10)
                    player.Hp += recovery;

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
