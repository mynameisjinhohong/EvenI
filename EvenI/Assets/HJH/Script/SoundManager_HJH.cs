using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_HJH : MonoBehaviour
{
    AudioSource audio;
    public AudioClip[] clips;
    enum Sound
    {
        JuksunSound,
        RockBreakSound,
        ObjectBreakSound,
        LifeZeroSound,
    }
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void JuksunSoundPlay()
    {
        for(int i = 0; i<transform.childCount+1; i++)
        {
            GameObject target;
            if(i == 0)
            {
                target = gameObject;
            }
            else
            {
                target = gameObject.transform.GetChild(i - 1).gameObject;
            }
            audio = target.GetComponent<AudioSource>();
            if (!audio.isPlaying)
            {
                audio.clip = clips[(int)Sound.JuksunSound];
                audio.Play();
                break;
            }
            if(i == transform.childCount)
            {
                MakeBaby((int)Sound.JuksunSound);
                break;
            }
        }

    }
    public void RockBreakSoundPlay()
    {
        for (int i = 0; i < transform.childCount + 1; i++)
        {
            GameObject target;
            if (i == 0)
            {
                target = gameObject;
            }
            else
            {
                target = gameObject.transform.GetChild(i - 1).gameObject;
            }
            audio = target.GetComponent<AudioSource>();
            if (!audio.isPlaying)
            {
                audio.clip = clips[(int)Sound.RockBreakSound];
                audio.Play();
                break;
            }
            if (i == transform.childCount)
            {
                MakeBaby((int)Sound.RockBreakSound);
                break;
            }
        }
    }
    public void ObjectBreakSoundPlay()
    {
        for (int i = 0; i < transform.childCount + 1; i++)
        {
            GameObject target;
            if (i == 0)
            {
                target = gameObject;
            }
            else
            {
                target = gameObject.transform.GetChild(i - 1).gameObject;
            }
            audio = target.GetComponent<AudioSource>();
            if (!audio.isPlaying)
            {
                audio.clip = clips[(int)Sound.ObjectBreakSound];
                audio.Play();
                break;
            }
            if (i == transform.childCount)
            {
                MakeBaby((int)Sound.ObjectBreakSound);
                break;
            }
        }
    }
    public void LifeZeroSoundPlay()
    {
        audio.clip = clips[(int)Sound.LifeZeroSound];
        audio.Play();
    }

    void MakeBaby(int target)
    {
        GameObject obj = new GameObject("SoundManagerChild" + gameObject.transform.childCount);
        obj.AddComponent<AudioSource>();
        obj.transform.SetParent(transform);
        obj.GetComponent<AudioSource>().clip = clips[target];
        obj.GetComponent<AudioSource>().Play();
    }
}
