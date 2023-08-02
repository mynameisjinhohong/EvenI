using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TImer_shj : MonoBehaviour
{
    public Text text;
    public Player_shj player;
    float timer = 0.0f;
    // Update is called once per frame
    void Update()
    {
        if (player.enabled)
        {
            timer += Time.deltaTime;
            text.text = timer.ToString("F1");
        }

    }
}
