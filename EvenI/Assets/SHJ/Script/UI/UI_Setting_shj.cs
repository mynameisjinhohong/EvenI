using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class UI_Setting_shj : MonoBehaviour, IPointerClickHandler
{
    public Sprite[] background_list;
    public Image background;

    protected Sprite[] scenario_img;

    public Image story_bg;

    protected List<Dictionary<string, object>> senario;
    protected GameObject root_UI = null;

    public GameObject player;

    public void UI_On_Off() //��ư�� ù��° �ڽ� �Ѱ� ����
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

    public void Lastest_Open_UI_Close() //������ ���� UI�ݱ�
    {
        if (root_UI != null) root_UI.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData) //�� ��ũ��Ʈ�� ���� ������Ʈ�� Ŭ���ɰ��
    {
        Lastest_Open_UI_Close();
    }

    public void BackGround_Set()
    {
        if (background_list.Length > 0)
        {
            int cnt = Random.Range(0, background_list.Length);
            background.sprite = background_list[cnt];
        }
    }

    public void Data_Reset()
    {
        GameManager_shj.Getinstance.Save_data.juksun = 0;
        GameManager_shj.Getinstance.Save_data.last_play_scene_num = 2;
        GameManager_shj.Getinstance.Save_data.hp = 10;
        Data_Save();
    }

    public  void Data_change(int cnt,int num)
    {
        GameManager_shj.Getinstance.Save_data.juksun = cnt;
        GameManager_shj.Getinstance.Save_data.last_play_scene_num = SceneManager.GetActiveScene().buildIndex + num;
        GameManager_shj.Getinstance.Save_data.hp = player.GetComponent<Player_shj>().hp;
        Data_Save();
    }

    protected IEnumerator Delay_active (float delay_time,GameObject obj)
    {
        yield return new WaitForSeconds(delay_time);
        obj.SetActive(false);
    }

    public void Data_Save()
    {
        GameManager_shj.Getinstance.Data_Save();
    }
}
