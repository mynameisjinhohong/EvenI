using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class UI_Setting_shj : MonoBehaviour, IPointerClickHandler
{
    protected AudioSource audio;

    public Sprite[] background_list; //맵 배경
    public Image background; //맵 배경을 보이게 할 이미지 컴포넌트
    public Image story_bg; //스토리를 보이게 할 이미지 컴포넌트
    public RectTransform text_pos;
    public Text story_text;
    public Text btn_text;

    protected Sprite[] scenario_img; //시나리오 이미지 배열

    protected List<Dictionary<string, object>> senario; //CSV로 불러올 시나리오 내용
    protected GameObject root_UI = null; //없어도 될듯 삭제예정

    public GameObject player;
    public GameObject main;
    public GameObject story;

    public Sprite[] opening_img;
    public Sprite[] ending1_img;
    public Sprite[] ending2_img;
    public Sprite[] ending3_img;
    public Sprite[] ending4_img;
    public Sprite[] hidden1_img;
    public Sprite[] hidden2_img;

    List<int> scene_chk = new List<int>() { 3 };
    List<int> ending_chk = new List<int>() { 12, 15, 18 };

    protected int click_cnt = -1;
    protected bool gamestart = false;


    public void UI_On_Off() //버튼의 첫번째 자식 켜고 끄기 음향 추가 해야함
    {
        GameObject G_obj = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject;

        if (!G_obj.activeSelf)
        {
            G_obj.SetActive(true);
            root_UI = G_obj;
        }
    }

    public void Game_Exit() //게임종료
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif 
    }

    public bool Ending { get { return ending_chk.Contains(SceneManager.GetActiveScene().buildIndex); } }
    public bool Select_chk { get { return scene_chk.Contains(SceneManager.GetActiveScene().buildIndex); } }

    public int Scene_num { get { return SceneManager.GetActiveScene().buildIndex; } }

    //씬이동 변경 고민해봐야할듯
    public void Next_Scene() //현재씬에서 다음 씬으로 넘어감
    {
        StartCoroutine(GameManager_shj.Getinstance.Change_Scene(1));
        //GameManager_shj.Getinstance.Change_Next_Scene(true);
    }

    public void Retry() //현재 씬을 재실행
    {
        StartCoroutine(GameManager_shj.Getinstance.Change_Scene(0));
    }

    public void Return_Lobby() //로비로 돌아가기
    {
        StartCoroutine(GameManager_shj.Getinstance.Change_Scene(1, false));
    }

    public virtual void Return_Scene(int num) //원하는 씬 번호로 이동
    {
        StartCoroutine(GameManager_shj.Getinstance.Change_Scene(num, false));
    }

    public void Call_Save_Scene()
    {
        StartCoroutine(GameManager_shj.Getinstance.Change_Scene(GameManager_shj.Getinstance.Save_data.last_play_scene_num, false));
    }

    
    public void Lastest_Open_UI_Close() //마지막 열린 UI닫기 //제거 예정
    {
        if (root_UI != null) root_UI.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData) //이 스크립트를 가진 오브젝트가 클릭될경우 //제거예정
    {
        Lastest_Open_UI_Close();
    }

    public void BackGround_Set() //랜덤 배경 설정
    {
        if (background_list.Length > 0)
        {
            int cnt = Random.Range(0, background_list.Length);
            background.sprite = background_list[cnt];
        }
    }

    public void Data_Reset() //데이터 리셋 매개변수로 받을까 싶음
    {
        GameManager_shj.Getinstance.Save_data.juksun = 0;
        GameManager_shj.Getinstance.Save_data.last_play_scene_num = 2;
        GameManager_shj.Getinstance.Save_data.hp = 50;

        for (int i = 0; i < GameManager_shj.Getinstance.Save_data.playing.Length; i++)
            GameManager_shj.Getinstance.Save_data.playing[i] = 0.0f;

        Data_Save();
    }

    public  void Data_change(int cnt,int num) //데이터값 변경 매개변수 조정으로 RESET이랑 교체하고싶음
    {
        GameManager_shj.Getinstance.Save_data.juksun = cnt;
        GameManager_shj.Getinstance.Save_data.last_play_scene_num = SceneManager.GetActiveScene().buildIndex + num;
        GameManager_shj.Getinstance.Save_data.hp = player.GetComponent<Player_shj>().hp;
        Data_Save();
    }

    protected IEnumerator Delay_active (float delay_time,GameObject obj) //지연 삭제
    {
        yield return new WaitForSeconds(delay_time);
        obj.SetActive(false);
    }

    public void Data_Save() //데이터 저장
    {
        GameManager_shj.Getinstance.Data_Save();
    }

    public void Skip_btn()
    {
        if (gamestart)
            Next_Scene();
        else if (Ending)
            Return_Lobby();
        else
        {
            story.SetActive(false);
            main.SetActive(true);
        }
    }

    public void Load_Story(string scenario_name) //--
    {
        switch (scenario_name)
        {
            case "opening":
                scenario_img = opening_img;
                break;

            case "ending1":
                scenario_img = ending1_img;
                break;

            case "ending2":
                scenario_img = ending2_img;
                break;

            case "ending3":
                scenario_img = ending3_img;
                break;

            case "ending4":
                scenario_img = ending4_img;
                break;

            case "hidden1":
                scenario_img = hidden1_img;
                break;

            case "hidden2":
                scenario_img = hidden2_img;
                break;
        }
        if(main != null) main.SetActive(false);
        story.SetActive(true);
        click_cnt = -1;
        senario = CSVReader.Read("Scenario/" + scenario_name + "/" + scenario_name + "_scenario");
        next_text();
    }

    public void next_text()
    {
        click_cnt++;

        if (senario.Count / 3 > click_cnt)
        {
            if(audio != null) audio.Play();

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
            Skip_btn();
    }
}
