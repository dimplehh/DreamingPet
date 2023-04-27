using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager2 : MonoBehaviour
{
    private static SoundManager2 _instance;

    public AudioSource effectSound;
    public bool soundOn;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            soundOn = true;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void EffectSoundPlay(AudioClip clip)
    {
        if(soundOn)
        {
            effectSound.clip = clip;
            effectSound.volume = 0.1f;
            effectSound.PlayOneShot(effectSound.clip);
        }
    }
    public void EffectSoundStop(AudioClip clip)
    {
        if (soundOn)
        {
            effectSound.clip = clip;
            effectSound.volume = 0.1f;
            effectSound.Stop();
        }
    }
}
