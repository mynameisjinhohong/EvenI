using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

//class SaveData
//{

//}

public class DataManager_shj : MonoBehaviour
{
    string jsondata;
    byte[] data;
    FileStream stream;

    public bool Data_Check()
    {
        return File.Exists(Application.persistentDataPath + "/test.json");
        //if (File.Exists(Application.persistentDataPath + "/test.json")) //세이브 데이터 있음
        //    Load_Data(GameManager_shj.Getinstance.Save_data);
        //else //세이브 데이터 없음
        //{
        //    Save_Data(GameManager_shj.Getinstance.Save_data);
        //}

        //while(!File.Exists(Application.persistentDataPath + "/test.json"))
        //{

        //}

        //GameManager_shj.Getinstance.
    }

    public void Load_Data()
    {
        
        stream = new FileStream(Application.persistentDataPath + "/test.json", FileMode.Open);
        data = new byte[stream.Length];
        stream.Read(data,0,data.Length);
        jsondata = Encoding.UTF8.GetString(data);
        GameManager_shj.Getinstance.Save_data = JsonConvert.DeserializeObject<Save_Data_shj>(jsondata);

        //save_Data = JsonConvert.DeserializeObject<Save_Data_shj>(jsondata);

        //try
        //{
        //    stream = new FileStream(Application.persistentDataPath + "/test.json", FileMode.Open);
        //    data = new byte[stream.Length];
        //    stream.Read(data, 0, data.Length);
        //    jsondata = Encoding.UTF8.GetString(data);
        //    //save_Data = JsonUtility.FromJson<Save_Data_shj>(jsondata);
        //    save_Data = JsonConvert.DeserializeObject<Save_Data_shj>(jsondata);
        //    Data_Copy(GameManager_shj.Getinstance.Save_data,save_Data);

        //    stream.Close();
        //}
        //catch
        //{
        //    Save_Data(save_Data);
        //}
        //return save_Data;
    }

    public void Save_Data(Save_Data_shj save_Data)
    {
        if(File.Exists(Application.persistentDataPath + "/test.json"))
        {
            StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/test.json", false);
            sw.Close();
        }

        stream = new FileStream(Application.persistentDataPath + "/test.json", FileMode.OpenOrCreate);

        jsondata = JsonUtility.ToJson(save_Data); //jsonconvert를 이용하여 직렬화가 되지않아서 JsonUtility를 사용
        data = Encoding.UTF8.GetBytes(jsondata);
       
        stream.Write(data, 0, data.Length);
        stream.Close();
    }

    //public void Data_Copy(Save_Data_shj obj1, Save_Data_shj obj2)
    //{
    //    obj1.nickname = obj2.nickname;
    //    obj1.juksun = obj2.juksun;
    //    obj1.last_play_scene_num = obj2.last_play_scene_num;
    //    obj1.hp = obj2.hp;
    //    obj1.ending = obj2.ending;
    //    obj1.bgm_vol = obj2.bgm_vol;
    //    obj1.eff_vol = obj2.eff_vol;
    //}
}
