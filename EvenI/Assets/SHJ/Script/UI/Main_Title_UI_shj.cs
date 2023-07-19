using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Main_Title_UI_shj : UI_Setting_shj
{
    public GameObject info;

    private void Start()
    {
        BackGround_Set();
    }

    public void Data_Check()
    {
        if(GameManager_shj.Getinstance.Data_Manager.Data_Check())
            GameManager_shj.Getinstance.Data_Manager.Load_Data();

        else
        {
            GameManager_shj.Getinstance.Data_Manager.Save_Data(new Save_Data_shj()); //���ο� ������ ����

            while(!GameManager_shj.Getinstance.Data_Manager.Data_Check()) //�����Ͱ� ������������� �ݺ�
                info.SetActive(true);

        }
        Next_Scene();
    }
}