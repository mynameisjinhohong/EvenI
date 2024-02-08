using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class InGame_UI_shj : UI_Setting_shj
{
    #region 임시 변수
    public GameObject endingPopUpBtn;
    public Text jukSunText;
    public Transform StoryEndImage;
    public GameObject SomeThingOpen;
    public Sprite[] hidden_img_list;
    #endregion

    public TextMeshProUGUI carrot_text;
    public Image carrot_heart;
    public float carrotCount = 0;

    public TextMeshProUGUI ancientStone_text;
    public int ancientStoneCount = 0;

    public TextMeshProUGUI count_text;

    public GameObject select_panel;

    public Slider playing_slider;
    public GameObject end_pos;

    int count;
    int next_scene_cnt = 0;
    bool btn_down;
    //bool game_start;


    public int Count { get {  return count; } set { count = value; } }
    
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

        Advertisement.Initialize(gameID, false, this);

        count = GameManager_shj.Getinstance.Save_data.juksun;
        carrotCount = GameManager_shj.Getinstance.Save_data.carrot;
        ancientStoneCount = GameManager_shj.Getinstance.Save_data.ancientRock;
        playerScript = player.GetComponent<Player_shj>();
        if(SceneManager.GetActiveScene().buildIndex > 29)
        {
            playerScript.hp = 1;
        }
        else
        {
            playerScript.hp = GameManager_shj.Getinstance.Save_data.hp;

        }
    }

    public void Start()
    {
        btn_down = false;
        string scene_name = SceneManager.GetActiveScene().name;
        count_down_txt.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = scene_name.Substring(0, 3) + " STAGE";

        TextMeshProUGUI[] tmpro_list = count_down_txt.GetComponentsInChildren<TextMeshProUGUI>();

        for (int i = 0; i < tmpro_list.Length; i++)
            tmpro_list[i].color = new Color(1,1,1,1);

        init_set();
        Time.timeScale = 1.0f;
        //Time.fixedDeltaTime = 0.0f; //밀림현상때문에 생성, 카운트다운 버벅임 원인의심
        respawn = false;
        count_down_txt.SetActive(true);
        playerScript.enabled = false;
        count_down_txt.GetComponent<Animator>().Play("padeout", 0, 0f);
        player.GetComponent<Animator>().enabled = false;
        //game_start = false;
        //gamestart = false;
        countdown = 3.4f;

        next_scene_cnt = Scene_num > 4 ? 6 : 1;

        if(Select_chk)
        {
            string[] stage_title = { "지하동굴", "얼음동굴", "용암동굴", "정글", "설산" };

            int num = Scene_num;
            int stage_num = num == 4 ? 2 : 3;
            select_panel.transform.GetChild(0).GetChild(0).GetComponentInChildren<Button>().onClick.AddListener(() => Set_cnt(next_scene_cnt));
            //select_panel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(Change_Scene);
            select_panel.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = background_list[(num + next_scene_cnt) / 5 - 1];
            select_panel.transform.GetChild(0).GetChild(0).GetComponentInChildren<Text>().text = stage_title[(num + next_scene_cnt) / 5 - 1];

            select_panel.transform.GetChild(0).GetChild(1).GetComponentInChildren<Button>().onClick.AddListener(() => Set_cnt(next_scene_cnt + 5));
            //select_panel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(Change_Scene);
            select_panel.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = background_list[(num + next_scene_cnt + 5) / 5 - 1];
            select_panel.transform.GetChild(0).GetChild(1).GetComponentInChildren<Text>().text = stage_title[(num + next_scene_cnt + 5) / 5 - 1];

            select_panel.GetComponentInChildren<TextMeshProUGUI>().text = stage_num + " START";
        }
    }

    void Set_cnt(int value)
    {
        //if (btn_down) return;
        next_scene_cnt = value;
        Change_Scene();
        //btn_down = true;
    }

    private void Update()
    {
        carrot_text.text = carrotCount.ToString("F0") + "%";
        carrot_heart.fillAmount = carrotCount / 100;
        if(carrotCount > 99.5)
        {
            carrotCount -= 100;
            playerScript.hp++;
            GetComponent<AudioSource>().Play();
        }
        ancientStone_text.text = ancientStoneCount.ToString();
        count_text.text = count.ToString();
        hp_cnt.text = playerScript.hp.ToString();

        if (!gamestart) Count_down();

        playing_slider.value = playing_slider.value < 1.0f ?
            player.transform.position.x / end_pos.transform.position.x : 1.0f;
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

    public void Restart_Check()
    {
        respawn = true;
        ShowAds(0);
        //GameObject player = GameObject.Find("Player");
        //Vector3 player_pos = player.transform.localPosition;
        //int hp = player.GetComponent<Player_shj>().hp;

        //RaycastHit2D hit = Physics2D.Raycast(player_pos + new Vector3(- 15f, 20, 0), Vector3.down, 30.0f);

        //if(player.GetComponent<Player_shj>().hp < 3) ShowAds(ads);
        //else if (player_pos.y <= -6)
        //{
        //    hp -= 2;
        //    ads.SetActive(false);
        //}

        //gamestart = false;
        //player.GetComponent<Player_shj>().gameOverPanel.SetActive(false);
        //player.transform.localPosition = hit.point;
        //Camera.main.transform.position = (Vector3)hit.point + new Vector3(player.GetComponent<Player_shj>().camera_distance, 2, -10);
        ////player.GetComponent<Player_shj>().Start();
        //player.GetComponent<Player_shj>().hp = hp;

        //Start();
    }

    public void Ending_Check()
    {
        int num = Scene_num < 30 ? (Scene_num / 5) - 2 : (Scene_num / 4) - 2;
        if (count < 1700 && Scene_num < 30) num = 4;

        switch (num)
        {
            case 1:
                Load_Story("ending1");
                break;
            case 2:
                Load_Story("ending2");
                break;
            case 3:
                Load_Story("ending3");
                break;
            case 4:
                Load_Story("ending4");
                break;
            case 5:
                Load_Story("hidden1");
                break;
            case 6:
                Load_Story("hidden2");
                break;
        }
        //GameManager_shj.Getinstance.Save_data.ending[num] = true;
        Data_Reset();
        Data_Save();
    }

    public void Select_Check(GameObject obj)
    {
        if (Select_chk)
        {
            obj.SetActive(false);
            select_panel.SetActive(true);
        }
        else
            Change_Scene();
    }

    public void Change_Scene()
    {
        if (btn_down) return;
        btn_down = true;
        int stage_num = Scene_num / 5;

        if (stage_num < 6)
        {
            GameManager_shj.Getinstance.Save_data.playing[stage_num] += stage_num != 0 ? 0.2f : 0.34f;
            if (GameManager_shj.Getinstance.Save_data.playing[stage_num] > 1.0f)
                GameManager_shj.Getinstance.Save_data.playing[stage_num] = 1.0f;
            if (Select_chk)
            {
                Return_Scene(Scene_num + next_scene_cnt);
                Data_change(count, carrotCount, ancientStoneCount, next_scene_cnt);
            }
            else
            {
                Next_Scene();
                Data_change(count, carrotCount, ancientStoneCount, 1);
            }
        }
        else
        {
            Next_Scene();
            Data_change(0, 0, 0, 1);
        }

    }

    public override void HiddenOpenCheck()
    {
        endingPopUpBtn.SetActive(true);
        jukSunText.text = "X " + count;
        int num = Scene_num < 30 ? (Scene_num / 5) - 2 : (Scene_num / 4) - 2;
        if (GameManager_shj.Getinstance.Save_data.ending[num] == false)
        {
            GameObject some = Instantiate(SomeThingOpen, StoryEndImage);
            some.transform.SetSiblingIndex(1);
            some.transform.GetChild(0).gameObject.GetComponent<Text>().text = "ENDING.N0 " + num + " 해금!";

            if (count < 1700 && Scene_num < 30) num = 4;

            switch (num)
            {
                case 1:
                    some.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = ending1_img[0];
                    break;
                case 2:
                    some.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = ending2_img[0];
                    break;
                case 3:
                    some.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = ending3_img[0];
                    break;
                case 4:
                    some.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = ending4_img[0];
                    break;
                case 5:
                    some.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = hidden1_img[0];
                    some.transform.SetSiblingIndex(1);
                    some.transform.GetChild(0).gameObject.GetComponent<Text>().text = "무릉도원 챌린지 오픈!";
                    some.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = hidden_img_list[0];
                    GameManager_shj.Getinstance.Save_data.hidden_open[2] = true;
                    break;
                case 6:
                    some.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = hidden2_img[0];
                    some.transform.SetSiblingIndex(1);
                    some.transform.GetChild(0).gameObject.GetComponent<Text>().text = "고대유적 챌린지 오픈!";
                    some.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = hidden_img_list[1];
                    GameManager_shj.Getinstance.Save_data.hidden_open[3] = true;
                    break;
            }

            GameManager_shj.Getinstance.Save_data.ending[num] = true;
        }
        Debug.Log(count);
        //현재 사진 반대로나오고잇음 무릉-고대
        if (/*GameManager_shj.Getinstance.Save_data.ancientRock*/ancientStoneCount >= player.GetComponent<Player_shj>().ancientMax && GameManager_shj.Getinstance.Save_data.hidden_open[1] == false)
        {
            GameObject some = Instantiate(SomeThingOpen, StoryEndImage);
            some.transform.SetSiblingIndex(1);
            some.transform.GetChild(0).gameObject.GetComponent<Text>().text = "고대유적 Stage 오픈!";
            some.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = hidden_img_list[1];
            GameManager_shj.Getinstance.Save_data.hidden_open[1] = true;

            //GameManager_shj.Getinstance.Save_data.hidden_open[0] = true;
        }
        if (/*GameManager_shj.Getinstance.Save_data.juksun*/count >= player.GetComponent<Player_shj>().juksunMax && GameManager_shj.Getinstance.Save_data.hidden_open[0] == false)
        {
            GameObject some = Instantiate(SomeThingOpen, StoryEndImage);
            some.transform.SetSiblingIndex(1);
            some.transform.GetChild(0).gameObject.GetComponent<Text>().text = "무릉도원 Stage 오픈!";
            some.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = hidden_img_list[0];
            GameManager_shj.Getinstance.Save_data.hidden_open[0] = true;

            //GameManager_shj.Getinstance.Save_data.hidden_open[1] = true;
        }
        GameManager_shj.Getinstance.Data_Save();
    }

    public void GoMainScene()
    {
        SceneManager.LoadScene("Main_Lobby");
    }
}
