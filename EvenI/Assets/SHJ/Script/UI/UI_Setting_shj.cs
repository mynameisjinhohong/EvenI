using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UI_Setting_shj : MonoBehaviour ,IPointerClickHandler
{
    protected List<Dictionary<string, object>> senario;
    protected GameObject root_UI = null;
    public void UI_On_Off() //버튼의 첫번째 자식 켜고 끄기
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

    public void Next_Scene() //현재씬에서 다음 씬으로 넘어감
    {
        GameManager_shj.Getinstance.Change_Next_Scene(true);
    }

    public void Return_Lobby() //로비로 돌아가기
    {
        GameManager_shj.Getinstance.Change_Next_Scene(false);
    }

    public void Lastest_Open_UI_Close() //마지막 열린 UI닫기
    {
        if (root_UI != null) root_UI.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData) //이 스크립트를 가진 오브젝트가 클릭될경우
    {
        Lastest_Open_UI_Close();
    }
}
