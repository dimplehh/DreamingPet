using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    public GameObject panel,guidepanel, feverPanel;
    [SerializeField]
    LevelManager lv;
    [SerializeField]
    AudioClip[] bgList;
    SoundManager sound;
    SoundManager2 sound2;

    public void Start()
    {
        sound = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>();
        sound2 = GameObject.FindGameObjectWithTag("Sound2").GetComponent<SoundManager2>();
    }

    public void ClickSetting()
    {
        gamePause(0f);
        panel.SetActive(true);
        sound2.EffectSoundPlay(bgList[0]);
    }
    public void ClickClose()
    {
        gamePause(1f);
        panel.SetActive(false);
        sound2.EffectSoundPlay(bgList[0]);
    }
    public void gamePause(float timescale)
    {
        if (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().feverState && timescale != 0f)
            timescale = 2.5f;
        Time.timeScale=timescale;
    }

    public void ChangeScene(string sceneName)
    {
        gamePause(1f);
        sound2.EffectSoundPlay(bgList[1]);
        int guide = PlayerPrefs.GetInt("guideAdCount", 0);
        guide++;
        PlayerPrefs.SetInt("guideAdCount", guide);
        PlayerPrefs.Save();
        SceneManager.LoadScene(sceneName);
    }
    public void CloseGuide()
    {
        sound2.EffectSoundPlay(bgList[2]);
        guidepanel.gameObject.SetActive(false);
    }

    public void CloseFeverGuide()
    {
        sound2.EffectSoundPlay(bgList[2]);
        GameManager gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        feverPanel.gameObject.SetActive(false);
        gm.UpdateFeverScore(gm.fCount);
    }
}
