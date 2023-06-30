using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_shj : MonoBehaviour
{
    private static GameManager_shj instance = null;
    public static GameManager_shj Getinstance { get { return instance; } }
    public string nickname;

    public void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    public void Change_Next_Scene(bool next)
    {
        if(next)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene(1);
    }
}
