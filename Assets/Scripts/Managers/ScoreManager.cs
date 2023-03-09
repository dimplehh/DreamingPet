using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    public TMP_Text scoreText2;
    public TMP_Text highScoreText2;
    public int savedScore = 0;
    private string KeyString = "HighScore";

    public void GenerateScore()
    {
        savedScore = PlayerPrefs.GetInt(KeyString);
        PlayerPrefs.Save();
        highScoreText.text = "BEST : " + savedScore.ToString();
        highScoreText2.text = savedScore.ToString();
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt(KeyString, 0);
        savedScore = PlayerPrefs.GetInt(KeyString);
        PlayerPrefs.Save();
        highScoreText.text = "BEST : " + string.Format("{0:n0}", savedScore);
        highScoreText2.text = string.Format("{0:n0}", savedScore);
    }
    
    public void UpdateScore(GameObject player)
    {
        if (player != null)
        {
            scoreText.text = "Score : " + string.Format("{0:n0}", player.GetComponent<Player>().score);
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
