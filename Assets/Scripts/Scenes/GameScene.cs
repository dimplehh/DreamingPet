using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameScene : BaseScene
{
    public GameObject player;
    [SerializeField]
    ScoreManager scoreManager;
    [SerializeField] bool gameState;

    void Start()
    {
        scoreManager.GenerateScore();
    }

    protected override void Init()
    {
        base.Init();

        //SceneType = Define.Scene.Game;

        Managers.Game.gamePause(0f); //게임 시작 전 일시정
        gameState = false;
        player = Managers.Game.Spawn("dog");
        GameObject bone = Managers.Game.Spawn("bone");
    }
    public override void Clear()
    {

    }

    void Update(){
        /*터치 시 게임 시작*/
        if (gameState == false && Input.GetMouseButton(0))
        {
            GameObject.Find("Touch").SetActive(false);
            Managers.Game.gamePause(1f);
            gameState = true;
        }

        /*Score Update*/
        scoreManager.UpdateScore(player);
    }

    
}
