using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager:MonoBehaviour
{
    [SerializeField]
    float maxSpawnDelay;
    [SerializeField]
    float curSpawnDelay;
    [SerializeField]
    float speed;
    public float score;
    public int life;
    public Image[] lifeImage;
    [SerializeField]
    GameObject Panel;
    [SerializeField]
    GameObject Back;

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

    //프로토타입 캐릭터 삭제에 사용된다 추후 게임에서는 사용되지 않는다.
    public void DestroyPlayer()
    {
        if (GameObject.FindWithTag("Player") == true)
        {
            Destroy(GameObject.FindWithTag("Player"));
            Destroy(GameObject.FindWithTag("Bone"));
        }
        else
        {
            GameObject.Find("@Scene").GetComponent<GameScene>().player = Spawn("dog");
            Spawn("bone");
            UpdateLife(5);
            GameObject.Find("PlayerSpeedSlider").GetComponent<PlayerSpeed>().speedSlider.value = 1f;
        }
    }
}
