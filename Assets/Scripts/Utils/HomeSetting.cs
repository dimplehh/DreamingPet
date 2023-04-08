using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeSetting : MonoBehaviour
{
    [SerializeField]
    GameObject settingPanel, GuidePanel, StoryPanel;
    [SerializeField]
    Image[] images;
    [SerializeField]
    Sprite[] cutSceneSprite;
    [SerializeField]
    GameObject exitButton;
    GameObject sound;
    GameObject sound2;
    public float size; //원하는 사이즈
    public float speed; //커질 때의 속도

    private float time = 0;
    private Vector2 originScale; //원래 크기
    private void Awake()
    {
        originScale = transform.localScale; //원래 크기 저장
    }
    private void Start()
    {
        sound = GameObject.Find("soundManager"); //find 함수 차후 수정
        sound2 = GameObject.Find("soundManager2");
    }
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
        StoryPanel.gameObject.SetActive(true);
    }

    public void StoryNextButton()
    {
        if(StoryPanel.GetComponent<Image>().sprite == cutSceneSprite[1])
        {
            StoryPanel.GetComponent<Image>().sprite = cutSceneSprite[0];
            StoryPanel.gameObject.SetActive(false);
        }
        else
        {
            StoryPanel.GetComponent<Image>().sprite = cutSceneSprite[1];
        }
    }
}
