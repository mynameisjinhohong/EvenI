using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Main_Title_UI_shj : UI_Setting_shj
{
    public Text nickname_text;
    public Text story_text;
    public Text btn_text;

    int click_cnt = -1;

    //���̺� ���� ��������� �����ؾ��ҰͰ���
    //ó�� �����ϰԵǸ� �г��� ����,������ ���丮 �Ƶ��� <- ���� �����Ϳ��� �о�;���
    //�ƾ��� �Ϸ�Ǹ� ���������� �Ѿ .�ƾ��� �Ⱥ��°�� ȭ�� Ŭ���� ���������� ����

    private void Start()
    {
        senario = CSVReader.Read("Scenario/opening_scenario");
        next_text();
    }

    public void Set_NickName() //�г��� ����
    {
        GameManager_shj.Getinstance.nickname = nickname_text.text;
        Lastest_Open_UI_Close();
    }

    public void next_text()
    {
        click_cnt++;

        if (senario.Count / 3 > click_cnt)
        {
            for (int i = 0; i < 3; i++)
            {
                if(i == 0)
                    story_text.text = senario[i + 3 * click_cnt]["text"].ToString();
                else
                    story_text.text += "\n" + senario[i + 3 * click_cnt]["text"].ToString();
            }
        }
        else
        {
            Next_Scene();
        }
    }
}
