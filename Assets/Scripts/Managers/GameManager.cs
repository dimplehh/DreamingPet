using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager:MonoBehaviour
{
    public float score;
    public int life;
    public TMP_Text lifeText;
    public GameObject OverPanel;
    public GameObject Back;
    [SerializeField]
    public Slider feverSlider;
    public bool feverState;
    [SerializeField]
    GameObject feverBack;
    GameObject player;
    GameObject bone;
    [SerializeField]
    float feverTime = 2.5f;
    SoundManager soundManager;

    [SerializeField]
    AudioClip feverBGM;
    [SerializeField]
    AudioClip mainBGM;
    public GameObject ReAd;
    [SerializeField]
    TMP_Text Timer;
    public bool EndPoint;
    float time = 0f;
    float F_time = 1f;

    float backSpeed;

    public void gamePause(float timescale)
    {
        if (Time.timeScale == 0)
        {
            if (feverState)
                Time.timeScale = feverTime;
            else
                Time.timeScale = 1.0f;
        }
        else
            Time.timeScale = 0.0f;
    }

    public GameObject Spawn(string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);
        return go;
    }

    public float Score(float score)
    {
        return score;
    }
    public int Life(int life)
    {
        return life;
    }

    void Start()
    {
        Managers mg = Managers.Instance;///이걸 나중에 사용할 수 있을 것(싱글톤 클래스)- 코드 깔끔히 하는 용  
        feverState = false;
        EndPoint = false;
        player = GameObject.FindGameObjectWithTag("Player");
        bone = GameObject.FindGameObjectWithTag("Bone");
        soundManager = GameObject.Find("soundManager").GetComponent<SoundManager>();
    }

    /* 플레이어의 목숨 업데이트 */
    public void UpdateLife(int curlife){
        lifeText.text = player.GetComponent<Player>().life.ToString();
        if (curlife <= 0)
        {
            if (EndPoint == false)
            {
                StopAll();
                OverPanel.SetActive(true);
                OverPanel.transform.Find("Menu2").gameObject.SetActive(false);
                OverPanel.transform.Find("Menu1").gameObject.SetActive(true);
                EndPoint = true;
                int endAdCount = PlayerPrefs.GetInt("EndAdCount", 0);
                endAdCount++;
                PlayerPrefs.SetInt("EndAdCount", endAdCount);
                PlayerPrefs.Save();
                if (endAdCount % 3 == 0) StartCoroutine(EndAd());
            }
            else
            {
                StopAll();
                OverPanel.SetActive(true);
                OverPanel.transform.Find("Menu2").gameObject.SetActive(true);
                OverPanel.transform.Find("Menu1").gameObject.SetActive(false);
            }
        }
    }
    public void UpdateFeverScore(int feverScore)
    {
        feverSlider.value = (feverScore * 15.0f / 5) + 0.12f;
        if (feverSlider.value == 15.0f)
        {
            feverState = true;
            StartCoroutine(FeverTime());
            //StartCoroutine(FadeFlow());
        }
    }
    IEnumerator FeverTime()
    {
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        player.GetComponent<ChangeColor>().enabled = true;
        Time.timeScale = feverTime;
        feverSlider.GetComponent<SliderTimer>().enabled = true;
        player.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        gameObject.GetComponent<FadeScript>().Fade();
        backSpeed = Back.GetComponent<Background2>().speed;
        Back.GetComponent<Background2>().speed = 0.0f;
        Back.SetActive(false);
        feverBack.SetActive(true);
        if(soundManager.soundOn)soundManager.BgSoundPlay(feverBGM);

        yield return new WaitForSeconds(30f);

        feverState = false;
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        player.GetComponent<ChangeColor>().enabled = false;
        Time.timeScale = 1.0f;
        player.GetComponent<Player>().feverScore = 0;
        feverSlider.GetComponent<SliderTimer>().enabled = false;
        player.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        gameObject.GetComponent<FadeScript>().Fade();
        Back.GetComponent<Background2>().speed = backSpeed;
        Back.SetActive(true);
        feverBack.SetActive(false);
        feverBack.transform.position = new Vector3(0.0f,-5.0f,0.0f);
        soundManager.BgSoundStop(feverBGM);
        if (soundManager.soundOn)
        {
            soundManager.BgSoundStop(feverBGM);
            soundManager.BgSoundPlay(mainBGM);
        }

        StartCoroutine(InvicibleTime(player));
    }

    IEnumerator InvicibleTime(GameObject gm)
    {
        gm.layer = 7;
        gm.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(3f);
        gm.layer = 0;
        gm.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    IEnumerator EndAd()
    {
        yield return new WaitForSeconds(1f);
        Managers.Ad.ShowFrontAd();
    }

    IEnumerator RestartAd()
    {
        ReAd.SetActive(true);
        Back.GetComponent<Background2>().enabled = false;
        //GameObject.Find("GameManager").GetComponent<LevelManager>().StopEnemy();
        //적 멈추기
        EndPoint = true;
        for (int i = 1; i <= 10; i++)
        {
            Timer.text = (10-i).ToString();
            yield return new WaitForSeconds(1f);
        }
        NoRewardAd();
    }

    public void showReward()
    {
        Managers.Ad.ShowRewardAd(player,bone,this);
    }

    public void NoRewardAd()
    {
        if (EndPoint == false) StopCoroutine("RestartAd");
        Back.GetComponent<Background2>().enabled = false;
        ReAd.SetActive(false);
        OverPanel.SetActive(true);
        int endAdCount = PlayerPrefs.GetInt("EndAdCount", 0);
        endAdCount++;
        PlayerPrefs.SetInt("EndAdCount", endAdCount);
        PlayerPrefs.Save();
        if (endAdCount % 3 == 0) StartCoroutine(EndAd());
    }

    public void StopAll()
    {
        gameObject.GetComponent<LevelManager>().stop = true;
        gameObject.GetComponent<LevelManager>().StopEnemy();
        gameObject.GetComponent<LevelManager>().StopFever();
        gameObject.GetComponent<LevelManager>().StopRain();
        gameObject.GetComponent<LevelManager>().StopSpaceShip();
        gameObject.GetComponent<LevelManager>().StopHeart();
        Back.GetComponent<Background2>().enabled = false;
    }

    public void OverAd()
    {
        Managers.Ad.ShowRewardAd(player, bone, this);
    }
}
