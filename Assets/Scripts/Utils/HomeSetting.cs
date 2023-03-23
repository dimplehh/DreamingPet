using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeSetting : MonoBehaviour
{
    [SerializeField]
    GameObject settingPanel;
    [SerializeField]
    GameObject BackgroundMusic;
    AudioSource backmusic;
    [SerializeField]
    Image[] images;

    void Start()
    {
        backmusic = BackgroundMusic.GetComponent<AudioSource>();
        DontDestroyOnLoad(BackgroundMusic);
    }
    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }


    public void OpenSetting()
    {
        settingPanel.SetActive(true);
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);
    }

    public void BackOnButton()
    {
        if (!backmusic.isPlaying)
        {
            backmusic.Play();
            images[0].gameObject.SetActive(true);
            images[1].gameObject.SetActive(false);
        }
    }

    public void BackOffButton()
    {
        if (backmusic.isPlaying)
        {
            backmusic.Pause();
            images[0].gameObject.SetActive(false);
            images[1].gameObject.SetActive(true);
        }
    }
}
