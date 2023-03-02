using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScene : BaseScene
{
    public TMP_Text scoreText;
    [SerializeField] GameObject player;
    
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


    void Update(){
        /*Score Update*/
        if(player!=null){
            scoreText.text = "Score : " + string.Format("{0:n0}",player.GetComponent<Player>().score);
            Managers.Game.score = player.GetComponent<Player>().score;
        }
    }
}
