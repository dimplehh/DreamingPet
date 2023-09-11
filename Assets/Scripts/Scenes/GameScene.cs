using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameScene : BaseScene
{
    public GameObject player;
    [SerializeField]
    ScoreManager scoreManager;
    [SerializeField]
    CoinManager coinManager;
    public bool gameState;
    public GameObject GuidePanel;


    void Start()
    {
        scoreManager.GenerateScore();
        coinManager.GenerateCoin();
    }

    protected override void Init()
    {
        base.Init();
        //SceneType = Define.Scene.Game;

        int guide = PlayerPrefs.GetInt("guideAdCount", 0);
        if (guide == 0)
        {
            GuidePanel.gameObject.SetActive(true);
        }

        /*
        guide++;
        PlayerPrefs.SetInt("guideAdCount", guide);
        PlayerPrefs.Save();
        */

        gameState = false;
        player = Managers.Game.Spawn("dog");
        GameObject bone = Managers.Game.Spawn("bone");

        Managers.Game.gamePause(0f); //게임 시작 전 일시정

    }
    public override void Clear()
    {

    }

    void Update(){
        /*터치 시 게임 시작*/
        if (gameState == false && Input.GetMouseButton(0) &&
            (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < 3.4f) && !GuidePanel.activeSelf)
        {
                GameObject.Find("Touch").SetActive(false);
                Managers.Game.gamePause(1f);
                gameState = true;
        }

        /*Score Update*/
        scoreManager.UpdateScore(player);
    }

    
}