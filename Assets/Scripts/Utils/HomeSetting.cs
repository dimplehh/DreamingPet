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
    Image[] images;
    GameObject sound;
    public float size; //���ϴ� ������
    public float speed; //Ŀ�� ���� �ӵ�

    private float time = 0;
    private Vector2 originScale; //���� ũ��
    private void Awake()
    {
        originScale = transform.localScale; //���� ũ�� ����
    }
    private void Start()
    {
        sound = GameObject.Find("soundManager");
    }
    public void Play()
    {
        this.GetComponent<FadeScript>().Fade();
        StartCoroutine(Up());
    }
    IEnumerator Up()
    {
        while (transform.localScale.x < size)
        {
            transform.localScale = originScale * (1f + time * speed);
            time += Time.deltaTime;

            if (transform.localScale.x >= size)
            {
                time = 0;
                break;
            }
            yield return null;
        }
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
}
