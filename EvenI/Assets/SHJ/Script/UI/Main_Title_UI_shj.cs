using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Main_Title_UI_shj : UI_Setting_shj
{
    public Text nickname_text;

    //���̺� ���� ��������� �����ؾ��ҰͰ���
    //ó�� �����ϰԵǸ� �г��� ����,������ ���丮 �Ƶ��� <- ���� �����Ϳ��� �о�;���
    //�ƾ��� �Ϸ�Ǹ� ���������� �Ѿ .�ƾ��� �Ⱥ��°�� ȭ�� Ŭ���� ���������� ����

    public void Set_NickName() //�г��� ����
    {
        GameManager_shj.Getinstance.nickname = nickname_text.text;
        Lastest_Open_UI_Close();
    }
}
