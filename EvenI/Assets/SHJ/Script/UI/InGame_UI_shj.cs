using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame_UI_shj : UI_Setting_shj
{
    public void Game_Stop() //게임 정지 버튼
    {
        Time.timeScale = 0.0f; //게임 일시정지
        UI_On_Off();
    }
}
