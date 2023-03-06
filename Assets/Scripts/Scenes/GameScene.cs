using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScene : BaseScene
{
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public int savedScore = 0;
    [SerializeField] GameObject player;

    private string KeyString = "HighScore";

    void Start()
    {
        savedScore = PlayerPrefs.GetInt(KeyString);
        PlayerPrefs.Save();
        highScoreText.text = "BEST : " + savedScore.ToString();
    }

    protected override void Init()
    {
        base.Init();

        //SceneType = Define.Scene.Game;

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
    }

    void Update(){
        /*Score Update*/
        if(player!=null){
            scoreText.text = "Score : " + string.Format("{0:n0}",player.GetComponent<Player>().score);
            highScoreText.text = "BEST : " + string.Format("{0:n0}", savedScore);

            if(player.GetComponent<Player>().score > savedScore)
            {
                Debug.Log("hi?");
                savedScore = player.GetComponent<Player>().score;
                PlayerPrefs.SetInt(KeyString, player.GetComponent<Player>().score);
                PlayerPrefs.Save();
                highScoreText.text = "BEST : " + string.Format("{0:n0}", savedScore);
            }

            Managers.Game.score = player.GetComponent<Player>().score;
        }
    }
}
