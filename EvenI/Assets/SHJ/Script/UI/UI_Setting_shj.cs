using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class UI_Setting_shj : MonoBehaviour, IPointerClickHandler
{
    protected AudioSource audio;

    public Sprite[] background_list; //�� ���
    public Image background; //�� ����� ���̰� �� �̹��� ������Ʈ
    public Image story_bg; //���丮�� ���̰� �� �̹��� ������Ʈ
    public RectTransform text_pos;
    public Text story_text;
    public Text btn_text;

    protected Sprite[] scenario_img; //�ó����� �̹��� �迭

    protected List<Dictionary<string, object>> senario; //CSV�� �ҷ��� �ó����� ����
    protected GameObject root_UI = null; //��� �ɵ� ��������

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


    public void UI_On_Off() //��ư�� ù��° �ڽ� �Ѱ� ���� ���� �߰� �ؾ���
    {
        GameObject G_obj = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject;

        if (!G_obj.activeSelf)
        {
            G_obj.SetActive(true);
            root_UI = G_obj;
        }
    }

    public void Game_Exit() //��������
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

    //���̵� ���� ����غ����ҵ�
    public void Next_Scene() //��������� ���� ������ �Ѿ
    {
        StartCoroutine(GameManager_shj.Getinstance.Change_Scene(1));
        //GameManager_shj.Getinstance.Change_Next_Scene(true);
    }

    public void Retry() //���� ���� �����
    {
        StartCoroutine(GameManager_shj.Getinstance.Change_Scene(0));
    }

    public void Return_Lobby() //�κ�� ���ư���
    {
        StartCoroutine(GameManager_shj.Getinstance.Change_Scene(1, false));
    }

    public virtual void Return_Scene(int num) //���ϴ� �� ��ȣ�� �̵�
    {
        StartCoroutine(GameManager_shj.Getinstance.Change_Scene(num, false));
    }

    public void Call_Save_Scene()
    {
        StartCoroutine(GameManager_shj.Getinstance.Change_Scene(GameManager_shj.Getinstance.Save_data.last_play_scene_num, false));
    }

    
    public void Lastest_Open_UI_Close() //������ ���� UI�ݱ� //���� ����
    {
        if (root_UI != null) root_UI.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData) //�� ��ũ��Ʈ�� ���� ������Ʈ�� Ŭ���ɰ�� //���ſ���
    {
        Lastest_Open_UI_Close();
    }

    public void BackGround_Set() //���� ��� ����
    {
        if (background_list.Length > 0)
        {
            int cnt = Random.Range(0, background_list.Length);
            background.sprite = background_list[cnt];
        }
    }

    public void Data_Reset() //������ ���� �Ű������� ������ ����
    {
        GameManager_shj.Getinstance.Save_data.juksun = 0;
        GameManager_shj.Getinstance.Save_data.last_play_scene_num = 2;
        GameManager_shj.Getinstance.Save_data.hp = 50;

        for (int i = 0; i < GameManager_shj.Getinstance.Save_data.playing.Length; i++)
            GameManager_shj.Getinstance.Save_data.playing[i] = 0.0f;

        Data_Save();
    }

    public  void Data_change(int cnt,int num) //�����Ͱ� ���� �Ű����� �������� RESET�̶� ��ü�ϰ����
    {
        GameManager_shj.Getinstance.Save_data.juksun = cnt;
        GameManager_shj.Getinstance.Save_data.last_play_scene_num = SceneManager.GetActiveScene().buildIndex + num;
        GameManager_shj.Getinstance.Save_data.hp = player.GetComponent<Player_shj>().hp;
        Data_Save();
    }

    protected IEnumerator Delay_active (float delay_time,GameObject obj) //���� ����
    {
        yield return new WaitForSeconds(delay_time);
        obj.SetActive(false);
    }

    public void Data_Save() //������ ����
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

                //�����ʿ�
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

                //�г��� �κ��� ����
                if (text.Contains("(�г���)")) text = text.Replace("(�г���)", GameManager_shj.Getinstance.Save_data.nickname);

                if (i == 0) story_text.text = text;
                else story_text.text += "\n" + text;
            }
            btn_text.text = "����";
            if (click_cnt == senario.Count / 3 - 1) //���丮 ��������
            {
                if (gamestart) btn_text.text = "���� ����!";
                else btn_text.text = "���� ����";
            }
        }
        else
            Skip_btn();
    }
}
