using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save_Data_shj : MonoBehaviour
{
    public string nickname; //�Ǵ��̸�
    public int juksun; //�׼�����
    public int last_play_scene_num; //������ �÷����� �� ��ȣ
    public int hp; //�Ǵ� hp
    public bool[] ending; //���� ����
    public float bgm_vol; //����� ����
    public float eff_vol; //ȿ���� ����

    public Save_Data_shj() //������
    {
        nickname = "";
        juksun = 0;
        last_play_scene_num = 0;
        hp = 10;
        ending = new bool[6];
        bgm_vol = 0.5f;
        eff_vol = 0.5f;

        for (int i = 0; i < 6; i++) ending[i] = false;
    }
}
