using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InGame_UI_shj : UI_Setting_shj
{
    public TextMeshProUGUI count_text;
    public GameObject Hp;
    public Transform Hp_list;
    public Player_shj player;

    int count = 0;
    public int Count { get {  return count; } set { count = value; } }

    private void Start()
    {
        if(Hp_list.childCount == 0)
        {
            for (int i = 0; i < player.hp; i++)
            {
                GameObject obj = Instantiate(Hp);
                obj.transform.parent = Hp_list;
            }
        }
    }

    private void Update()
    {
        count_text.text = count.ToString();
        Debug.Log(Time.timeScale);
    }

    public void Game_Stop() //게임 정지 버튼
    {
        Time.timeScale = 0.0f; //게임 일시정지
        UI_On_Off();
    }

    public void Game_Continue()
    {
        Time.timeScale = 1.0f;
        Lastest_Open_UI_Close();
    }
}
