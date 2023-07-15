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

    public Save_Data_shj Load_Data(Save_Data_shj save_Data)
    {
        try
        {
            stream = new FileStream(Application.dataPath + "/test.json", FileMode.Open);

            data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);
            jsondata = Encoding.UTF8.GetString(data);
            save_Data = JsonConvert.DeserializeObject<Save_Data_shj>(jsondata);
            stream.Close();
        }
        catch
        {
            save_Data = new Save_Data_shj();
            Save_Data(save_Data);
            //stream = new FileStream(Application.dataPath + "/test.json", FileMode.OpenOrCreate);
            //jsondata = JsonConvert.SerializeObject(save_Data);
            //data = Encoding.UTF8.GetBytes(jsondata);
            //stream.Write(data, 0, data.Length);
        }

        return save_Data;
    }

    public void Save_Data(Save_Data_shj save_Data)
    {
        stream = new FileStream(Application.dataPath + "/test.json", FileMode.OpenOrCreate);
        jsondata = JsonUtility.ToJson(save_Data); //jsonconvert를 이용하여 직렬화가 되지않아서 JsonUtility를 사용
        data = Encoding.UTF8.GetBytes(jsondata);

        stream.Write(data, 0, data.Length);
        stream.Close();
    }
}
