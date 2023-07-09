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

    [Range(0,10f)]
    public float delay;

    public void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
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
        yield return new WaitForSeconds(delay);

        if(next)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + num);
        else
            SceneManager.LoadScene(num);
    }

    public bool Set_vibration { set { vibration = !vibration; } }
}
