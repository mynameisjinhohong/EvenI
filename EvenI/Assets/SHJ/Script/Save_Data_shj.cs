using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Save_Data_shj : MonoBehaviour
{
    public string nickname; //판다이름
    public int juksun; //죽순개수
    public int carrot; //당근개수
    public int ancientRock; //고대비석개수
    public int last_play_scene_num; //마지막 플레이한 씬 번호
    public int max_hp; //판다 최대hp
    public int hp; //판다 hp
    public float[] playing; //진행률
    public bool[] ending; //엔딩 수집
    public float bgm_vol; //배경음 볼륨
    public float eff_vol; //효과음 볼륨
    public string lastjoin; //마지막 접속
    public string lastheal; //마지막회복
    public int healcnt; //일일 회복횟수

    public Save_Data_shj() //생성자
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
