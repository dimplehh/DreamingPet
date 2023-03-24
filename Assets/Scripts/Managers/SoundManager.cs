using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public AudioSource bgSound;
    public AudioClip[] bgList;
    public bool soundOn;
    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            soundOn = true;
            Debug.Log("A");
        }
        else if(_instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < bgList.Length; i++)
        {
            if(arg0.name == bgList[i].name)
            {
                if (soundOn)
                    BgSoundPlay(bgList[i]);
                else
                    BgSoundStop(bgList[i]);
            }
        }
    }

    public void BgSoundPlay(AudioClip clip)
    {
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = 0.1f;
        bgSound.Play();
    }

    public void BgSoundStop(AudioClip clip)
    {
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = 0.1f;
        bgSound.Stop();
    }
}
