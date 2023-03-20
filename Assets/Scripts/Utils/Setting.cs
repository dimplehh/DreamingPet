using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public
    GameObject panel;
    [SerializeField]
    LevelManager lv;
    public void ClickSetting()
    {
        panel.transform.GetChild(3).gameObject.SetActive(true);
        panel.transform.GetChild(1).gameObject.SetActive(false);
        panel.SetActive(true);
    }
    public void ClickClose()
    {
        panel.transform.GetChild(3).gameObject.SetActive(false);
        panel.transform.GetChild(1).gameObject.SetActive(true);
        lv.stop = false;
        lv.StopEnemy();
        
        panel.SetActive(false);
    }
    public void gamePause(float timescale)
    {
        Time.timeScale=timescale;
    }
}
