using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager:MonoBehaviour
{
    public float score;
    public int life;
    public Image[] lifeImage;
    [SerializeField]
    GameObject Panel;
    public GameObject Back;
    [SerializeField]
    Slider feverSlider;
    public bool feverState;
    GameObject player;
    GameObject bone;
    float feverTime = 1.3f;

    public GameObject ReAd;
    [SerializeField]
    TMP_Text Timer;
    public bool EndPoint;

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
    }

    /* 플레이어의 목숨 업데이트 */
    public void UpdateLife(int curlife){
        /*life 초기화*/
        for(int i = 0; i < 5; i++){
            lifeImage[i].color = new Color(1,1,1,0);
        }
        /*life 적용*/
        for(int i = 0; i < curlife; i++){
            lifeImage[i].color = new Color(1,1,1,1);
        }
        if (curlife <= 0)
        {
            if (EndPoint == false)
            {
                gameObject.GetComponent<LevelManager>().stop = true;
                gameObject.GetComponent<LevelManager>().StopEnemy();
                StartCoroutine("RestartAd");
            }
            else
            {
                NoRewardAd();
            }
        }
    }
    public void UpdateFeverScore(int feverScore)
    {
        feverSlider.value = (feverScore / 2.0f );
        if (feverSlider.value == 1.0f)
        {
            feverState = true;
            StartCoroutine(FeverTime());
        }
    }

    IEnumerator FeverTime()
    {
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        player.GetComponent<ChangeColor>().enabled = true;
        Time.timeScale = feverTime;
        yield return new WaitForSeconds(10f);
        feverState = false;
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        player.GetComponent<ChangeColor>().enabled = false;
        Time.timeScale = 1.0f;
        player.GetComponent<Player>().feverScore = 0;
        feverSlider.value = 0.0f;
    }

    IEnumerator EndAd()
    {
        yield return new WaitForSeconds(1f);
        Managers.Ad.ShowFrontAd();
    }

    IEnumerator RestartAd()
    {
        ReAd.SetActive(true);
        Back.GetComponent<Background>().enabled = false;
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
        Back.GetComponent<Background>().enabled = false;
        ReAd.SetActive(false);
        Panel.SetActive(true);
        int endAdCount = PlayerPrefs.GetInt("EndAdCount", 0);
        endAdCount++;
        PlayerPrefs.SetInt("EndAdCount", endAdCount);
        PlayerPrefs.Save();
        if (endAdCount % 3 == 0) StartCoroutine(EndAd());
    }

    
    
}
