using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Main_Title_UI_shj : UI_Setting_shj
{
    public Text nickname_text;

    //세이브 파일 만들어지면 제작해야할것같음
    //처음 시작하게되면 닉네임 설정,오프닝 스토리 컷등장 <- 저장 데이터에서 읽어와야함
    //컷씬이 완료되면 다음씬으로 넘어감 .컷씬을 안보는경우 화면 클릭시 다음씬으로 변경

    public void Set_NickName() //닉네임 설정
    {
        GameManager_shj.Getinstance.nickname = nickname_text.text;
        Lastest_Open_UI_Close();
    }
}
