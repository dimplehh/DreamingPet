using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScene : BaseScene
{
    public GameObject canvas;
    public int story;

    protected override void Init()
    {
        base.Init();

        story = PlayerPrefs.GetInt("StoryCount", 0);
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
