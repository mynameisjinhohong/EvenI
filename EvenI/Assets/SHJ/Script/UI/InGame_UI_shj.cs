using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame_UI_shj : UI_Setting_shj
{
    public Text count_text;

    int count = 0;

    private void Update()
    {
        count_text.text = count.ToString();
    }

    public void Game_Stop() //게임 정지 버튼
    {
        Time.timeScale = 0.0f; //게임 일시정지
        UI_On_Off();
    }
}
