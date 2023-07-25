using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save_Data_shj : MonoBehaviour
{
    public string nickname; //판다이름
    public int juksun; //죽순개수
    public int last_play_scene_num; //마지막 플레이한 씬 번호
    public int hp; //판다 hp
    public bool[] ending; //엔딩 수집
    public float bgm_vol; //배경음 볼륨
    public float eff_vol; //효과음 볼륨

    public Save_Data_shj() //생성자
    {
        nickname = "";
        juksun = 0;
        last_play_scene_num = 0;
        hp = 50;
        ending = new bool[7];
        bgm_vol = 0.5f;
        eff_vol = 0.5f;

        for (int i = 0; i < ending.Length; i++) ending[i] = false;
    }
}
