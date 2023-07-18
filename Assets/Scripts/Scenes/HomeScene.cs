using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HomeScene : BaseScene
{
    public GameObject canvas;
    public int story;
    public int coin;
    [SerializeField]
    TMP_Text coinText;
    [SerializeField]
    ShopManager shopManager;

    protected override void Init()
    {
        base.Init();
        shopManager.Load();
        story = PlayerPrefs.GetInt("StoryCount", 0);
        coin = PlayerPrefs.GetInt("TotalCoin", 0);
        coinText.text = string.Format("{0:n0}", coin);
        if (story == 0)
        {
            canvas.GetComponent<HomeSetting>().StoryButton();
            PlayerPrefs.SetInt("StoryCount", 1);
            PlayerPrefs.Save();
        }
    }
    public override void Clear()
    {

    }
}
