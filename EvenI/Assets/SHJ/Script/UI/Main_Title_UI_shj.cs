using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Main_Title_UI_shj : MonoBehaviour
{
    public Text nickname_text;

    //���̺� ���� ��������� �����ؾ��ҰͰ���
    //ó�� �����ϰԵǸ� �г��� ����,������ ���丮 �Ƶ��� <- ���� �����Ϳ��� �о�;���

    public void Set_NickName()
    {
        GameManager_shj.Getinstance.nickname = nickname_text.text;
    }
}
