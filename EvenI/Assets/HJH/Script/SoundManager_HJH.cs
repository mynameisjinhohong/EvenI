using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager_HJH : MonoBehaviour
{
    AudioSource audio;
    public AudioClip[] clips;
    public GameObject soundObject;
    public int idx = 0;
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
            if (i == transform.childCount)
            {
                if (transform.childCount < 30)
                {
                    MakeBaby((int)Sound.JuksunSound);
                    break;
                }
                else
                {
                    gameObject.transform.GetChild(idx).GetComponent<AudioSource>().clip = clips[(int)Sound.JuksunSound];
                    gameObject.transform.GetChild(idx).GetComponent<AudioSource>().Play();
                    idx++;
                    if(idx > 29)
                    {
                        idx = 0;
                    }
                }
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

                if (transform.childCount < 30)
                {
                    MakeBaby((int)Sound.RockBreakSound);
                    break;
                }
                else
                {
                    gameObject.transform.GetChild(idx).GetComponent<AudioSource>().clip = clips[(int)Sound.RockBreakSound];
                    gameObject.transform.GetChild(idx).GetComponent<AudioSource>().Play();
                    idx++;
                    if (idx > 29)
                    {
                        idx = 0;
                    }
                }
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

                if (transform.childCount < 30)
                {
                    MakeBaby((int)Sound.ObjectBreakSound);
                    break;
                }
                else
                {
                    gameObject.transform.GetChild(idx).GetComponent<AudioSource>().clip = clips[(int)Sound.ObjectBreakSound];
                    gameObject.transform.GetChild(idx).GetComponent<AudioSource>().Play();
                    idx++;
                    if (idx > 29)
                    {
                        idx = 0;
                    }
                }
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
        if(transform.childCount < 30)
        {
            GameObject obj = Instantiate(soundObject, transform);
            obj.GetComponent<AudioSource>().clip = clips[target];
            obj.GetComponent<AudioSource>().volume = 0.2f;
            obj.GetComponent<AudioSource>().Play();
        }

    }
}
