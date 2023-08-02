using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class GameManager_shj : MonoBehaviour
{
    private static GameManager_shj instance = null;
    AudioSource audio;
    public static GameManager_shj Getinstance { get { return instance; } }

    bool vibration = true;

    Save_Data_shj save_Data;
    DataManager_shj dataManager;
    Notification_Manager_shj notification;

    public AudioMixer mixer;

    [Range(0,10f)]
    public float delay;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this; 
            DontDestroyOnLoad(this.gameObject);
            audio = GetComponent<AudioSource>();

            dataManager = GetComponent<DataManager_shj>();
            notification = GetComponent<Notification_Manager_shj>();
            save_Data = gameObject.AddComponent<Save_Data_shj>();
            //dataManager.Load_Data(new Save_Data_shj());
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        //if (dataManager.Data_Check())
        //{
        //    dataManager.Load_Data();
        //    Volume_Set("bgm", save_Data.bgm_vol);
        //    Volume_Set("effect", save_Data.eff_vol);
        //}
    }

    //public void Change_Next_Scene(bool next)
    //{
    //    if(next)
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //    else
    //        SceneManager.LoadScene(1);
    //}

    //public void Change_Next_Scene(bool next)
    //{
    //    if (next)
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

    }
    private void Update()
    {
        if (audio != null)
        {
            if (audio.isPlaying && !SceneManager.GetActiveScene().name.Contains("Main"))
                audio.Stop();

            else if (!audio.isPlaying && SceneManager.GetActiveScene().name.Contains("Main"))
                audio.Play();
        }
    }

    public void Volume_Set(string type,float vol)
    {
        if (vol == -20f) mixer.SetFloat(type, -80f);
        else mixer.SetFloat(type, vol);

        switch (type)
        {
            case "BGM":
                mixer.GetFloat(type, out save_Data.bgm_vol);
                break;
            case "Effect":
                mixer.GetFloat(type, out save_Data.eff_vol);
                break;
        }
    }

    //public void BGM_vol_set(float vol)
    //{
    //    if (vol == -20f) mixer.SetFloat("BGM", -80f);
    //    else mixer.SetFloat("BGM",vol);

    //    mixer.GetFloat("BGM",out save_Data.bgm_vol);
    //}

    //public void Effect_vol_set(float vol)
    //{
    //    if (vol == -20f) mixer.SetFloat("Effect", -80f);
    //    else mixer.SetFloat("Effect", vol);

    //    mixer.GetFloat("Effect", out save_Data.eff_vol);
    //}

    public void Data_Save() { dataManager.Save_Data(save_Data); } 
    //public bool Set_vibration { set { vibration = !vibration; } }
    public Save_Data_shj Save_data { get { return save_Data; } set { save_Data = value; } }
    public DataManager_shj Data_Manager { get { return dataManager; } }
    public Notification_Manager_shj Noti { get { return notification; } }
}
