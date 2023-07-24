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
    public SoundManager sound;
    public SoundManager2 sound2;
    [SerializeField]
    AudioClip[] bgList;
    public float size; //원하는 사이즈
    public float speed; //커질 때의 속도
    static int index = 0;

    public void Awake()
    {
        sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>();
        sound2 = GameObject.FindGameObjectWithTag("Sound2").GetComponent<SoundManager2>();
    }

    public void Play()
    {
        SceneManager.LoadScene("Loading");
        sound2.EffectSoundPlay(bgList[0]);
    }

    public void OpenSetting()
    {
        sound2.EffectSoundPlay(bgList[1]);
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
        if (sound2.GetComponent<SoundManager2>().soundOn)
        {
            images[2].gameObject.SetActive(true);
            images[3].gameObject.SetActive(false);
        }
        else
        {
            images[2].gameObject.SetActive(false);
            images[3].gameObject.SetActive(true);
        }
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false);
        sound2.EffectSoundPlay(bgList[2]);
    }

    public void BackOnButton()
    {
        sound2.EffectSoundPlay(bgList[3]);
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
        sound2.EffectSoundPlay(bgList[3]);
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
        sound2.EffectSoundPlay(bgList[3]);
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
        sound2.EffectSoundPlay(bgList[3]);
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
        sound2.EffectSoundPlay(bgList[1]);
    }
    public void GuideOffButton()
    {
        GuidePanel.gameObject.SetActive(false);
        sound2.EffectSoundPlay(bgList[2]);
    }

    public void StoryButton()
    {
        sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>();
        sound2 = GameObject.FindGameObjectWithTag("Sound2").GetComponent<SoundManager2>();
        sound2.EffectSoundPlay(bgList[0]);
        if (sound.soundOn) sound.BgSoundPlay(bgList[4]);
        for (int i = 0; i < cutSceneSprite.Length; i++)
        {
            cutSceneSprite[i].SetActive(false);
        }
        for (int i = 0; i < cutSceneSprite2.Length; i++)
        {
            cutSceneSprite2[i].SetActive(false);
        }
        StoryPanel.gameObject.SetActive(true);
        StartCoroutine(StoryNextButton());
    }

    public IEnumerator StoryNextButton()
    {
        for (int index = 0;index <cutSceneSprite.Length; index++)
        {
            cutSceneSprite[index].SetActive(true);
            if(index != 0)
                sound2.EffectSoundPlay(bgList[5]);
            yield return new WaitForSeconds(2.0f);
        }
        StoryPanel.SetActive(false);
        StoryPanel2.SetActive(true);
        index = 0;
        yield return StartCoroutine(StoryNextButton2());
    }
    IEnumerator StoryNextButton2()
    {
        for (index = 0; index< cutSceneSprite2.Length; index++)
        {
            cutSceneSprite2[index].SetActive(true);
            if (index == cutSceneSprite2.Length - 1)
                sound2.EffectSoundPlay(bgList[6]);
            else
                sound2.EffectSoundPlay(bgList[5]);
            yield return new WaitForSeconds(2.0f);
        }
        StoryPanel2.SetActive(false);
        index = 0;
        sound.GetComponent<SoundManager>().BgSoundPlay(sound.GetComponent<SoundManager>().bgList[0]);
        yield return null;
    }

    public void ExitCutScene()
    {
        StopAllCoroutines();
        for (int i = 0; i < cutSceneSprite.Length; i++)
        {
            cutSceneSprite[i].SetActive(false);
        }
        for (int i = 0; i < cutSceneSprite2.Length; i++)
        {
            cutSceneSprite2[i].SetActive(false);
        }
        StoryPanel.SetActive(false);
        StoryPanel2.SetActive(false);
        sound2.EffectSoundPlay(bgList[2]);
        sound.GetComponent<SoundManager>().BgSoundPlay(sound.GetComponent<SoundManager>().bgList[0]);
        return;
    }
}
