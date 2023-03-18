using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager:MonoBehaviour
{
    public float score;
    public int life;
    public Image[] lifeImage;
    [SerializeField]
    GameObject Panel;
    [SerializeField]
    GameObject Back;
    [SerializeField]
    Slider feverSlider;
    public bool feverState;
    GameObject player;
    private int EndAdCount = 0;

    public void gamePause(float timescale)
    {
        if (Time.timeScale == 0)
            Time.timeScale = 1.0f;//나중에 이 값을 fever 상태에 따라 바뀌도록...
        else
            Time.timeScale = 0.0f;
        //Time.timeScale = timescale; // 게임 시간 일시 정지
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
        player = GameObject.FindGameObjectWithTag("Player");
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
            if (EndAdCount == 0)
            {
                EndAdCount++;
                

            }
            else
            {
                Panel.SetActive(true);
                Back.GetComponent<Background>().enabled = false;

                int endAdCount = PlayerPrefs.GetInt("EndAdCount", 0);
                endAdCount++;
                PlayerPrefs.SetInt("EndAdCount", endAdCount);
                PlayerPrefs.Save();
                if (endAdCount % 3 == 0) StartCoroutine(EndAd());
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
        yield return new WaitForSeconds(10f);
        feverState = false;
        player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        player.GetComponent<Player>().feverScore = 0;
        feverSlider.value = 0.0f;
    }

    IEnumerator EndAd()
    {
        yield return new WaitForSeconds(1f);
        Managers.Ad.ShowFrontAd();
    }
}
