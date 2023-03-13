using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMusic : MonoBehaviour
{
    [SerializeField]
    GameObject BackgroundMusic;
    AudioSource backmusic;
    [SerializeField]
    Sprite soundOff;
    [SerializeField]
    Sprite soundOn;

    void Awake()
    {
        backmusic = BackgroundMusic.GetComponent<AudioSource>();
        if (backmusic.isPlaying) return;
        else
        {
            backmusic.Play();
            DontDestroyOnLoad(BackgroundMusic);
        }
    }

    public void BackGroundMusicOffButton()
    {
        if (backmusic.isPlaying)
        {
            backmusic.Pause();
            GetComponent<Image>().sprite = soundOff;
        }
        else
        {
            backmusic.Play();
            GetComponent<Image>().sprite = soundOn;
        }
    }
}
