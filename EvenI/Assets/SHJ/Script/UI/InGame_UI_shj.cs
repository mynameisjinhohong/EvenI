using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGame_UI_shj : UI_Setting_shj
{
    public TextMeshProUGUI count_text;

    public GameObject HP;
    public Transform HP_list;

    int count = 0;

    public int Count { get { return count; } set { count = value; } }

    private void Start()
    {
        if(HP_list.childCount == 0)
        {
            for (int i = 0; i < GameObject.Find("Player").GetComponent<Player_shj>().hp; i++)
            {
                GameObject obj = Instantiate(HP);
                obj.transform.parent = HP_list;
            }
        }
    }

    private void Update()
    {
        count_text.text = count.ToString();
        Debug.Log(count.ToString());
    }

    public void Game_Stop() //게임 정지 버튼
    {
        Time.timeScale = 0.0f; //게임 일시정지
        UI_On_Off();
    }
}
