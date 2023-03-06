using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameScene : BaseScene
{
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text scoreText2;
    public TMP_Text highScoreText2;
    public int savedScore = 0;
    public GameObject player;
    [SerializeField] bool gameState;
    private string KeyString = "HighScore";

    void Start()
    {
        savedScore = PlayerPrefs.GetInt(KeyString);
        PlayerPrefs.Save();
        highScoreText.text = "BEST : " + savedScore.ToString();
        highScoreText2.text = savedScore.ToString();
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

    public void ResetScore()
    {
        PlayerPrefs.SetInt(KeyString, 0);
        PlayerPrefs.Save();
        highScoreText.text = "BEST : " + string.Format("{0:n0}", savedScore);
        highScoreText2.text = string.Format("{0:n0}", savedScore);
    }

    void Update(){
        /*터치 시 게임 시작*/
        if (gameState==false && Input.GetMouseButton(0))
        {
            GameObject.Find("Touch").SetActive(false);
            Managers.Game.gamePause(1f);
            gameState = true;
        }

        /*Score Update*/
        if (player!=null){
            scoreText.text = "Score : " + string.Format("{0:n0}",player.GetComponent<Player>().score);
            scoreText2.text = string.Format("{0:n0}", player.GetComponent<Player>().score);
            highScoreText.text = "BEST : " + string.Format("{0:n0}", savedScore);
            highScoreText2.text = string.Format("{0:n0}", savedScore);

            if (player.GetComponent<Player>().score > savedScore)
            {
                savedScore = player.GetComponent<Player>().score;
                PlayerPrefs.SetInt(KeyString, player.GetComponent<Player>().score);
                PlayerPrefs.Save();
                highScoreText.text = "BEST : " + string.Format("{0:n0}", savedScore);
                highScoreText2.text = string.Format("{0:n0}", savedScore);
            }

            Managers.Game.score = player.GetComponent<Player>().score;
        }
    }

    
}
