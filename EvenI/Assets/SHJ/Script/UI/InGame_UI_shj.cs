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

    public void Game_Stop() //���� ���� ��ư
    {
        Time.timeScale = 0.0f; //���� �Ͻ�����
        UI_On_Off();
    }
}
