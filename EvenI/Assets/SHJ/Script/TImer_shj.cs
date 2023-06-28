using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TImer_shj : MonoBehaviour
{
    public Text text;

    float timer = 0.0f;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        text.text = timer.ToString("F1");
    }
}
