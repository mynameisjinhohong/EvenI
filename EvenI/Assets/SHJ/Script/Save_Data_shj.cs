using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Save_Data_shj : MonoBehaviour
{
    public string nickname; //�Ǵ��̸�
    public int juksun; //�׼�����
    public int carrot; //��ٰ���
    public int ancientRock; //���񼮰���
    public int last_play_scene_num; //������ �÷����� �� ��ȣ
    public int max_hp; //�Ǵ� �ִ�hp
    public int hp; //�Ǵ� hp
    public float[] playing; //�����
    public bool[] ending; //���� ����
    public float bgm_vol; //����� ����
    public float eff_vol; //ȿ���� ����
    public string lastjoin; //������ ����
    public string lastheal; //������ȸ��
    public int healcnt; //���� ȸ��Ƚ��

    public Save_Data_shj() //������
    {
        nickname = "";
        juksun = 0;
        carrot = 0;
        ancientRock = 0;
        last_play_scene_num = 0;
        max_hp = 30;
        hp = 30;
        ending = new bool[7];
        playing = new float[6];
        bgm_vol = 0.5f;
        eff_vol = 0.5f;
        lastjoin = DateTime.Now.ToString("yyyy-MM-dd");
        lastheal = DateTime.Now.ToString("HH-mm-ss");
        healcnt = 0;

        for (int i = 0; i < ending.Length; i++) ending[i] = false;
        for (int i = 0; i < playing.Length; i++) playing[i] = 0.0f;

    }
}
