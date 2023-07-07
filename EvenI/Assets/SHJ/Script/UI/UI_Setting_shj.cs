using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UI_Setting_shj : MonoBehaviour ,IPointerClickHandler
{
    protected List<Dictionary<string, object>> senario;
    protected GameObject root_UI = null;
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
        GameManager_shj.Getinstance.Change_Next_Scene(true);
    }

    public void Return_Lobby() //�κ�� ���ư���
    {
        GameManager_shj.Getinstance.Change_Next_Scene(false);
    }

    public void Lastest_Open_UI_Close() //������ ���� UI�ݱ�
    {
        if (root_UI != null) root_UI.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData) //�� ��ũ��Ʈ�� ���� ������Ʈ�� Ŭ���ɰ��
    {
        Lastest_Open_UI_Close();
    }
}
