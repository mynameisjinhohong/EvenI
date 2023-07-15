using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_shj : MonoBehaviour
{
    private static GameManager_shj instance = null;

    public static GameManager_shj Getinstance { get { return instance; } }
    public string nickname;

    bool vibration = true;

    Save_Data_shj save_Data;
    DataManager_shj dataManager;

    [Range(0,10f)]
    public float delay;

    public void Awake()
    {
        instance = this; 
        DontDestroyOnLoad(this);

        DataManager_shj dataManager = new DataManager_shj();

        save_Data = dataManager.Load_Data(save_Data);
        Debug.Log( save_Data.nickname);
    }

    //public void Change_Next_Scene(bool next)
    //{
    //    if(next)
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //    else
    //        SceneManager.LoadScene(1);
    //}

    public IEnumerator Change_Scene(int num, bool next = true)
    {
        yield return new WaitForSecondsRealtime(delay);
        if(next)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + num);
        else
            SceneManager.LoadScene(num);

        Time.timeScale = 1.0f;
    }

    //public void Data_Save()
    //{
    //    dataManager.Save_Data(save_Data);
    //}

    public bool Set_vibration { set { vibration = !vibration; } }
    public Save_Data_shj Set_save_data { get { return save_Data; } set { save_Data = value; } }

}
