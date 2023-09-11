using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameScene : BaseScene
{
    GameObject player;
    public bool gameState = true;
    public GameObject GuidePanel;
    [SerializeField]
    GameObject Touch;

    protected override void Init()
    {
    }

    void Start()
    {
        ScoreManager.instance.GenerateScore();
        CoinManager.instance.GenerateCoin();
        int guide = PlayerPrefs.GetInt("guideAdCount", 0);
        if (guide == 0)
        {
            GuidePanel.gameObject.SetActive(true);
        }
        gameState = false;
        Managers.Game.gamePause(0f); //게임 시작 전 일시정지
    }
    public override void Clear()
    {

    }

    void Update(){
        /*터치 시 게임 시작*/
        if (gameState == false && Input.GetMouseButton(0) &&
            (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < 3.4f) && !GuidePanel.activeSelf)
        {
                Touch.SetActive(false);
                Managers.Game.gamePause(1f);
                gameState = true;
        }
        if (Touch.activeSelf == true)
            return;
        else
            ScoreManager.instance.UpdateScore(GameManager.instance.player);
    }

    
}