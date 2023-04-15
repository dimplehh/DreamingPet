using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeSetting : MonoBehaviour
{
    [SerializeField]
    GameObject settingPanel, GuidePanel, StoryPanel, StoryPanel2;
    [SerializeField]
    Image[] images;
    [SerializeField]
    GameObject[] cutSceneSprite;
    [SerializeField]
    GameObject[] cutSceneSprite2;
    [SerializeField]
    GameObject exitButton;
    [SerializeField]
    GameObject sound;
    [SerializeField]
    GameObject sound2;
    public float size; //원하는 사이즈
    public float speed; //커질 때의 속도
    static int index = 0;

    public void Play()
    {
        this.GetComponent<FadeScript>().Fade();
        SceneManager.LoadScene("Loading");
    }

    public void OpenSetting()
    {
        settingPanel.SetActive(true);
        if (sound.GetComponent<SoundManager>().soundOn)
        {
            images[0].gameObject.SetActive(true);
            images[1].gameObject.SetActive(false);
        }
        else
        {
            images[0].gameObject.SetActive(false);
            images[1].gameObject.SetActive(true);
        }
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);
    }

    public void BackOnButton()
    {
        if (!sound.GetComponent<SoundManager>().soundOn)
        {
            sound.GetComponent<AudioSource>().Play();
            sound.GetComponent<SoundManager>().soundOn = true;
            images[0].gameObject.SetActive(true);
            images[1].gameObject.SetActive(false);
        }
    }

    public void BackOffButton()
    {
        if (sound.GetComponent<SoundManager>().soundOn)
        {
            sound.GetComponent<AudioSource>().Pause();
            sound.GetComponent<SoundManager>().soundOn = false;
            images[0].gameObject.SetActive(false);
            images[1].gameObject.SetActive(true);
        }
    }

    public void EffectOnButton()
    {
        if (!sound2.GetComponent<SoundManager2>().soundOn)
        {
            sound2.GetComponent<AudioSource>().Play();
            sound2.GetComponent<SoundManager2>().soundOn = true;
            images[2].gameObject.SetActive(true);
            images[3].gameObject.SetActive(false);
        }
    }

    public void EffectOffButton()
    {
        if (sound2.GetComponent<SoundManager2>().soundOn)
        {
            sound2.GetComponent<AudioSource>().Pause();
            sound2.GetComponent<SoundManager2>().soundOn = false;
            images[2].gameObject.SetActive(false);
            images[3].gameObject.SetActive(true);
        }
    }

    public void GuideOnButton()
    {
        GuidePanel.gameObject.SetActive(true);
    }
    public void GuideOffButton()
    {
        GuidePanel.gameObject.SetActive(false);
    }

    public void StoryButton()
    {
        for (int i = 0; i < cutSceneSprite.Length; i++)
        {
            cutSceneSprite[i].SetActive(false);
        }
        for (int i = 0; i < cutSceneSprite2.Length; i++)
        {
            cutSceneSprite2[i].SetActive(false);
        }
        StoryPanel.gameObject.SetActive(true);
    }

    public void StoryNextButton()
    {
        if (index >= cutSceneSprite.Length)
        {
            StoryPanel.SetActive(false);
            StoryPanel2.SetActive(true);
            index = 0;
            return;
        }
        cutSceneSprite[index].SetActive(true);
        index++;
    }
    public void StoryNextButton2()
    {
        if (index >= cutSceneSprite2.Length)
        {
            StoryPanel2.SetActive(false);
            index = 0;
            sound.GetComponent<SoundManager>().BgSoundPlay(sound.GetComponent<SoundManager>().bgList[0]);
            return;
        }
        cutSceneSprite2[index].SetActive(true);
        index++;
    }
}
