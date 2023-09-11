using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float score;
    public int life;
    public int fCount;
    public GameObject[] lifeImages;
    public GameObject OverPanel;
    public GameObject feverPanel;
    public GameObject Back;
    [SerializeField]
    GameObject shieldIcon;
    [SerializeField]
    public Slider feverSlider;
    [SerializeField]
    Slider shieldSlider;
    public bool feverState;
    public bool shieldState;
    [SerializeField]
    GameObject feverBack;
    public GameObject player;
    GameObject bone;
    [SerializeField]
    float feverTime = 2.5f;
    public SoundManager soundManager;
    public SoundManager2 soundManager2;
    public CoinManager coinManager;


    [SerializeField]
    AudioClip feverBGM;
    public AudioClip mainBGM;
    [SerializeField]
    AudioClip effectSound;
    public GameObject ReAd;
    [SerializeField]
    TMP_Text Timer;

    TMP_Text Timers;
    public bool EndPoint;  

    float backSpeed;
    public bool clean;
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
    public float Coin(float coin)
    {
        return coin;
    }
    public int Life(int life)
    {
        return life;
    }

    void Start()
    {
        Managers mg = Managers.Instance;///이걸 나중에 사용할 수 있을 것(싱글톤 클래스)- 코드 깔끔히 하는 용  
        feverState = false;
        shieldState = false;
        clean = false;
        EndPoint = false;
        fCount = 5;
        player = GameObject.FindGameObjectWithTag("Player");
        bone = GameObject.FindGameObjectWithTag("Bone");
        soundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>();
        soundManager2 = GameObject.FindGameObjectWithTag("Sound2").GetComponent<SoundManager2>();
        coinManager= GameObject.FindGameObjectWithTag("CoinManager").GetComponent<CoinManager>();
        Timers = GameObject.FindGameObjectWithTag("Canvas").transform.Find("Timer").GetComponent<TMP_Text>();
    }

    /* 플레이어의 목숨 업데이트 */
    public void UpdateCoin(int curcoin)
    {
        coinManager.coin += curcoin;
        coinManager.savedCoin += curcoin;
        coinManager.coinText.text = string.Format("{0:n0}", coinManager.coin) ;
        PlayerPrefs.SetInt(coinManager.KeyString, coinManager.savedCoin);
        PlayerPrefs.Save();
        coinManager.TotalCoinText.text = string.Format("{0:n0}", coinManager.savedCoin.ToString());
    }

    public void UpdateLife(int curlife)
    {
        
            for (int i = 0; i < curlife; i++)
            {
                if (!lifeImages[i].activeSelf)
                    lifeImages[i].SetActive(true);
            }
            for (int i = curlife; i < 3; i++)
            {
                if (lifeImages[i].activeSelf)
                    lifeImages[i].SetActive(false);
            }
        if (curlife <= 0)
        {
            StopAll();
            if (soundManager.soundOn) soundManager.BgSoundStop(mainBGM);
            OverPanel.SetActive(true);
            OverPanel.transform.Find("Menu2").gameObject.SetActive(true);
            OverPanel.transform.Find("Menu1").gameObject.SetActive(false);
            /*임시 주석처리(수정중)
            if (EndPoint == false)
            {
                StopAll();
                if (soundManager.soundOn) soundManager.BgSoundStop(mainBGM);
                OverPanel.SetActive(true);
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
                if (soundManager.soundOn) soundManager.BgSoundStop(mainBGM);
                OverPanel.SetActive(true);
                OverPanel.transform.Find("Menu2").gameObject.SetActive(true);
                OverPanel.transform.Find("Menu1").gameObject.SetActive(false);
                
            }

            int guide = PlayerPrefs.GetInt("guideAdCount", 0);
            guide++;
            PlayerPrefs.SetInt("guideAdCount", guide);
            PlayerPrefs.Save();
            */
        }
    }
    public void UpdateFeverScore(int feverScore)
    {
        feverSlider.value = (feverScore * 10.0f / fCount);
        if (feverSlider.value == 10.0f)
        {
            int story = PlayerPrefs.GetInt("feverPanelCount", 0);
            if (story == 0)
            {
                feverPanel.gameObject.SetActive(true);
                PlayerPrefs.SetInt("feverPanelCount", 1);
                PlayerPrefs.Save();
                Time.timeScale = 0f;
            }
            if(!feverPanel.gameObject.activeSelf)
            {
                feverState = true;
                player.gameObject.layer = 0;
                StartCoroutine(FeverTime());
            }
        }
    }

    public void UpdateShieldScore(int shieldScore)
    {
        shieldSlider.value = shieldScore;
        if (shieldScore >= 5)
        {
            shieldState = true;
            shieldIcon.GetComponent<BounceAnim>().time = 0;
        }
        else
            shieldState = false;
    }

    IEnumerator FeverTime()
    {
        shieldIcon.SetActive(true);
        player.GetComponent<Player>().shieldScore = 0;
        player.transform.Find("bubble").gameObject.SetActive(false);
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
        soundManager2.EffectSoundPlay(effectSound);
        if (soundManager.soundOn) soundManager.BgSoundPlay(feverBGM);

        yield return new WaitForSeconds(20f);

        feverState = false;
        clean = true;
        shieldIcon.SetActive(false);
        shieldSlider.value = 0;
        if (shieldState)
            player.transform.Find("bubble").gameObject.SetActive(true);
        player.GetComponent<Player>().feverScore = 0;
        gameObject.GetComponent<LevelManager>().DeleteEnemy();
        gameObject.GetComponent<LevelManager>().DeleteShieldPiece();
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        player.GetComponent<ChangeColor>().enabled = false;
        Time.timeScale = 1.0f;
        feverSlider.GetComponent<SliderTimer>().enabled = false;
        player.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        gameObject.GetComponent<FadeScript>().Fade();
        Back.GetComponent<Background2>().speed = backSpeed;
        Back.SetActive(true);
        feverBack.SetActive(false);
        feverBack.transform.position = new Vector3(0.0f, -23.0f, 0.0f);
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
        clean = false;
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
            Timer.text = (10 - i).ToString();
            yield return new WaitForSeconds(1f);
        }
        NoRewardAd();
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
        gameObject.GetComponent<LevelManager>().StopCoin();
        gameObject.GetComponent<LevelManager>().StopShieldPiece();
        Back.GetComponent<Background2>().enabled = false;
    }

    public void OverAd()
    {
        Managers.Ad.ShowRewardAd(player, bone, this);
        //Timers.gameObject.SetActive(true);
        //startTimer();
    }
    public void startTimer()
    {
        StartCoroutine(restartTimer());
    }
    IEnumerator restartTimer()
    {
        Debug.Log("DDDD");
        for (int i = 1; i <= 3; i++)
        {
            Timers.text = (4 - i).ToString();
            yield return new WaitForSecondsRealtime(1f);
            Debug.Log(i);
        }
        Debug.Log("AAAA");
        Timers.gameObject.SetActive(false);
        Debug.Log("BBBB");
        Time.timeScale = 1f;
        Debug.Log("CCCC");
    }
}
