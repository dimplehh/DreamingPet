using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    public
    GameObject panel;
    [SerializeField]
    LevelManager lv;
    public void ClickSetting()
    {
        gamePause(0f);
        panel.SetActive(true);
    }
    public void ClickClose()
    {
        gamePause(1f);
        panel.SetActive(false);
    }
    public void gamePause(float timescale)
    {
        if (GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().feverState && timescale != 0f)
            timescale = 2.5f;
        Time.timeScale=timescale;
    }

    public void ChangeScene(string sceneName)
    {
        gamePause(1f);
        SceneManager.LoadScene(sceneName);
    }
    
}
