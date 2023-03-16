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

    public void gamePause(float timescale)
    {
        Time.timeScale = timescale; // 게임 시간 일시 정지
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
            Panel.SetActive(true);
            Back.GetComponent<Background>().enabled = false;
        }
    }

    public void UpdateFeverScore(int feverScore)
    {
        feverSlider.value = (feverScore / 5.0f );
        Debug.Log(feverSlider.value);
    }
}
