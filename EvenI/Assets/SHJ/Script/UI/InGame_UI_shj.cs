using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame_UI_shj : UI_Setting_shj
{
    public void Game_Stop() //���� ���� ��ư
    {
        Time.timeScale = 0.0f; //���� �Ͻ�����
        UI_On_Off();
    }
}
