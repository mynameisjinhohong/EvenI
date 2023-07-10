using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Main_Title_UI_shj : UI_Setting_shj
{
    public Text nickname_text;
    public Text story_text;
    public Text btn_text;

    int click_cnt = -1;

    AudioSource audio;
    //���̺� ���� ��������� �����ؾ��ҰͰ���
    //ó�� �����ϰԵǸ� �г��� ����,������ ���丮 �Ƶ��� <- ���� �����Ϳ��� �о�;���
    //�ƾ��� �Ϸ�Ǹ� ���������� �Ѿ .�ƾ��� �Ⱥ��°�� ȭ�� Ŭ���� ���������� ����

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        senario = CSVReader.Read("Scenario/opening_scenario");
        next_text();
    }

    //public void Next_Scene() //��������� ���� ������ �Ѿ
    //{
    //    audio.Play();
    //    Invoke("NextScene", 0.1f);
    //}

    //void NextScene()
    //{
    //    GameManager_shj.Getinstance.Change_Next_Scene(true);
    //}

    public void Set_NickName() //�г��� ����
    {
        GameManager_shj.Getinstance.nickname = nickname_text.text;
    }

    public void next_text()
    {
        click_cnt++;

        if (senario.Count / 3 > click_cnt)
        {
            audio.Play();

            for (int i = 0; i < 3; i++)
            {
                string text = senario[i + 3 * click_cnt]["text"].ToString();

                //�г��� �κ��� ����
                if (text.Contains("(�г���)")) text = text.Replace("(�г���)", GameManager_shj.Getinstance.nickname);

                if (i == 0) story_text.text = text; 
                else story_text.text += "\n" + text;
            }

            if (click_cnt == senario.Count / 3 - 1) btn_text.text = "�κ�� �̵�";
        }
        else
            Next_Scene();
    }
}
