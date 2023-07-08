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
        audio.clip = clips[(int)Sound.JuksunSound];
        audio.Play();
    }
    public void RockBreakSoundPlay()
    {
        audio.clip = clips[(int)Sound.RockBreakSound];
        audio.Play();
    }
    public void ObjectBreakSoundPlay()
    {
        audio.clip = clips[((int)Sound.ObjectBreakSound)];
        audio.Play();
    }
}
